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
 * Servi�o de Dom�nio Broker (eCommerce.Domain/Services/Broker.cs)
 * 
 * Prop�sito:
 * Implementa a l�gica de neg�cios relacionada a opera��es com Brokers que n�o se encaixam
 * naturalmente em uma �nica entidade. Este servi�o atua como uma camada de coordena��o
 * entre diferentes partes do dom�nio.
 * 
 * Responsabilidades:
 * 1. Coordena��o de Opera��es:
 *    - Valida��o de v�nculos entre Brokers e Varejistas
 *    - Publica��o de eventos de dom�nio
 *    - Logging de opera��es importantes
 * 
 * 2. Depend�ncias:
 *    - IBroker: Reposit�rio para opera��es de persist�ncia
 *    - IEventDispatcher: Servi�o para publica��o de eventos
 *    - ILogger: Servi�o para logging
 * 
 * Exemplos de Uso:
 * 
 * 1. Inje��o do Servi�o (Startup.cs ou Program.cs):
 *    services.AddScoped<IBroker, Broker>();
 *    services.AddScoped<IEventDispatcher, EventDispatcher>();
 *    services.AddLogging();
 * 
 * 2. Valida��o de V�nculo Broker-Varejista:
 *    // Em um controller ou application service
 *    public class BrokerController 
 *    {
 *        private readonly Services.Broker _brokerService;
 *        
 *        public BrokerController(Services.Broker brokerService)
 *        {
 *            _brokerService = brokerService;
 *        }
 *        
 *        public async Task<IActionResult> ValidarVinculo(int brokerId, int varejistaId)
 *        {
 *            try 
 *            {
 *                var isValid = await _brokerService.ValidarVinculoVarejistaAsync(brokerId, varejistaId);
 *                return Ok(isValid);
 *            }
 *            catch (ArgumentException ex)
 *            {
 *                return BadRequest(ex.Message);
 *            }
 *        }
 *    }
 * 
 * Fluxo de Execu��o ValidarVinculoVarejistaAsync:
 * 1. Valida os par�metros de entrada (brokerId e varejistaId)
 * 2. Busca os brokers associados ao varejista
 * 3. Verifica se o broker espec�fico est� na lista
 * 4. Registra logs de informa��o ou warning baseado no resultado
 * 5. Publica um evento de dom�nio sobre a valida��o
 * 6. Retorna o resultado da valida��o
 * 
 * Tratamento de Erros:
 * - ArgumentException: Quando IDs s�o inv�lidos (? 0)
 * - ArgumentNullException: Quando depend�ncias s�o nulas
 * - Logging de erros com detalhes da opera��o
 * 
 * Eventos de Dom�nio:
 * - Publica Events.Broker ap�s valida��o de v�nculo
 * - Permite que outros componentes reajam a mudan�as
 * 
 * Boas Pr�ticas:
 * 1. Valida��o rigorosa de par�metros
 * 2. Logging adequado para troubleshooting
 * 3. Tratamento de exce��es com mensagens claras
 * 4. Uso de inje��o de depend�ncia
 * 5. Separa��o clara de responsabilidades
 * 
 * Observa��es:
 * - Servi�o stateless (n�o mant�m estado)
 * - Opera��es s�o ass�ncronas para melhor performance
 * - Segue princ�pios SOLID e DDD
 * - Facilita testes unit�rios atrav�s de depend�ncias injetadas
 */
