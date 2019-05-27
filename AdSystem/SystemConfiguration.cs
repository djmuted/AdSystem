using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using AdSystem.Database;
using NLog;
using AdSystem.RTBSystem;

namespace AdSystem
{
    class SystemConfiguration
    {
        public string serverUrl = "http://localhost:5050";
        public string externalUrl = "http://localhost/";
        public DatabaseConfiguration dbConfig = new DatabaseConfiguration("localhost", "adsystem", "root", "12345");
        public RTBConfiguration rtbConfig = new RTBConfiguration(100, "2.3");

        public SystemConfiguration()
        {
            
        }
        public static SystemConfiguration FromFile(string path)
        {
            SystemConfiguration config;
            if (File.Exists(path))
            {
                try
                {
                    config = JsonConvert.DeserializeObject<SystemConfiguration>(File.ReadAllText(path));
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error("Could not read system config: " + ex);
                    config = new SystemConfiguration();
                    LogManager.GetCurrentClassLogger().Warn("Initialized new config");
                }
            }
            else
            {
                config = new SystemConfiguration();
                LogManager.GetCurrentClassLogger().Warn("Initialized new config");
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(config));
            LogManager.GetCurrentClassLogger().Debug("Configuration initialized");
            return config;
        }
    }
}