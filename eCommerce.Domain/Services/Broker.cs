using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a Broker.
    /// </summary>
    public class Broker : IDomainService
    {
        private readonly IBroker _brokerRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<Broker> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe Broker.
        /// </summary>
        /// <param name="brokerRepository">Repositório de Broker.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <param name="logger">Logger para registro de logs.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public Broker(IBroker brokerRepository, IEventDispatcher eventDispatcher, ILogger<Broker> logger)
        {
            _brokerRepository = brokerRepository ?? throw new ArgumentNullException(nameof(brokerRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Valida se um Broker está vinculado a um Varejista específico.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se o Broker está vinculado ao Varejista, caso contrário False.</returns>
        /// <exception cref="ArgumentException">Lançada quando brokerId ou varejistaId são inválidos.</exception>
        public async Task<bool> ValidarVinculoVarejistaAsync(int brokerId, int varejistaId)
        {
            if (brokerId <= 0)
            {
                _logger.LogError("Invalid brokerId: {BrokerId}", brokerId);
                throw new ArgumentException("Broker ID must be greater than zero.", nameof(brokerId));
            }

            if (varejistaId <= 0)
            {
                _logger.LogError("Invalid varejistaId: {VarejistaId}", varejistaId);
                throw new ArgumentException("Varejista ID must be greater than zero.", nameof(varejistaId));
            }

            try
            {
                var brokers = await _brokerRepository.GetByVarejistaIdAsync(varejistaId);
                var isValid = brokers.Any(b => b.IdBroker == brokerId);

                if (isValid)
                {
                    _logger.LogInformation("Broker {BrokerId} is linked to Varejista {VarejistaId}", brokerId, varejistaId);
                }
                else
                {
                    _logger.LogWarning("Broker {BrokerId} is not linked to Varejista {VarejistaId}", brokerId, varejistaId);
                }

                // Publicar evento de domínio
                var evento = new Events.Broker(brokerId, "Nome do Broker", "Nome do Usuário");
                await _eventDispatcher.PublishAsync(evento);

                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating broker-varejista link for BrokerId: {BrokerId}, VarejistaId: {VarejistaId}", brokerId, varejistaId);
                throw;
            }
        }
    }
}

/*
 * Sobre DomainService:
 * 
 * Um DomainService é uma classe que encapsula a lógica de negócios que não pertence a uma entidade ou valor específico.
 * Ele é usado para operações que envolvem múltiplas entidades ou que não se encaixam bem em uma única entidade.
 * 
 * No contexto deste projeto, a classe Broker como DomainService é responsável por operações relacionadas a Brokers,
 * como validar vínculos entre Brokers e Varejistas. Ela utiliza repositórios para acessar dados e despachantes de eventos
 * para publicar eventos de domínio quando necessário.
 */