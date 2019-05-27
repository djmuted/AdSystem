using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.ApiObjects
{
    class MetaData
    {
        public int code = 200;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string error_type;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string error_message;
    }
}
