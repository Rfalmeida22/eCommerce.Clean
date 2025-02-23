namespace eCommerce.Domain.EventHandlers
{
    public interface IEventHandler<in TEvent> where TEvent : Events.Event
    {
        Task HandleAsync(TEvent @event);
    }
} 