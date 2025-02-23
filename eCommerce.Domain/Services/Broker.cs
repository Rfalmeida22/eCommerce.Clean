namespace eCommerce.Domain.Services
{
    public class Broker : IDomainService
    {
        private readonly IBroker _brokerRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public Broker(IBroker brokerRepository, IEventDispatcher eventDispatcher)
        {
            _brokerRepository = brokerRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<bool> ValidarVinculoVarejistaAsync(int brokerId, int varejistaId)
        {
            var brokers = await _brokerRepository.GetByVarejistaIdAsync(varejistaId);
            return brokers.Any(b => b.IdBroker == brokerId);
        }
    }
} 