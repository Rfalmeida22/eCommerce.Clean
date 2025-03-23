using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using eCommerce.Domain.Exceptions;

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
                // 1. Verifica se j� existe loja com mesmo CNPJ
                if (!await ValidarCnpjUnicoAsync(cnpj))
                {
                    _logger.LogWarning("Loja com CNPJ {Cnpj} j� existe", cnpj);
                    throw new DomainException($"J� existe uma Loja com o CNPJ {cnpj}");
                }

                // 2. Verifica se j� existe loja com mesmo c�digo
                if (!await ValidarCodigoUnicoAsync(codigoLoja))
                {
                    _logger.LogWarning("Loja com c�digo {Codigo} j� existe", codigoLoja);
                    throw new DomainException($"J� existe uma Loja com o c�digo {codigoLoja}");
                }

                // 3. Cria uma nova inst�ncia de loja
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
                    _logger.LogWarning("Loja com ID {Id} n�o existe", idLoja);
                    throw new DomainException($"Loja com ID {idLoja} n�o existe");
                }

                // 2. Verifica se j� existe loja com mesmo CNPJ
                if (!await ValidarCnpjUnicoAsync(cnpj))
                {
                    _logger.LogWarning("Loja com CNPJ {Cnpj} j� existe", cnpj);
                    throw new DomainException($"J� existe uma Loja com o CNPJ {cnpj}");
                }

                // 3. Verifica se j� existe loja com mesmo c�digo
                if (!await ValidarCodigoUnicoAsync(codigoLoja))
                {
                    _logger.LogWarning("Loja com c�digo {Codigo} j� existe", codigoLoja);
                    throw new DomainException($"J� existe uma Loja com o c�digo {codigoLoja}");
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