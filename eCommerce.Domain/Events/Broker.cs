namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada a um Broker.
    /// </summary>
    public class Broker : Event
    {
        /// <summary>
        /// Identificador único do Broker.
        /// </summary>
        public int BrokerId { get; }

        /// <summary>
        /// Nome do Broker.
        /// </summary>
        public string Nome { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento Broker.
        /// </summary>
        /// <param name="brokerId">Identificador único do Broker.</param>
        /// <param name="nome">Nome do Broker.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public Broker(int brokerId, string nome, string userName)
            : base(userName)
        {
            BrokerId = brokerId;
            Nome = nome;
        }
    }
}

/*
 * Sobre Eventos de Domínio:
 * 
 * Eventos de domínio são usados para notificar outras partes do sistema sobre mudanças significativas no estado do domínio.
 * Eles ajudam a manter o sistema desacoplado e reativo, permitindo que diferentes partes do sistema se comuniquem de forma assíncrona.
 * 
 * A classe Broker representa um evento de domínio específico relacionado a um Broker. Quando uma ação significativa ocorre,
 * como a criação ou atualização de um Broker, um evento Broker pode ser publicado para notificar outras partes do sistema.
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


/*
 * Fluxo de Publicação e Manipulação de Eventos:
 * 
 * 1. Publicação do Evento:
 *    - No método `ValidarVinculoVarejistaAsync` da classe `Broker` (em `Services/Broker.cs`), um evento `Broker` é criado e publicado usando `_eventDispatcher.PublishAsync(evento)`.
 *    - O evento contém informações relevantes, como `brokerId`, `Nome do Broker`, e `Nome do Usuário`.
 * 
 * 2. Despachante de Eventos (Event Dispatcher):
 *    - A interface `IEventDispatcher` define o método `PublishAsync`, que é responsável por publicar o evento.
 *    - A implementação do `IEventDispatcher` (por exemplo, `EventDispatcher`) distribui o evento para os manipuladores de eventos registrados.
 * 
 * 3. Manipuladores de Eventos (Event Handlers):
 *    - Os manipuladores de eventos implementam a interface `IEventHandler<TEvent>`, onde `TEvent` é o tipo do evento.
 *    - No arquivo `EventHandlers/Broker.cs`, a classe `Broker` implementa `IEventHandler<Events.Broker>` e define o método `HandleAsync` para tratar o evento.
 *    - Quando o evento é publicado, o `EventDispatcher` chama o método `HandleAsync` de todos os manipuladores registrados para o tipo de evento.
 *    - No método `HandleAsync`, a lógica necessária para tratar o evento é executada, como registrar logs, notificar outros sistemas, etc.
 * 
 * Exemplo de Publicação de Evento:
 * 
 * public async Task<bool> ValidarVinculoVarejistaAsync(int brokerId, int varejistaId)
 * {
 *     // Lógica de validação...
 * 
 *     // Publicar evento de domínio
 *     var evento = new Events.Broker(brokerId, "Nome do Broker", "Nome do Usuário");
 *     await _eventDispatcher.PublishAsync(evento);
 * 
 *     return isValid;
 * }
 * 
 * Exemplo de Manipulação de Evento:
 * 
 * public class BrokerEventHandler : IEventHandler<Events.Broker>
 * {
 *     private readonly ILogger<BrokerEventHandler> _logger;
 * 
 *     public BrokerEventHandler(ILogger<BrokerEventHandler> logger)
 *     {
 *         _logger = logger;
 *     }
 * 
 *     public async Task HandleAsync(Events.Broker @event)
 *     {
 *         // Lógica para manipular o evento
 *         _logger.LogInformation("Broker atualizado: {BrokerId}, Nome: {Nome}", @event.BrokerId, @event.Nome);
 *         // Aqui poderia criar acessos, notificar sistemas, etc
 *     }
 * }
 * 
 * Este fluxo garante que o sistema reaja de forma assíncrona a eventos de domínio, mantendo o sistema desacoplado e reativo.
 */