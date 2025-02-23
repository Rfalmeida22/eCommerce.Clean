using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a Varejista.
    /// </summary>
    public class Varejista : IDomainService
    {
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Inicializa uma nova instância da classe Varejista.
        /// </summary>
        /// <param name="varejistaRepository">Repositório de Varejista.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public Varejista(IVarejista varejistaRepository, IEventDispatcher eventDispatcher)
        {
            _varejistaRepository = varejistaRepository ?? throw new ArgumentNullException(nameof(varejistaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        /// <summary>
        /// Valida se o CNPJ do varejista é único.
        /// </summary>
        /// <param name="cnpj">CNPJ do varejista.</param>
        /// <param name="ignorarId">ID do varejista a ser ignorado na validação (opcional).</param>
        /// <returns>True se o CNPJ é único, caso contrário False.</returns>
        public async Task<bool> ValidarCnpjUnicoAsync(string cnpj, int? ignorarId = null)
        {
            var varejista = await _varejistaRepository.GetByCnpjAsync(cnpj);
            return varejista == null || (ignorarId.HasValue && varejista.IdVarejista == ignorarId.Value);
        }
    }
} 