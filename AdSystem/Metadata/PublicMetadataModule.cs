using AdSystem.ApiObjects;
using Nancy;
using Nancy.Metadata.Modules;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Fluent;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.Metadata
{
    public class PublicMetadataModule : MetadataModule<SwaggerRouteMetadata>
    {
        public PublicMetadataModule()
        {
            Describe["RegisterAdvertiser"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.Created).ToString(), typeof(MetaData), "")
            .WithSummary("Register a new advertiser account")
            .WithRequestParameter("username", "string", null, true, "Account username", "form")
            .WithRequestParameter("password", "string", null, true, "Account password", "form")
            .WithRequestParameter("openRtbUrl", "string", null, true, "An URL to advertiser OpenRTB API Server", "form"));

            Describe["RegisterPublisher"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.Created).ToString(), typeof(MetaData), "{\"meta\":{\"code\":201},\"data\":{\"apiKey\":\"f839d1a4b74f469293b5ce103aa91f10\",\"href\":{\"embedUrl\":\"/api/publisher/embedcode\"}}}")
            .WithSummary("Register a new publisher account")
            .WithRequestParameter("username", "string", null, true, "Account username", "form")
            .WithRequestParameter("password", "string", null, true, "Account password", "form")
            .WithRequestParameter("domain", "string", null, true, "Publisher website domain", "form")
            .WithRequestParameter("categories", "string", null, true, "A comma-separated array of OpenRTB category types", "form"));

            Describe["LoginAdvertiser"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "")
            .WithSummary("Login to an advertiser account")
            .WithRequestParameter("username", "string", null, true, "Account username", "form")
            .WithRequestParameter("password", "string", null, true, "Account password", "form"));

            Describe["LoginPublisher"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "{\"meta\":{\"code\":200},\"data\":{\"apiKey\":\"f839d1a4b74f469293b5ce103aa91f10\",\"href\":{\"embedUrl\":\"/api/publisher/embedcode\"}}}")
            .WithSummary("Login to a publisher account")
            .WithRequestParameter("username", "string", null, true, "Account username", "form")
            .WithRequestParameter("password", "string", null, true, "Account password", "form"));

            Describe["ClientGetAd"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "")
            .WithSummary("Get the HTML code of the RTB auction winning ad")
            .WithRequestParameter("publisherid", "integer", null, true, "Publisher identifier", "query"));
        }
    }
}
