using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using NLog;
using AdSystem.Models;

namespace AdSystem
{
    class Program
    {
        public static SystemConfiguration config;
        static void Main(string[] args)
        {
            LogManager.GetCurrentClassLogger().Debug("Starting AdSystem");
            config = SystemConfiguration.FromFile("AdSystemConfiguration.json");
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls(config.serverUrl)
                .UseStartup<Startup>()
                .Build();
            var ctx = new AdSystemDbContext();
            ctx.Database.EnsureCreated();
            ctx.SaveChanges();
            LogManager.GetCurrentClassLogger().Info("AdSystem started");
            host.Run();
        }
    }
}
