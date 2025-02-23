using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Servi�o de dom�nio para opera��es relacionadas a Loja.
    /// </summary>
    public class Loja : IDomainService
    {
        private readonly ILoja _lojaRepository;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe Loja.
        /// </summary>
        /// <param name="lojaRepository">Reposit�rio de Loja.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lan�ada quando qualquer depend�ncia � nula.</exception>
        public Loja(ILoja lojaRepository, IEventDispatcher eventDispatcher)
        {
            _lojaRepository = lojaRepository ?? throw new ArgumentNullException(nameof(lojaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        /// <summary>
        /// Valida se o CNPJ da loja � �nico.
        /// </summary>
        /// <param name="cnpj">CNPJ da loja.</param>
        /// <returns>True se o CNPJ � �nico, caso contr�rio False.</returns>
        public async Task<bool> ValidarCnpjUnicoAsync(string cnpj)
        {
            var loja = await _lojaRepository.GetByCnpjAsync(cnpj);
            return loja == null;
        }

        /// <summary>
        /// Valida se o c�digo da loja � �nico.
        /// </summary>
        /// <param name="codigo">C�digo da loja.</param>
        /// <returns>True se o c�digo � �nico, caso contr�rio False.</returns>
        public async Task<bool> ValidarCodigoUnicoAsync(string codigo)
        {
            var loja = await _lojaRepository.GetByCodigoAsync(codigo);
            return loja == null;
        }
    }
} 