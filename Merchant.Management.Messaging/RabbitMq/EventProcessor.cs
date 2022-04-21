using System.Text.Json;
using Merchant.Management.Messaging.Publisher;
using Merchant.Management.Messaging.Publisher.Events;
using Merchant.Management.Messaging.Subscription;
using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Management.Messaging.RabbitMq
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void ProcessEvent(string message)
        {
            var eventType = JsonSerializer.Deserialize<GenericEvent>(message);

            var @event = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                from type in asm.GetTypes()
                where type.IsClass && type.Name == eventType.EventName && typeof(IMessage).IsAssignableFrom(type)
                select type).FirstOrDefault();

            if (@event == null)
            {
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var eventData = JsonSerializer.Deserialize(message, @event);
                
            //switch (eventType?.EventName)
            //{
            //    case nameof(MerchantCreatedEvent):
            //        GetHandler(scope.ServiceProvider, eventData as MerchantCreatedEvent);
            //        break;
            //}
        }

        private static void GetHandler<T>(IServiceProvider sp, T content) where T : IMessage?
        {
            var handler = sp.GetRequiredService<ISubscriptionHandler<T>>();
            handler.Handle(content);
        }
    }
}
