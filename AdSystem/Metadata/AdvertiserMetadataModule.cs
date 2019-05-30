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
    public class AdvertiserMetadataModule : MetadataModule<SwaggerRouteMetadata>
    {
        public AdvertiserMetadataModule()
        {
            Describe["AddAd"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.Created).ToString(), typeof(MetaData), "{\"meta\":{\"code\":201},\"data\":{\"id\":\"08d6e3d4-e1d8-10a4-26cf-a12538c0427d\",\"name\":\"My first ad\",\"href\":\"/api/advertiser/ads/08d6e3d4e1d810a426cfa12538c0427d\",\"bannerUrl\":\"/adimg/08d6e3d4-e1d8-10a4-26cf-a12538c0427d.png\"}}")
                        .WithSummary("Add a new ad")
                        .WithRequestParameter("name", "string", null, true, "The name of the ad", "form")
                        .WithRequestParameter("file", "file", null, true, "An image of the ad banner", "form")
                        .WithRequestParameter("Authorization", "string", null, true, "Advertiser API key", "header"));

            Describe["GetAds"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "")
            .WithSummary("Get the paged result containing all your ads")
            .WithRequestParameter("page", "integer", null, true, "The page number (starting from 1)", "query")
            .WithRequestParameter("pageSize", "integer", null, true, "Ads per page (max 100)", "query")
            .WithRequestParameter("Authorization", "string", null, true, "Advertiser API key", "header"));

            Describe["GetAd"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "")
            .WithSummary("Get the details of an ad")
            .WithRequestParameter("guid", "string", null, true, "Ad identifier", "path")
            .WithRequestParameter("Authorization", "string", null, true, "Advertiser API key", "header"));

            Describe["DeleteAd"] = desc => new SwaggerRouteMetadata(desc)
            .With(i => i.WithResponseModel(((int)HttpStatusCode.OK).ToString(), typeof(MetaData), "")
            .WithSummary("Delete an ad")
            .WithRequestParameter("guid", "string", null, true, "Ad identifier", "path")
            .WithRequestParameter("Authorization", "string", null, true, "Advertiser API key", "header"));

        }
    }
}
