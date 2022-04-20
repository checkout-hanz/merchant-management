using MongoDB.Driver;

namespace Merchant.Management.MongoDb.Configuration
{
    public class MongoContext : IMongoContext
    {
        private readonly Lazy<IMongoDatabase> _database;

        private readonly Lazy<IMongoCollection<Merchant.Management.MongoDb.Models.Merchant>> _messageRecords;

        public MongoContext(IMongoClient mongoClient, IMongoSettings mongoSettings)
        {
            Client = mongoClient;
            _database = new Lazy<IMongoDatabase>(() => mongoClient.GetDatabase(mongoSettings.Database));

            Lazy<IMongoCollection<T>> CreateLazyCollection<T>(string collectionName)
            {
                return new Lazy<IMongoCollection<T>>(() => Database.GetCollection<T>(collectionName));
            }

            _messageRecords = CreateLazyCollection<Merchant.Management.MongoDb.Models.Merchant>(DatabaseConstants.MerchantCollection);
        }

        public IMongoClient Client { get; }
        public IMongoDatabase Database => _database.Value;
        public IMongoCollection<Merchant.Management.MongoDb.Models.Merchant> Merchants => _messageRecords.Value;
    }
}
