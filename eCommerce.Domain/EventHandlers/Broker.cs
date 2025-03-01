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
            _logger.LogInformation("Broker atualizado: {BrokerId}, Nome: {Nome}", @event.BrokerId, @event.Nome);
            // Aqui poderia criar acessos, notificar sistemas, etc
        }
    }
   
} 