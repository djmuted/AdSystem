using Nancy.Metadata.Swagger.Modules;
using Nancy.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.Modules
{
    public class DocsModule : SwaggerDocsModuleBase
    {
        public DocsModule(IRouteCacheProvider routeCacheProvider)
        : base(routeCacheProvider,
          "/api-docs/",                   // where module should be located
          "AdSystem API documentation",  // title
          "v1.0",                        // api version
          Program.config.externalUrl.Split("//")[1],    // host
          "/",                           // api base url (ie /dev, /api)
          "http")                        // schemes
        {
        }
    }
}
