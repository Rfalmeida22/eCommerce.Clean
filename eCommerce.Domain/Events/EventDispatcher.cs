using eCommerce.Domain.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Despachante de eventos responsável por publicar eventos de domínio e notificar os manipuladores de eventos registrados.
    /// </summary>
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Inicializa uma nova instância da classe EventDispatcher.
        /// </summary>
        /// <param name="serviceProvider">Provedor de serviços para resolver dependências.</param>
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Publica um evento de domínio e notifica os manipuladores de eventos registrados.
        /// </summary>
        /// <typeparam name="TEvent">Tipo do evento de domínio.</typeparam>
        /// <param name="event">Instância do evento de domínio.</param>
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
 * A classe EventDispatcher é uma implementação de IEventDispatcher que utiliza o padrão de injeção de dependência para resolver
 * e notificar os manipuladores de eventos registrados. Quando um evento de domínio é publicado, o EventDispatcher cria um escopo
 * de serviço para obter todos os manipuladores de eventos registrados para o tipo de evento específico e chama o método HandleAsync
 * em cada manipulador para processar o evento.
 * 
 * Exemplo de Publicação de Evento:
 * 
 * public async Task AtualizarBrokerAsync(int brokerId, string novoNome, string userName)
 * {
 *     // Lógica para atualizar o Broker
 *     // ...
 * 
 *     // Publicar evento de domínio
 *     var evento = new Broker(brokerId, novoNome, userName);
 *     await _eventDispatcher.PublishAsync(evento);
 * }
 * 
 * Exemplo de Manipulação de Evento:
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
 *         // Lógica para manipular o evento
 *         _logger.LogInformation("Broker atualizado: {BrokerId}, Nome: {Nome}", @event.BrokerId, @event.Nome);
 *         // ...
 *     }
 * }
 */