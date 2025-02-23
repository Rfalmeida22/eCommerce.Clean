using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas ao relacionamento entre Broker e Varejista.
    /// </summary>
    public class BrokerVarejista : IDomainService
    {
        private readonly IBrokerVarejista _brokerVarejistaRepository;
        private readonly IBroker _brokerRepository;
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Inicializa uma nova instância da classe BrokerVarejista.
        /// </summary>
        /// <param name="brokerVarejistaRepository">Repositório de relacionamento entre Broker e Varejista.</param>
        /// <param name="brokerRepository">Repositório de Broker.</param>
        /// <param name="varejistaRepository">Repositório de Varejista.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public BrokerVarejista(
            IBrokerVarejista brokerVarejistaRepository,
            IBroker brokerRepository,
            IVarejista varejistaRepository,
            IEventDispatcher eventDispatcher)
        {
            _brokerVarejistaRepository = brokerVarejistaRepository ?? throw new ArgumentNullException(nameof(brokerVarejistaRepository));
            _brokerRepository = brokerRepository ?? throw new ArgumentNullException(nameof(brokerRepository));
            _varejistaRepository = varejistaRepository ?? throw new ArgumentNullException(nameof(varejistaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        /// <summary>
        /// Valida se já existe um relacionamento entre um Broker e um Varejista específico.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se o relacionamento já existe, caso contrário False.</returns>
        public async Task<bool> ValidarVinculoExistenteAsync(int brokerId, int varejistaId)
        {
            return await _brokerVarejistaRepository.ExistsRelacionamentoAsync(brokerId, varejistaId);
        }

        /// <summary>
        /// Valida se as entidades Broker e Varejista existem.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se ambas as entidades existem, caso contrário False.</returns>
        public async Task<bool> ValidarEntidadesExistemAsync(int brokerId, int varejistaId)
        {
            var broker = await _brokerRepository.GetByIdAsync(brokerId);
            var varejista = await _varejistaRepository.GetByIdAsync(varejistaId);

            return broker != null && varejista != null;
        }
    }
} 