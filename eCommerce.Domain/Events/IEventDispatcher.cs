namespace eCommerce.Domain.Events
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
} 