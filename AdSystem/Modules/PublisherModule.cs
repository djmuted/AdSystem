using System;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using AdSystem.Models;
using AdSystem.ApiObjects;
using System.Xml.Linq;
using Nancy.ModelBinding;
using Nancy.Authentication.Stateless;
using AdSystem.NancySettings;
using System.Security.Claims;
using System.Linq;
using System.Drawing;
using System.IO;
using AdSystem.RTBSystem;

namespace AdSystem.Modules
{
    public class PublisherModule : IAdSystemModule
    {
        private Publisher publisher;
        public PublisherModule(IAppConfiguration appConfig)
        {
            Before += ctx =>
            {
                Guid apiKey;
                if (Guid.TryParse(ctx.Request.Headers.Authorization, out apiKey))
                {
                    publisher = db.Publishers.Where(a => a.account.apiKey == apiKey).FirstOrDefault();
                }
                if (publisher == null)
                {
                    return ErrorResponse(HttpStatusCode.Unauthorized, "UnauthorizedAccessException", "API key is invalid or expired.");
                }
                return null;
            };
            Get("/api/publisher/embedcode", args =>
            {
                PublisherAdvertisement embed = new PublisherAdvertisement();
                embed.id = publisher.id.ToString();
                embed.embed = "<script src=\"//code.jquery.com/jquery-2.0.3.min.js\" type=\"text/javascript\"></script><script>var div=document.createElement(\"div\");div.style.width=\"728px\";div.style.height=\"90px\";div.style.padding=\"0 0 0 0\";document.currentScript.parentNode.insertBefore(div, document.currentScript);$.getJSON(\"" + Flurl.Url.Combine(new string[] { Program.config.externalUrl, "/api/publisher/ad?publisherid=" + this.publisher.id.ToString() }) + "\", function(result){div.innerHTML=result.data.embed;});</script>";
                return SuccessResponse(HttpStatusCode.OK, embed);
            });
        }
    }
}