using eCommerce.Domain.Events;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.EventHandlers
{
    public class BrokerVarejista : IEventHandler<Events.BrokerVarejista>
    {
        private readonly ILogger<BrokerVarejista> _logger;

        public BrokerVarejista(ILogger<BrokerVarejista> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Events.BrokerVarejista @event)
        {
            _logger.LogInformation($"Broker {@event.BrokerId} vinculado ao Varejista {@event.VarejistaId} por {@event.UserName} em {@event.OccurredOn}");
            // Aqui poderia atualizar permissões, sincronizar dados, etc
        }
    }
} 