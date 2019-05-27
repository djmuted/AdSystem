using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem.Database
{
    class DatabaseConfiguration
    {
        public string server;
        public string database;
        public string username;
        public string password;

        public DatabaseConfiguration(string _server, string _database, string _username, string _password)
        {
            this.server = _server;
            this.database = _database;
            this.username = _username;
            this.password = _password;
        }
    }
}
