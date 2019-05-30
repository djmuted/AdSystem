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
    public class PublisherMetadataModule : MetadataModule<SwaggerRouteMetadata>
    {
        public PublisherMetadataModule()
        {
            Describe["EmbedCode"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "")
            .WithSummary("Get the HTML embed code for an ad")
            .WithRequestParameter("Authorization", "string", null, true, "Publisher API key", "header"));
        }
    }
}
