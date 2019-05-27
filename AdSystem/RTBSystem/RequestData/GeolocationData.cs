using AdSystem.ApiObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AdSystem.RTBSystem
{
    class GeolocationData
    {
        [JsonProperty(PropertyName = "as")]
        public string _as;
        public string city;
        public string country;
        public string countryCode;
        public string isp;
        public float lat;
        public float lon;
        public string org;
        public string query;
        public string region;
        public string regionName;
        public string status;
        public string timezone;
        public string zip;

        public GeolocationData(string ip)
        {
        }
        public static GeolocationData FromIP(string ip)
        {
            WebClient wc = new WebClient();
            string json = wc.DownloadString("http://ip-api.com/json/" + ip);
            return JsonConvert.DeserializeObject<GeolocationData>(json);
        }
    }
}
