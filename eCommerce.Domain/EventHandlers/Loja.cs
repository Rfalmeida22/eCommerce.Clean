using eCommerce.Domain.Events;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.EventHandlers
{
    public class Loja : IEventHandler<Events.Loja>
    {
        private readonly ILogger<Loja> _logger;

        public Loja(ILogger<Loja> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Events.Loja @event)
        {
            _logger.LogInformation($"Loja criada: {@event.Nome} (CNPJ: {@event.Cnpj}) do Varejista {@event.VarejistaId} por {@event.UserName} em {@event.OccurredOn}");
            // Aqui poderia criar estruturas iniciais, notificar sistemas, etc
        }
    }
} 