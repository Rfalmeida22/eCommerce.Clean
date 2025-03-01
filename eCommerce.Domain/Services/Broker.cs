using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Servi�o de dom�nio para opera��es relacionadas a Broker.
    /// </summary>
    public class Broker : IDomainService
    {
        private readonly IBroker _brokerRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<Broker> _logger;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe Broker.
        /// </summary>
        /// <param name="brokerRepository">Reposit�rio de Broker.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <param name="logger">Logger para registro de logs.</param>
        /// <exception cref="ArgumentNullException">Lan�ada quando qualquer depend�ncia � nula.</exception>
        public Broker(IBroker brokerRepository, IEventDispatcher eventDispatcher, ILogger<Broker> logger)
        {
            _brokerRepository = brokerRepository ?? throw new ArgumentNullException(nameof(brokerRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Valida se um Broker est� vinculado a um Varejista espec�fico.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se o Broker est� vinculado ao Varejista, caso contr�rio False.</returns>
        /// <exception cref="ArgumentException">Lan�ada quando brokerId ou varejistaId s�o inv�lidos.</exception>
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

                // Publicar evento de dom�nio
                var evento = new Events.Broker(brokerId, "Nome do Broker", "Nome do Usu�rio");
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
 * Um DomainService � uma classe que encapsula a l�gica de neg�cios que n�o pertence a uma entidade ou valor espec�fico.
 * Ele � usado para opera��es que envolvem m�ltiplas entidades ou que n�o se encaixam bem em uma �nica entidade.
 * 
 * No contexto deste projeto, a classe Broker como DomainService � respons�vel por opera��es relacionadas a Brokers,
 * como validar v�nculos entre Brokers e Varejistas. Ela utiliza reposit�rios para acessar dados e despachantes de eventos
 * para publicar eventos de dom�nio quando necess�rio.
 */