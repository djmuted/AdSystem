using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.ApiObjects
{
    class LoginData
    {
        public string apiKey;
        public dynamic href;

        public LoginData(string _apiKey, dynamic _href)
        {
            this.apiKey = _apiKey;
            this.href = _href;
        }
    }
}
