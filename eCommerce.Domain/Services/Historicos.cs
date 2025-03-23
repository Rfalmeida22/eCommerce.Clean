using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a Historicos.
    /// </summary>
    public class Historicos : IDomainService
    {
        private readonly IHistoricos _historicosRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<Historicos> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe HistoricosService.
        /// </summary>
        /// <param name="historicosRepository">Repositório de Historicos.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <param name="logger">Logger para registro de logs.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public Historicos(IHistoricos historicosRepository, IEventDispatcher eventDispatcher, ILogger<Historicos> logger)
        {
            _historicosRepository = historicosRepository ?? throw new ArgumentNullException(nameof(historicosRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Publica um evento de criação ou atualização de histórico.
        /// </summary>
        /// <param name="historicosCod">ID do histórico.</param>
        /// <param name="acao">Ação realizada.</param>
        /// <param name="data">Data da ação.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public async Task PublicarEventoHistoricosAsync(int historicosCod, string acao, DateTime data, string userName)
        {
            var evento = new Events.Historicos(historicosCod, acao, data, userName);
            await _eventDispatcher.PublishAsync(evento);
        }
    }
}

