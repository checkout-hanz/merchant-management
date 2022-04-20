using Merchant.Management.Services;
using Merchant.Management.Utils;

namespace Merchant.Management.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMerchantService, MerchantService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
