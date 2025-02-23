using eCommerce.Domain.Events;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.EventHandlers
{
    public class Broker : IEventHandler<Events.Broker>
    {
        private readonly ILogger<Broker> _logger;

        public Broker(ILogger<Broker> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Events.Broker @event)
        {
            _logger.LogInformation($"Broker criado: {event.Nome} (ID: {event.BrokerId}) por {event.UserName} em {event.OccurredOn}");
            // Aqui poderia criar acessos, notificar sistemas, etc
        }
    }
} 