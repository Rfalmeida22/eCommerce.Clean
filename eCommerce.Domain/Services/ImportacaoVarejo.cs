namespace eCommerce.Domain.Services
{
    public class ImportacaoVarejo : IDomainService
    {
        private readonly ILoja _lojaRepository;
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public ImportacaoVarejo(
            ILoja lojaRepository,
            IVarejista varejistaRepository,
            IEventDispatcher eventDispatcher)
        {
            _lojaRepository = lojaRepository;
            _varejistaRepository = varejistaRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<bool> ValidarDadosImportacaoAsync(string cnpjLoja, int varejistaId)
        {
            var varejista = await _varejistaRepository.GetByIdAsync(varejistaId);
            if (varejista == null) return false;

            var loja = await _lojaRepository.GetByCnpjAsync(cnpjLoja);
            if (loja == null) return false;

            return loja.IdVarejista == varejistaId;
        }

        public async Task<bool> ValidarPermissaoImportacaoAsync(int lojaId, int varejistaId)
        {
            var lojas = await _lojaRepository.GetByVarejistaIdAsync(varejistaId);
            return lojas.Any(l => l.IdLoja == lojaId);
        }
    }
} 