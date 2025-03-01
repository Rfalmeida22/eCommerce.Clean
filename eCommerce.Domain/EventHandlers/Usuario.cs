using eCommerce.Domain.Events;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.EventHandlers
{
    public class Usuario : 
        IEventHandler<Events.Usuario>
    {
        private readonly ILogger<Usuario> _logger;

        public Usuario(ILogger<Usuario> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Events.Usuario @event)
        {
            _logger.LogInformation($"Usuário criado: {@event.Nome} ({@event.Email}) por {@event.UserName} em {@event.OccurredOn}");
            // Aqui poderia enviar email de boas vindas, notificar outros sistemas, etc
        }

    }
} 