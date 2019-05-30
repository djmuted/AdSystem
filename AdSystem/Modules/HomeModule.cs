﻿using System;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using AdSystem.Models;
using AdSystem.ApiObjects;
using System.Xml.Linq;
using Nancy.ModelBinding;

namespace AdSystem.Modules
{
    public class HomeModule : IAdSystemModule
    {
        public HomeModule() : base ("/")
        {
            Get("/", args => Response.AsRedirect($"/swagger-ui/index.html?url="+Flurl.Url.Combine(Program.config.serverUrl, "/api-docs")), null, "Docs");

        }
    }
}
