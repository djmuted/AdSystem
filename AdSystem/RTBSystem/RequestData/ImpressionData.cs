using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.RTBSystem
{
    class ImpressionData
    {
        public string id;
        public BannerData banner;

        public ImpressionData(string _id, BannerData _banner)
        {
            this.id = _id;
            this.banner = _banner;
        }
    }
}
