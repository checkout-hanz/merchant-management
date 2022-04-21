namespace Merchant.Management.Messaging.Publisher.Events
{
    public class MerchantCreatedEvent : IEvent
    {
        public MerchantCreatedEvent()
        {
            EventName = nameof(MerchantCreatedEvent);
        }

        public Guid MerchantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EventName { get; set; }
    }
}
