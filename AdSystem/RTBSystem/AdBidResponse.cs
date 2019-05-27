using AdSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.RTBSystem
{
    class AdBidResponse
    {
        public Ad ad;
        public BidResponse bidResponse;

        public AdBidResponse(Ad _ad, BidResponse _bidResponse)
        {
            this.ad = _ad;
            this.bidResponse = _bidResponse;
        }
    }
}
