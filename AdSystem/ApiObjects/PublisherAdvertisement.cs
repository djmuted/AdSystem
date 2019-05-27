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
            this.embed = "<a href='"+ Program.config.externalUrl + "'><img src='" + Url.Combine(new string[] { Program.config.externalUrl, "/adimg/" + this.id + ".png" }) +"' alt='Put your ad here' height='728' width='90'></a>";
        }
        public PublisherAdvertisement(AdBidResponse resp, Publisher publisher)
        {
            this.id = resp.ad.id.ToString("N");
            this.embed = "<a href='"+ Url.Combine(new string[] { Program.config.externalUrl, "/api/click?id=" + this.id + "&pubid=" + publisher.id + "&imp=" + resp.bidResponse.seatbid.bid.impid}) + "'><img src='" + Url.Combine(new string[] { Program.config.externalUrl, "/adimg/" + this.id + ".png" }) +"' alt='" + resp.ad.name + "' height='728' width='90'></a>";
        }
    }
}
