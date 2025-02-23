using eCommerce.Domain.Events;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.EventHandlers
{
    public class Varejista : IEventHandler<Events.Varejista>
    {
        private readonly ILogger<Varejista> _logger;

        public Varejista(ILogger<Varejista> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Events.Varejista @event)
        {
            _logger.LogInformation($"Varejista criado: {event.Nome} (CNPJ: {event.Cnpj}) por {event.UserName} em {event.OccurredOn}");
            // Aqui poderia criar estruturas iniciais, notificar sistemas, etc
        }
    }
} 