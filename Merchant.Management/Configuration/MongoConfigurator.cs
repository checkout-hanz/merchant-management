using Merchant.Management.MongoDb;
using Merchant.Management.MongoDb.Configuration;
using Merchant.Management.MongoDb.Repositories;
using MongoDB.Driver;

namespace Merchant.Management.Configuration
{
    public static class MongoConfigurator
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            var decodedConnectionString = configuration["MongoDb:EncodedConnectionString"];
            var databaseName = configuration["MongoDb:DatabaseName"];
            var mongoDbSettings = new MongoSettings(databaseName, hostEnvironment.EnvironmentName, decodedConnectionString);

            services.AddSingleton<IMongoSettings>(mongoDbSettings);
            services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));
            services.AddSingleton<IMongoContext, MongoContext>();

            services.AddSingleton<IMerchantRepository, MerchantRepository>();

            return services;
        }
    }
}
