using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Servi�o de dom�nio para opera��es relacionadas � importa��o de dados de Varejo.
    /// </summary>
    public class ImportacaoVarejo : IDomainService
    {
        private readonly ILoja _lojaRepository;
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe ImportacaoVarejo.
        /// </summary>
        /// <param name="lojaRepository">Reposit�rio de Loja.</param>
        /// <param name="varejistaRepository">Reposit�rio de Varejista.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lan�ada quando qualquer depend�ncia � nula.</exception>
        public ImportacaoVarejo(
            ILoja lojaRepository,
            IVarejista varejistaRepository,
            IEventDispatcher eventDispatcher)
        {
            _lojaRepository = lojaRepository ?? throw new ArgumentNullException(nameof(lojaRepository));
            _varejistaRepository = varejistaRepository ?? throw new ArgumentNullException(nameof(varejistaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        /// <summary>
        /// Valida se os dados de importa��o s�o v�lidos.
        /// </summary>
        /// <param name="cnpjLoja">CNPJ da Loja.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se os dados de importa��o s�o v�lidos, caso contr�rio False.</returns>
        public async Task<bool> ValidarDadosImportacaoAsync(string cnpjLoja, int varejistaId)
        {
            var varejista = await _varejistaRepository.GetByIdAsync(varejistaId);
            if (varejista == null) return false;

            var loja = await _lojaRepository.GetByCnpjAsync(cnpjLoja);
            if (loja == null) return false;

            return loja.IdVarejista == varejistaId;
        }

        /// <summary>
        /// Valida se a loja tem permiss�o para importa��o.
        /// </summary>
        /// <param name="lojaId">ID da Loja.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se a loja tem permiss�o para importa��o, caso contr�rio False.</returns>
        public async Task<bool> ValidarPermissaoImportacaoAsync(int lojaId, int varejistaId)
        {
            var lojas = await _lojaRepository.GetByVarejistaIdAsync(varejistaId);
            return lojas.Any(l => l.IdLoja == lojaId);
        }
    }
} 