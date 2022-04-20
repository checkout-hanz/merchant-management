namespace Merchant.Management.Messaging.Publisher
{
    public class Publisher<T> : IPublisher<T> where T : IEvent
    {
        public Task PublishAsync(T eventToPublish)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
