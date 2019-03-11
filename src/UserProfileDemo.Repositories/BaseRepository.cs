using System;
using System.IO;
using Couchbase.Lite;
using UserProfileDemo.Core;

namespace UserProfileDemo.Respositories
{
    public abstract class BaseRepository : IDisposable 
    {
        string DatabaseName { get; set; }

        DatabaseConfiguration _databaseConfig;
        DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                {
                    if (AppInstance.User?.Username == null)
                    {
                        throw new Exception($"Repository Exception: A valid user is required!");
                    }

                    _databaseConfig = new DatabaseConfiguration
                    {
                        Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                        AppInstance.User.Username)
                    };
                }

                return _databaseConfig;
            }
            set => _databaseConfig = value;
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
            private set => _database = value;
        }

        protected BaseRepository(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception($"Repository Exception: Database name cannot be null or empty!");
            }

            DatabaseName = databaseName;
        }

        public void Dispose()
        {
            DatabaseConfig = null;
            Database.Close();
            Database = null;
        }
    }
}
