using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Servi�o de dom�nio para opera��es relacionadas ao relacionamento entre Broker e Varejista.
    /// </summary>
    public class BrokerVarejista : IDomainService
    {
        private readonly IBrokerVarejista _brokerVarejistaRepository;
        private readonly IBroker _brokerRepository;
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe BrokerVarejista.
        /// </summary>
        /// <param name="brokerVarejistaRepository">Reposit�rio de relacionamento entre Broker e Varejista.</param>
        /// <param name="brokerRepository">Reposit�rio de Broker.</param>
        /// <param name="varejistaRepository">Reposit�rio de Varejista.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lan�ada quando qualquer depend�ncia � nula.</exception>
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
        /// Valida se j� existe um relacionamento entre um Broker e um Varejista espec�fico.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se o relacionamento j� existe, caso contr�rio False.</returns>
        public async Task<bool> ValidarVinculoExistenteAsync(int brokerId, int varejistaId)
        {
            return await _brokerVarejistaRepository.ExistsRelacionamentoAsync(brokerId, varejistaId);
        }

        /// <summary>
        /// Valida se as entidades Broker e Varejista existem.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se ambas as entidades existem, caso contr�rio False.</returns>
        public async Task<bool> ValidarEntidadesExistemAsync(int brokerId, int varejistaId)
        {
            var broker = await _brokerRepository.GetByIdAsync(brokerId);
            var varejista = await _varejistaRepository.GetByIdAsync(varejistaId);

            return broker != null && varejista != null;
        }
    }
} 