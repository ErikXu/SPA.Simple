using System.Configuration;
using MongoDB.Driver;

namespace SimpleStock.Common.Mongo
{
    public class MongoBase
    {
        private const string SettingsKey = "MongoServerSettings";
        private const string DbNameKey = "MongoDBName";

        protected MongoDatabase Init()
        {
            var connectionString = ConfigurationManager.AppSettings[SettingsKey];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new MongoException("No connection string is provided.");
            }

            var databaseName = ConfigurationManager.AppSettings[DbNameKey];
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new MongoException("No database name is specified.");
            }

            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(databaseName);
            return db;
        }
    }
}
