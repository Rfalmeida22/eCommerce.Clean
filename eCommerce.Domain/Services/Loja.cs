namespace eCommerce.Domain.Services
{
    public class Loja : IDomainService
    {
        private readonly ILoja _lojaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public Loja(ILoja lojaRepository, IEventDispatcher eventDispatcher)
        {
            _lojaRepository = lojaRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<bool> ValidarCnpjUnicoAsync(string cnpj)
        {
            var loja = await _lojaRepository.GetByCnpjAsync(cnpj);
            return loja == null;
        }

        public async Task<bool> ValidarCodigoUnicoAsync(string codigo)
        {
            var loja = await _lojaRepository.GetByCodigoAsync(codigo);
            return loja == null;
        }
    }
} 