using Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infraestructure.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase db;

        public MongoDBContext(IOptions<SettingsConfig> settingsConfig)
        {
            var client = new MongoClient(settingsConfig.Value.MONGO_URI);
            db = client.GetDatabase(settingsConfig.Value.MONGO_DATABASE_NAME);
        }

        public IMongoDatabase GetConn() => db;
    }
}
