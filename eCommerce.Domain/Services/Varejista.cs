using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a Varejista.
    /// </summary>
    public class Varejista : IDomainService
    {
        private readonly IVarejista _varejistaRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<Varejista> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe Varejista.
        /// </summary>
        /// <param name="varejistaRepository">Repositório de Varejista.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public Varejista(IVarejista varejistaRepository, IEventDispatcher eventDispatcher, ILogger<Varejista> logger)
        {
            _varejistaRepository = varejistaRepository ?? throw new ArgumentNullException(nameof(varejistaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Cadastra um novo Varejista no sistema.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="cdBanner"></param>
        /// <param name="cdCorFundo"></param>
        /// <param name="cdVarejista"></param>
        /// <param name="nmVarejista"></param>
        /// <param name="txLinkSite"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        public async Task<Entities.Varejista> CadastrarVarejistaAsync(string cnpj, string cdBanner, string cdCorFundo, string cdVarejista, string nmVarejista, string txLinkSite, string createdBy)
        {
            try
            {
                // 1. Verifica se já existe varejista com mesmo CNPJ
                if (!await ValidarCnpjUnicoAsync(cnpj))
                {
                    throw new DomainException($"Já existe um varejista com o CNPJ {cnpj}");
                }

                // 2. Cria uma nova instância de Varejista
                var varejista = Entities.Varejista.Create(cnpj, cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite, createdBy);
                
                // 3. Salva no banco de dados
                await _varejistaRepository.AddAsync(varejista);
                
                // 4. Registra log de sucesso
                _logger.LogInformation("Varejista {Nome} cadastrado com sucesso. Id: {Id}", nmVarejista, varejista.IdVarejista);
                
                return varejista;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar varejista {Nome}", nmVarejista);
                throw;
            }
        }

        /// <summary>
        /// Atualiza os dados de um varejista.
        /// </summary>
        /// <param name="idVarejista"></param>
        /// <param name="cnpj"></param>
        /// <param name="cdBanner"></param>
        /// <param name="cdCorFundo"></param>
        /// <param name="cdVarejista"></param>
        /// <param name="nmVarejista"></param>
        /// <param name="txLinkSite"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        public async Task<Entities.Varejista> AtualizarVarejistaAsync(int idVarejista, string cnpj, string cdBanner, string cdCorFundo, string cdVarejista, string nmVarejista, string txLinkSite, string updatedBy)
        {
            try
            {
                // 1. Verifica se o varejista existe
                var varejista = await _varejistaRepository.GetByIdAsync(idVarejista);
                if (varejista == null)
                {
                    throw new DomainException($"Varejista com ID {idVarejista} não encontrado");
                }
                // 2. Atualiza os dados do varejista
                varejista.AtualizarDados(cnpj, cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite, updatedBy);
                
                // 3. Salva no banco de dados
                await _varejistaRepository.UpdateAsync(varejista);
                
                // 4. Registra log de sucesso
                _logger.LogInformation("Varejista {Nome} atualizado com sucesso. Id: {Id}", nmVarejista, varejista.IdVarejista);

                return varejista;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar varejista {Nome}", nmVarejista);
                throw;
            }
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