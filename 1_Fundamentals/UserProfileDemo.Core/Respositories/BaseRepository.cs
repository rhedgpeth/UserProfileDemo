using System;
using System.IO;
using Couchbase.Lite;
using UserProfileDemo.Core.Models;
using UserProfileDemo.Core.Services;

namespace UserProfileDemo.Core.Respositories
{
    public abstract class BaseRepository 
    {
        string DatabaseName { get; set; }

        DatabaseConfiguration _databaseConfig;
        DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                {
                    _databaseConfig = new DatabaseConfiguration
                    {
                        Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                        AppInstance.User.Username)
                    };
                }

                return _databaseConfig;
            }
        }

        Database _database;
        protected Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new Database(DatabaseName, DatabaseConfig);
                }

                return _database;
            }
        }

        protected BaseRepository(string databaseName)
        {
            DatabaseName = databaseName;
        }
    }
}
