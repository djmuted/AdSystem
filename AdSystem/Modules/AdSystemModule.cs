using System;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using AdSystem.Models;
using AdSystem.ApiObjects;
using System.Xml.Linq;
using Nancy.ModelBinding;

namespace AdSystem.Modules
{
    public class AdSystemModule : IAdSystemModule
    {
        public AdSystemModule(IAppConfiguration appConfig)
        {
            var ctx = new AdSystemDbContext();
            Get("/", args => "Hello from Nancy running on " + DateTime.Now.ToString());
            Get("/test", _ =>
            {

                return "keks";
            });

        }
    }
}
