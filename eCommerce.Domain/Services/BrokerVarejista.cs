namespace eCommerce.Domain.Services
{
    public class BrokerVarejista : IDomainService
    {
        private readonly IBrokerVarejista _brokerVarejistaRepository;
        private readonly IBroker _brokerRepository;
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public BrokerVarejista(
            IBrokerVarejista brokerVarejistaRepository,
            IBroker brokerRepository,
            IVarejista varejistaRepository,
            IEventDispatcher eventDispatcher)
        {
            _brokerVarejistaRepository = brokerVarejistaRepository;
            _brokerRepository = brokerRepository;
            _varejistaRepository = varejistaRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<bool> ValidarVinculoExistenteAsync(int brokerId, int varejistaId)
        {
            return await _brokerVarejistaRepository.ExistsRelacionamentoAsync(brokerId, varejistaId);
        }

        public async Task<bool> ValidarEntidadesExistemAsync(int brokerId, int varejistaId)
        {
            var broker = await _brokerRepository.GetByIdAsync(brokerId);
            var varejista = await _varejistaRepository.GetByIdAsync(varejistaId);

            return broker != null && varejista != null;
        }
    }
} 