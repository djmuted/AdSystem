using AdSystem.Models;
using AdSystem.RTBSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Flurl;

namespace AdSystem.ApiObjects
{
    class PublisherAdvertisement
    {
        public string id;
        public string embed;

        public PublisherAdvertisement()
        {
            this.id = "00000000000000000000000000000000";
            this.embed = "<a href='"+ Program.config.externalUrl + "'><img src='" + Url.Combine(new string[] { Program.config.externalUrl, "/adimg/" + this.id + ".png" }) +"' alt='Put your ad here' width='728' height='90'></a>";
        }
        public PublisherAdvertisement(AdBidResponse resp, Publisher publisher)
        {
            this.id = resp.ad.id.ToString("N");
            this.embed = "<a href='"+ Url.Combine(new string[] { Program.config.externalUrl, "/api/click?id=" + this.id + "&pubid=" + publisher.id + "&imp=" + resp.bidResponse.seatbid.bid.impid}) + "'><img src='" + Url.Combine(new string[] { Program.config.externalUrl, resp.ad.bannerUrl }) +"' alt='" + resp.ad.name + "' width='728' height='90'></a>";
        }
    }
}
