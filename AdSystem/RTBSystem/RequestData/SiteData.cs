using System;
using System.Collections.Generic;
using System.Text;
using AdSystem.Models;
using Newtonsoft.Json;

namespace AdSystem.RTBSystem
{
    class SiteData
    {
        public string id;
        public string name;
        public string domain;
        public string[] cat;
        public string page;
        [JsonProperty(PropertyName = "ref")]
        public string referer;
        public PublisherData publisher;

        public SiteData(Publisher publisher, string uri)
        {
            this.id = publisher.id.ToString();
            this.name = publisher.domain;
            this.domain = publisher.domain;
            this.cat = publisher.categories.Split(',');
            this.page = uri;
            this.publisher = new PublisherData(publisher);
        }
    }
}
