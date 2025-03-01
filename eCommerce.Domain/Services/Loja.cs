using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Servi�o de dom�nio para opera��es relacionadas a Loja.
    /// </summary>
    public class Loja : IDomainService
    {
        private readonly ILoja _lojaRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<Loja> _logger;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe Loja.
        /// </summary>
        /// <param name="lojaRepository">Reposit�rio de Loja.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lan�ada quando qualquer depend�ncia � nula.</exception>
        public Loja(ILoja lojaRepository, IEventDispatcher eventDispatcher, ILogger<Loja> logger)
        {
            _lojaRepository = lojaRepository ?? throw new ArgumentNullException(nameof(lojaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        /// <summary>
        /// Valida se uma Loja est� vinculada a um Varejista espec�fico.
        /// </summary>
        /// <param name="lojaId">ID da Loja.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se a Loja est� vinculada ao Varejista, caso contr�rio False.</returns>
        /// <exception cref="ArgumentException">Lan�ada quando lojaId ou varejistaId s�o inv�lidos.</exception>
        public async Task<bool> ValidarVinculoVarejistaAsync(int lojaId, int varejistaId)
        {
            if (lojaId <= 0)
            {
                _logger.LogError("Invalid lojaId: {LojaId}", lojaId);
                throw new ArgumentException("Loja ID must be greater than zero.", nameof(lojaId));
            }

            if (varejistaId <= 0)
            {
                _logger.LogError("Invalid varejistaId: {VarejistaId}", varejistaId);
                throw new ArgumentException("Varejista ID must be greater than zero.", nameof(varejistaId));
            }

            try
            {
                var lojas = await _lojaRepository.GetByVarejistaIdAsync(varejistaId);
                var isValid = lojas.Any(l => l.IdLoja == lojaId);

                if (isValid)
                {
                    _logger.LogInformation("Loja {LojaId} is linked to Varejista {VarejistaId}", lojaId, varejistaId);
                }
                else
                {
                    _logger.LogWarning("Loja {LojaId} is not linked to Varejista {VarejistaId}", lojaId, varejistaId);
                }

                // Publicar evento de dom�nio
                var evento = new Events.Loja(lojaId, "Nome da Loja", "CNPJ da Loja", varejistaId, "Nome do Usu�rio");
                await _eventDispatcher.PublishAsync(evento);

                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating loja-varejista link for LojaId: {LojaId}, VarejistaId: {VarejistaId}", lojaId, varejistaId);
                throw;
            }
        }
    }
} 