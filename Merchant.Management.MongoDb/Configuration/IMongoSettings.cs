namespace Merchant.Management.MongoDb.Configuration
{
    public interface IMongoSettings
    {
        string ConnectionString { get; }
        string Database { get; }
    }
}
