using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using eCommerce.Domain.Exceptions;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a Loja.
    /// </summary>
    public class Loja : IDomainService
    {
        private readonly ILoja _lojaRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<Loja> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe Loja.
        /// </summary>
        /// <param name="lojaRepository">Repositório de Loja.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public Loja(ILoja lojaRepository, IEventDispatcher eventDispatcher, ILogger<Loja> logger)
        {
            _lojaRepository = lojaRepository ?? throw new ArgumentNullException(nameof(lojaRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Cadastra uma nova Loja no sistema.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="codigoLoja"></param>
        /// <param name="idLojista"></param>
        /// <param name="idVarejista"></param>
        /// <param name="nomeLoja"></param>
        /// <param name="endereco"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        public async Task<Entities.Lojas> CadastrarLojaAsync(string cnpj, string codigoLoja, int idLojista, int idVarejista, string nomeLoja, string endereco, string createdBy)
        {
            try
            {
                // 1. Verifica se já existe loja com mesmo CNPJ
                if (!await ValidarCnpjUnicoAsync(cnpj))
                {
                    _logger.LogWarning("Loja com CNPJ {Cnpj} já existe", cnpj);
                    throw new DomainException($"Já existe uma Loja com o CNPJ {cnpj}");
                }

                // 2. Verifica se já existe loja com mesmo código
                if (!await ValidarCodigoUnicoAsync(codigoLoja))
                {
                    _logger.LogWarning("Loja com código {Codigo} já existe", codigoLoja);
                    throw new DomainException($"Já existe uma Loja com o código {codigoLoja}");
                }

                // 3. Cria uma nova instância de loja
                var loja = Entities.Lojas.Create(cnpj, codigoLoja, idLojista, idVarejista, nomeLoja, endereco, createdBy);

                // 4. Salva no banco de dados
                await _lojaRepository.AddAsync(loja);

                // 5. Registra log de sucesso
                _logger.LogInformation("Loja {Nome} cadastrada com sucesso. Id: {Id}", nomeLoja, loja.IdLoja);

                return loja;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar Loja {Nome}", nomeLoja);
                throw;
            }
        }

        /// <summary>
        /// Atualiza os dados de uma Loja no sistema.
        /// </summary>
        /// <param name="idLoja"></param>
        /// <param name="cnpj"></param>
        /// <param name="codigoLoja"></param>
        /// <param name="idLojista"></param>
        /// <param name="idVarejista"></param>
        /// <param name="nomeLoja"></param>
        /// <param name="endereco"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        public async Task<Entities.Lojas> AtualizarLojaAsync(int idLoja, string cnpj, string codigoLoja, int idLojista, int idVarejista, string nomeLoja, string endereco, string updatedBy)
        {
            try
            {
                // 1. Verifica se a loja existe
                if (!await _lojaRepository.ExistsAsync(idLoja))
                {
                    _logger.LogWarning("Loja com ID {Id} não existe", idLoja);
                    throw new DomainException($"Loja com ID {idLoja} não existe");
                }

                // 2. Verifica se já existe loja com mesmo CNPJ
                if (!await ValidarCnpjUnicoAsync(cnpj))
                {
                    _logger.LogWarning("Loja com CNPJ {Cnpj} já existe", cnpj);
                    throw new DomainException($"Já existe uma Loja com o CNPJ {cnpj}");
                }

                // 3. Verifica se já existe loja com mesmo código
                if (!await ValidarCodigoUnicoAsync(codigoLoja))
                {
                    _logger.LogWarning("Loja com código {Codigo} já existe", codigoLoja);
                    throw new DomainException($"Já existe uma Loja com o código {codigoLoja}");
                }

                // 4. Atualiza os dados da loja
                var loja = await _lojaRepository.GetByIdAsync(idLoja);

                loja.AtualizarDados(cnpj, codigoLoja, idLojista, idVarejista, nomeLoja, endereco, updatedBy);

                // 5. Salva no banco de dados
                await _lojaRepository.UpdateAsync(loja);

                // 6. Registra log de sucesso
                _logger.LogInformation("Loja {Nome} atualizada com sucesso. Id: {Id}", nomeLoja, loja.IdLoja);

                return loja;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar Loja {Nome}", nomeLoja);
                throw;
            }
        }

        /// <summary>
        /// Valida se o CNPJ da loja é único.
        /// </summary>
        /// <param name="cnpj">CNPJ da loja.</param>
        /// <returns>True se o CNPJ é único, caso contrário False.</returns>
        public async Task<bool> ValidarCnpjUnicoAsync(string cnpj)
        {
            var loja = await _lojaRepository.GetByCnpjAsync(cnpj);
            return loja == null;
        }

        /// <summary>
        /// Valida se o código da loja é único.
        /// </summary>
        /// <param name="codigo">Código da loja.</param>
        /// <returns>True se o código é único, caso contrário False.</returns>
        public async Task<bool> ValidarCodigoUnicoAsync(string codigo)
        {
            var loja = await _lojaRepository.GetByCodigoAsync(codigo);
            return loja == null;
        }

        /// <summary>
        /// Valida se uma Loja está vinculada a um Varejista específico.
        /// </summary>
        /// <param name="lojaId">ID da Loja.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se a Loja está vinculada ao Varejista, caso contrário False.</returns>
        /// <exception cref="ArgumentException">Lançada quando lojaId ou varejistaId são inválidos.</exception>
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

                // Publicar evento de domínio
                var evento = new Events.Loja(lojaId, "Nome da Loja", "CNPJ da Loja", varejistaId, "Nome do Usuário");
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