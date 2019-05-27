using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Nancy.Owin;

namespace AdSystem
{
    class Startup
    {
        private readonly IConfiguration config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath);
            config = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {

        }
        public void Configure(IApplicationBuilder app)
        {
            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(config, appConfig);
            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new Bootstrapper(appConfig)));
        }
    }
}
