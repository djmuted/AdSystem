using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.ApiObjects
{
    class LoginData
    {
        public string apiKey;

        public LoginData(string _apiKey)
        {
            this.apiKey = _apiKey;
        }
    }
}
