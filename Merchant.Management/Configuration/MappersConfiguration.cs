using System.Reflection;

namespace Merchant.Management.Configuration
{
    public static class MappersConfiguration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
