using eCommerce.Domain.Events;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Entities;
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
        /// Cadastra um novo Broker no sistema.
        /// </summary>
        /// <param name="nome">Nome do Broker.</param>
        /// <param name="createdBy">Usuário que está criando o Broker.</param>
        /// <returns>Broker cadastrado.</returns>
        public async Task<Entities.Broker> CadasatrarBrokerAsync(string nome, string createdBy)
        {
            try 
            {
                // 1. Verifica se já existe broker com mesmo nome
                if (await _brokerRepository.ExistsByNomeAsync(nome))
                {
                    _logger.LogWarning("Broker {Nome} já existe", nome);
                    throw new DomainException($"Já existe um Broker com o nome {nome}");
                }

                // 2. Cria uma nova instância de Broker
                var broker = Entities.Broker.Create(nome, createdBy);

                // 3. Salva no banco de dados
                await _brokerRepository.AddAsync(broker);

                // 4. Registra log de sucesso
                _logger.LogInformation("Broker {Nome} cadastrado com sucesso. Id: {Id}", nome, broker.IdBroker);

                return broker;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar Broker {Nome}", nome);
                throw;
            }
        }

        /// <summary>
        /// Atualiza os dados de um Broker existente.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="novoNome">Novo nome do Broker.</param>
        /// <param name="updatedBy">Usuário que está atualizando o Broker.</param>
        /// <returns>Broker atualizado.</returns>
        public async Task<Entities.Broker> AtualizarBrokerAsync(int brokerId, string novoNome, string updatedby)
        {
            try
            {
                // 1. Verifica se o broker existe
                var broker = await _brokerRepository.GetByIdAsync(brokerId);
                if (broker == null)
                {
                    _logger.LogWarning("Broker {BrokerId} não encontrado", brokerId);
                    throw new DomainException($"Broker {brokerId} não encontrado");
                }

                // 2. Verifica se já existe outro broker com o mesmo nome
                if (await _brokerRepository.ExistsByNomeAsync(novoNome, brokerId))
                {
                    _logger.LogWarning("Já existe um Broker com o nome {Nome}", novoNome);
                    throw new DomainException($"Já existe um Broker com o nome {novoNome}");
                }

                // 3. Atualiza os dados
                broker.AtualizarDados(novoNome, updatedby);

                // 4. Salva no banco de dados
                await _brokerRepository.UpdateAsync(broker);

                // 5. Registra log de sucesso
                _logger.LogInformation("Broker {BrokerId} atualizado com sucesso. Novo nome: {NovoNome}", brokerId, novoNome);

                return broker;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar Broker {BrokerId}", brokerId);
                throw;
            }
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
 * Serviço de Domínio Broker (eCommerce.Domain/Services/Broker.cs)
 * 
 * Propósito:
 * Implementa a lógica de negócios relacionada a operações com Brokers que não se encaixam
 * naturalmente em uma única entidade. Este serviço atua como uma camada de coordenação
 * entre diferentes partes do domínio.
 * 
 * Responsabilidades:
 * 1. Coordenação de Operações:
 *    - Validação de vínculos entre Brokers e Varejistas
 *    - Publicação de eventos de domínio
 *    - Logging de operações importantes
 * 
 * 2. Dependências:
 *    - IBroker: Repositório para operações de persistência
 *    - IEventDispatcher: Serviço para publicação de eventos
 *    - ILogger: Serviço para logging
 * 
 * Exemplos de Uso:
 * 
 * 1. Injeção do Serviço (Startup.cs ou Program.cs):
 *    services.AddScoped<IBroker, Broker>();
 *    services.AddScoped<IEventDispatcher, EventDispatcher>();
 *    services.AddLogging();
 * 
 * 2. Validação de Vínculo Broker-Varejista:
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
 * Fluxo de Execução ValidarVinculoVarejistaAsync:
 * 1. Valida os parâmetros de entrada (brokerId e varejistaId)
 * 2. Busca os brokers associados ao varejista
 * 3. Verifica se o broker específico está na lista
 * 4. Registra logs de informação ou warning baseado no resultado
 * 5. Publica um evento de domínio sobre a validação
 * 6. Retorna o resultado da validação
 * 
 * Tratamento de Erros:
 * - ArgumentException: Quando IDs são inválidos (? 0)
 * - ArgumentNullException: Quando dependências são nulas
 * - Logging de erros com detalhes da operação
 * 
 * Eventos de Domínio:
 * - Publica Events.Broker após validação de vínculo
 * - Permite que outros componentes reajam a mudanças
 * 
 * Boas Práticas:
 * 1. Validação rigorosa de parâmetros
 * 2. Logging adequado para troubleshooting
 * 3. Tratamento de exceções com mensagens claras
 * 4. Uso de injeção de dependência
 * 5. Separação clara de responsabilidades
 * 
 * Observações:
 * - Serviço stateless (não mantém estado)
 * - Operações são assíncronas para melhor performance
 * - Segue princípios SOLID e DDD
 * - Facilita testes unitários através de dependências injetadas
 */
