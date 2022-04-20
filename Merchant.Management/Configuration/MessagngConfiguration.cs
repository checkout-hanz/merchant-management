using Merchant.Management.Messaging.Publisher;
using Merchant.Management.Messaging.Publisher.Events;

namespace Merchant.Management.Configuration
{
    public static class MessagingConfiguration
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IPublisher<MerchantCreatedEvent>, Publisher<MerchantCreatedEvent>>();
            return services;
        }
    }
}
