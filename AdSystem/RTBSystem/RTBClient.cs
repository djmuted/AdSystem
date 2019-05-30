using AdSystem.Models;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NLog;

namespace AdSystem.RTBSystem
{
    class RTBClient
    {
        private RequestHeaders headers;
        private AdSystemDbContext db;
        private Publisher publisher;
        private string client_ip;
        private string url;

        public RTBClient(RequestHeaders _headers, AdSystemDbContext context, Publisher _publisher, string ip, string _url)
        {
            this.headers = _headers;
            this.db = context;
            this.publisher = _publisher;
            this.client_ip = ip;
            this.url = _url;
        }
        public AdBidResponse GetAdBidResponse()
        {
            BidRequest bidRequest = new BidRequest();
            bidRequest.device = new DeviceData(headers, client_ip, GeolocationData.FromIP(client_ip));
            bidRequest.id = Guid.NewGuid().ToString("N");
            bidRequest.imp = new ImpressionData(bidRequest.id, new BannerData(728, 90));
            bidRequest.site = new SiteData(publisher, url);
            
            string bidRequestJson = JsonConvert.SerializeObject(bidRequest);
            Dictionary<Advertiser, BidResponse> bidResponses = new Dictionary<Advertiser, BidResponse>();
            Console.WriteLine("REQUEST " + bidRequestJson);

            Parallel.ForEach(db.Advertisers, (advertiser) =>
            {
                TimedWebClient wc = new TimedWebClient(Program.config.rtbConfig.timeout);
                wc.Headers.Set("Content-Type", "application/json");
                wc.Headers.Set("x-openrtb-version", Program.config.rtbConfig.version);
                try
                {
                    string responsejson = wc.UploadString(Flurl.Url.Combine(new string[] { advertiser.openRtbUrl, "/bid" }), bidRequestJson);
                    BidResponse bidResponse = JsonConvert.DeserializeObject<BidResponse>(responsejson);
                    decimal decimalTest = bidResponse.seatbid.bid.price;
                    if (decimalTest > 0)
                    {
                        lock (bidResponses)
                        {
                            Console.WriteLine("RESPONSE " + responsejson);
                            bidResponses.Add(advertiser, bidResponse);
                        }
                    }
                } catch(Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error("Advertiser failed to send a valid RTB Bid response "+ex);
                }
            });
            if (bidResponses.Count > 0) {
                var sorted = bidResponses.OrderBy(l => l.Value.seatbid.bid.price);
                foreach(var one in sorted)
                {
                    Guid guid;
                    Ad ad;
                    if (Guid.TryParse(one.Value.seatbid.bid.adid, out guid))
                    {
                        ad = db.Ads.Where(a => a.id == guid).FirstOrDefault();
                        if(ad == null)
                        {
                            continue;
                        } else
                        {
                            return new AdBidResponse(ad, one.Value);
                        }
                    } else
                    {
                        continue;
                    }

                }
            }
            return null;
        }
    }
}
