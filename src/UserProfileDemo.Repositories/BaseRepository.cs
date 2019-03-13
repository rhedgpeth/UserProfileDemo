using System;
using Couchbase.Lite;

namespace UserProfileDemo.Respositories
{
    public abstract class BaseRepository : IDisposable
    {
        string DatabaseName { get; set; }
        ListenerToken DatabaseListenerToken { get; set; }

        protected virtual DatabaseConfiguration DatabaseConfig { get; set; }

        // tag::database[]
        Database _database;
        protected Database Database
        {
            get
            {
                if (_database == null)
                {
                    // tag::databaseCreate[]
                    _database = new Database(DatabaseName, DatabaseConfig);
                    // end::databaseCreate[]
                }

                return _database;
            }
            private set => _database = value;
        }
        // end::database[]

        protected BaseRepository(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception($"Repository Exception: Database name cannot be null or empty!");
            }

            DatabaseName = databaseName;

            // tag::registerForDatabaseChanges[]
            DatabaseListenerToken = Database.AddChangeListener(OnDatabaseChangeEvent);
            // end::registerForDatabaseChanges[]
        }

        // tag::addChangeListener[]
        void OnDatabaseChangeEvent(object sender, DatabaseChangedEventArgs e)
        {
            foreach (var documentId in e.DocumentIDs)
            {
                var document = Database?.GetDocument(documentId);

                string message = $"Document (id={documentId}) was ";

                if (document == null)
                {
                    message += "deleted";
                }
                else
                {
                    message += "added/updaAted";
                }

                Console.WriteLine(message);
            }
        }
        // end::addChangeListener[]

        // tag::databaseClose[]
        public void Dispose()
        {
            DatabaseConfig = null;

            Database.RemoveChangeListener(DatabaseListenerToken);
            Database.Close();
            Database = null;
        }
        // end::databaseClose[]
    }
}
