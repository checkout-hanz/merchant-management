using Merchant.Management.Messaging.Publisher;
using Merchant.Management.Messaging.Publisher.Events;
using Merchant.Management.Messaging.RabbitMq;
using Merchant.Management.Messaging.Subscription;

namespace Merchant.Management.Configuration
{
    public static class MessagingConfiguration
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPublisher<MerchantCreatedEvent>, Publisher<MerchantCreatedEvent>>();

            var host = configuration["RabbitMQHost"];
            var port = int.Parse(configuration["RabbitMQPort"]);
            services.AddSingleton<IRabbitMQConfig>(_ => new RabbitMQConfig(host, port));
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();//(_ => new MessageBusClient(host, port));
            services.AddHostedService<MessageBusSubscriber>();
            //services.AddSingleton<ISubscriptionHandler<MerchantCreatedEvent>, MerchantCreatedHandler>();
            return services;
        }
    }
}
