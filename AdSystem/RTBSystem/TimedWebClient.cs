using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AdSystem.RTBSystem
{
    public class TimedWebClient : WebClient
    {
        public int Timeout { get; set; }

        public TimedWebClient(int timeout)
        {
            this.Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.Timeout;
            return objWebRequest;
        }
    }


}
