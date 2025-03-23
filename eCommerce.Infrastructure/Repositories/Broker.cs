using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade Broker.
    /// </summary>
    public class Broker : IBroker
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe Broker.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public Broker(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade Broker ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Broker a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Domain.Entities.Broker entity)
        {
            await _dbContext.Set<Domain.Entities.Broker>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade Broker do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Broker a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Domain.Entities.Broker>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade Broker existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Broker.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Broker>().AnyAsync(b => b.IdBroker == id);
        }

        /// <summary>
        /// Verifica se existe uma entidade Broker com o mesmo nome, ignorando um ID específico.
        /// </summary>
        /// <param name="nome">Nome do Broker.</param>
        /// <param name="ignorarId">ID a ser ignorado na verificação (opcional).</param>
        /// <returns>True se existe uma entidade com o mesmo nome, caso contrário False.</returns>
        public async Task<bool> ExistsByNomeAsync(string nome, int? ignorarId = null)
        {
            return await _dbContext.Set<Domain.Entities.Broker>().AnyAsync(b => b.NmBroker == nome && (!ignorarId.HasValue || b.IdBroker != ignorarId.Value));
        }

        /// <summary>
        /// Obtém todas as entidades Broker do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades Broker.</returns>
        public async Task<IEnumerable<Domain.Entities.Broker>> GetAllAsync()
        {
            return await _dbContext.Set<Domain.Entities.Broker>().ToListAsync();
        }

        /// <summary>
        /// Obtém uma entidade Broker pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Broker.</param>
        /// <returns>Entidade Broker correspondente ao ID.</returns>
        public async Task<Domain.Entities.Broker> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Broker>().FirstOrDefaultAsync(b => b.IdBroker == id);
        }

        /// <summary>
        /// Obtém uma entidade Broker pelo seu nome.
        /// </summary>
        /// <param name="nome">Nome do Broker.</param>
        /// <returns>Entidade Broker correspondente ao nome.</returns>
        public async Task<Domain.Entities.Broker> GetByNomeAsync(string nome)
        {
            return await _dbContext.Set<Domain.Entities.Broker>().FirstOrDefaultAsync(b => b.NmBroker == nome);
        }

        /// <summary>
        /// Obtém todas as entidades Broker associadas a um Varejista específico.
        /// </summary>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>Lista de entidades Broker associadas ao Varejista.</returns>
        public async Task<IEnumerable<Domain.Entities.Broker>> GetByVarejistaIdAsync(int varejistaId)
        {
            //TODO RDG: Implementar método GetByVarejistaIdAsync
            throw new NotImplementedException();
        }

        /// <summary>
        /// Atualiza uma entidade Broker existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Broker a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Domain.Entities.Broker entity)
        {
            _dbContext.Set<Domain.Entities.Broker>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

/*
 * Repositório Broker (eCommerce.Infrastructure/Repositories/Broker.cs)
 * 
 * Propósito:
 * Esta classe implementa a interface IBroker e é responsável por todas as operações de
 * persistência relacionadas à entidade Broker no banco de dados.
 * 
 * Principais Funcionalidades:
 * 1. Operações CRUD básicas:
 *    - AddAsync: Adiciona um novo broker
 *    - UpdateAsync: Atualiza um broker existente
 *    - DeleteAsync: Remove um broker pelo ID
 *    - GetByIdAsync: Obtém um broker específico pelo ID
 *    - GetAllAsync: Lista todos os brokers
 *    
 * 2. Operações Específicas:
 *    - GetByNomeAsync: Busca um broker pelo nome
 *    - ExistsByNomeAsync: Verifica duplicidade de nome
 *    - GetByVarejistaIdAsync: Lista brokers por varejista
 * 
 * Exemplos de Uso:
 * 
 * 1. Adicionar um novo Broker:
 *    var broker = Broker.Create("Nome do Broker", "Usuario");
 *    await _brokerRepository.AddAsync(broker);
 * 
 * 2. Atualizar um Broker existente:
 *    var broker = await _brokerRepository.GetByIdAsync(1);
 *    broker.AtualizarDados("Novo Nome", "Usuario");
 *    await _brokerRepository.UpdateAsync(broker);
 * 
 * 3. Verificar duplicidade de nome:
 *    var exists = await _brokerRepository.ExistsByNomeAsync("Nome", ignorarId: 1);
 * 
 * 4. Buscar Brokers de um Varejista:
 *    var brokers = await _brokerRepository.GetByVarejistaIdAsync(varejistaId);
 * 
 * Dependências:
 * - DbContext: Contexto do Entity Framework para acesso ao banco de dados
 * - IBroker: Interface que define as operações do repositório
 * 
 * Observações:
 * - Todas as operações são assíncronas para melhor performance
 * - As operações de alteração (Add/Update/Delete) chamam SaveChangesAsync automaticamente
 * - Implementa padrão Repository para abstrair a camada de persistência
 * - Utiliza Entity Framework Core para operações no banco de dados
 * 
 * Validações:
 * - Verifica existência da entidade antes de remover
 * - Permite verificar duplicidade de nome ignorando um ID específico
 * - Utiliza validações definidas na camada de domínio
 */

