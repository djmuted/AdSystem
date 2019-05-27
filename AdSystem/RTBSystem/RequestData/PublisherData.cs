using AdSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.RTBSystem
{
    class PublisherData
    {
        public string id;
        public string name;
        public string domain;

        public PublisherData(Publisher publisher)
        {
            this.id = publisher.id.ToString();
            this.name = publisher.domain;
            this.domain = publisher.domain;
        }
    }
}
