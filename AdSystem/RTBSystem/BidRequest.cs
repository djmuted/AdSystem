using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.RTBSystem
{
    class BidRequest
    {
        public string id;
        public ImpressionData imp;
        public SiteData site;
        public DeviceData device;
        //public UserData user; //No collecting user data yet
        //public ExternalData ext; //Not needed
    }
}
