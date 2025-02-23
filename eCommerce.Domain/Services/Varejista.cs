namespace eCommerce.Domain.Services
{
    public class Varejista : IDomainService
    {
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public Varejista(IVarejista varejistaRepository, IEventDispatcher eventDispatcher)
        {
            _varejistaRepository = varejistaRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<bool> ValidarCnpjUnicoAsync(string cnpj, int? ignorarId = null)
        {
            var varejista = await _varejistaRepository.GetByCnpjAsync(cnpj);
            return varejista == null || (ignorarId.HasValue && varejista.IdVarejista == ignorarId.Value);
        }
    }
} 