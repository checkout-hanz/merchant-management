using MongoDB.Driver;

namespace Merchant.Management.MongoDb.Configuration
{
    public interface IMongoContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }

        IMongoCollection<Merchant.Management.MongoDb.Models.Merchant> Merchants { get; }
    }
}