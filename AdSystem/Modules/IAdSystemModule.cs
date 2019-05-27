using AdSystem.ApiObjects;
using AdSystem.Models;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AdSystem.Modules
{
    public class IAdSystemModule : NancyModule
    {
        protected Response Serialize<T>(T obj)
        {
            switch ((string)(this.Request.Query["format"]))
            {
                case "xml":
                    {
                        XNode node = JsonConvert.DeserializeXNode(JsonConvert.SerializeObject(obj), typeof(T).Name.Split('`')[0]);
                        string xml = node.ToString();
                        Response response = xml;
                        response.ContentType = "application/xml";
                        return response;
                    }
                case "json":
                default:
                    {
                        string json = JsonConvert.SerializeObject(obj);
                        Response response = json;
                        response.ContentType = "application/json";
                        return response;
                    }
            }
        }
        protected Response ErrorResponse(HttpStatusCode statuscode, string error_type, string error_message)
        {
            var errorResponse = new response();
            errorResponse.meta.code = (int)statuscode;
            errorResponse.meta.error_type = error_type;
            errorResponse.meta.error_message = error_message;
            var nancyResponse = Serialize(errorResponse);
            nancyResponse.StatusCode = statuscode;
            return nancyResponse;
        }
        protected Response SuccessResponse(HttpStatusCode statuscode, object data)
        {
            var createdResponse = new response();
            createdResponse.meta.code = (int)statuscode;
            createdResponse.data = data;
            var nancyResponse = Serialize(createdResponse);
            nancyResponse.StatusCode = statuscode;
            return nancyResponse;
        }
        protected AdSystemDbContext db = new AdSystemDbContext();
        public IAdSystemModule()
        {
            After += ctx =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                            .WithHeader("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT")
                                            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            };
        }
    }
}
