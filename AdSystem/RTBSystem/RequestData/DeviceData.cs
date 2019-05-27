using Nancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.RTBSystem
{
    class DeviceData
    {
        public string ua;
        public string make;
        public string model;
        public string os;
        public string osv;
        public string ip;
        public GeolocationData geo;

        public DeviceData(RequestHeaders headers, string _ip, GeolocationData _geo)
        {
            var parser = UAParser.Parser.GetDefault();
            var dd = parser.Parse(headers.UserAgent);
            this.ua = headers.UserAgent;
            this.ip = _ip;
            this.os = dd.OS.Family;
            this.osv = dd.OS.Major + "." + dd.OS.Minor;
            this.make = dd.Device.Brand;
            this.model = dd.Device.Family;
            this.geo = _geo;
        }
    }
}
