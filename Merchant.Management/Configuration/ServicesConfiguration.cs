using FluentValidation;
using Merchant.Management.Models;
using Merchant.Management.Services;
using Merchant.Management.Utils;
using Merchant.Management.Validators;

namespace Merchant.Management.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMerchantService, MerchantService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IValidator<CreateMerchant>, CreateMerchantValidator>();
            return services;
        }
    }
}
