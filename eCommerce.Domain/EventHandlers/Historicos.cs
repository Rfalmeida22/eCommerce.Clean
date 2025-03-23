using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.EventHandlers
{
    public class Historicos : IEventHandler<Events.Historicos>
    {

        private readonly ILogger<Historicos> _logger;

        public Historicos(ILogger<Historicos> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(Events.Historicos @event)
        {
            _logger.LogInformation("Históricos atualizado, Ação: {Acao}, Nome: {UserName}, Em: {OccurredOn}", @event.Acao, @event.UserName, @event.OccurredOn);
            // Aqui poderia criar acessos, notificar sistemas, etc
        }
    }
}
