using eCommerce.Domain.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Despachante de eventos respons�vel por publicar eventos de dom�nio e notificar os manipuladores de eventos registrados.
    /// </summary>
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe EventDispatcher.
        /// </summary>
        /// <param name="serviceProvider">Provedor de servi�os para resolver depend�ncias.</param>
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Publica um evento de dom�nio e notifica os manipuladores de eventos registrados.
        /// </summary>
        /// <typeparam name="TEvent">Tipo do evento de dom�nio.</typeparam>
        /// <param name="event">Inst�ncia do evento de dom�nio.</param>
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}

/*
 * Sobre EventDispatcher:
 * 
 * A classe EventDispatcher � uma implementa��o de IEventDispatcher que utiliza o padr�o de inje��o de depend�ncia para resolver
 * e notificar os manipuladores de eventos registrados. Quando um evento de dom�nio � publicado, o EventDispatcher cria um escopo
 * de servi�o para obter todos os manipuladores de eventos registrados para o tipo de evento espec�fico e chama o m�todo HandleAsync
 * em cada manipulador para processar o evento.
 * 
 * Exemplo de Publica��o de Evento:
 * 
 * public async Task AtualizarBrokerAsync(int brokerId, string novoNome, string userName)
 * {
 *     // L�gica para atualizar o Broker
 *     // ...
 * 
 *     // Publicar evento de dom�nio
 *     var evento = new Broker(brokerId, novoNome, userName);
 *     await _eventDispatcher.PublishAsync(evento);
 * }
 * 
 * Exemplo de Manipula��o de Evento:
 * 
 * public class BrokerEventHandler : IEventHandler<Broker>
 * {
 *     private readonly ILogger<BrokerEventHandler> _logger;
 * 
 *     public BrokerEventHandler(ILogger<BrokerEventHandler> logger)
 *     {
 *         _logger = logger;
 *     }
 * 
 *     public async Task HandleAsync(Broker @event)
 *     {
 *         // L�gica para manipular o evento
 *         _logger.LogInformation("Broker atualizado: {BrokerId}, Nome: {Nome}", @event.BrokerId, @event.Nome);
 *         // ...
 *     }
 * }
 */