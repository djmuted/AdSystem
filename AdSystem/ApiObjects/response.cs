using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.ApiObjects
{
    class response
    {
        public MetaData meta;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object data;

        public response(object _payload) {
            this.meta = new MetaData();
            this.data = _payload;
        }
        public response()
        {
            this.meta = new MetaData();
        }
    }
}
