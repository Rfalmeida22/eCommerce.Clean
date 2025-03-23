using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas ao relacionamento entre Broker e Varejista.
    /// </summary>
    public class Brokers_Varejistas : IBrokerVarejista
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe Brokers_Varejistas.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public Brokers_Varejistas(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade Brokers_Varejistas ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Brokers_Varejistas a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Domain.Entities.Brokers_Varejistas entity)
        {
            await _dbContext.Set<Domain.Entities.Brokers_Varejistas>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade Brokers_Varejistas do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Brokers_Varejistas a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Domain.Entities.Brokers_Varejistas>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade Brokers_Varejistas existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Brokers_Varejistas.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Brokers_Varejistas>()
                .AnyAsync(bv => bv.IdSequencial == id);
        }

        /// <summary>
        /// Verifica se já existe relacionamento entre broker e varejista.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>True se o relacionamento existe, caso contrário False.</returns>
        public async Task<bool> ExistsRelacionamentoAsync(int brokerId, int varejistaId)
        {
            return await _dbContext.Set<Domain.Entities.Brokers_Varejistas>()
                .AnyAsync(bv => bv.IdBroker == brokerId && bv.IdVarejista == varejistaId);
        }

        /// <summary>
        /// Obtém todas as entidades Brokers_Varejistas do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades Brokers_Varejistas.</returns>
        public async Task<IEnumerable<Domain.Entities.Brokers_Varejistas>> GetAllAsync()
        {
            return await _dbContext.Set<Domain.Entities.Brokers_Varejistas>().ToListAsync();
        }

        /// <summary>
        /// Obtém relacionamentos por broker.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <returns>Lista de relacionamentos associados ao Broker.</returns>
        public async Task<IEnumerable<Domain.Entities.Brokers_Varejistas>> GetByBrokerIdAsync(int brokerId)
        {
            return await _dbContext.Set<Domain.Entities.Brokers_Varejistas>()
                .Where(bv => bv.IdBroker == brokerId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém relacionamentos por varejista.
        /// </summary>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>Lista de relacionamentos associados ao Varejista.</returns>
        public async Task<IEnumerable<Domain.Entities.Brokers_Varejistas>> GetByVarejistaIdAsync(int varejistaId)
        {
            return await _dbContext.Set<Domain.Entities.Brokers_Varejistas>()
                .Where(bv => bv.IdVarejista == varejistaId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém uma entidade Brokers_Varejistas pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Brokers_Varejistas.</param>
        /// <returns>Entidade Brokers_Varejistas correspondente ao ID.</returns>
        public async Task<Domain.Entities.Brokers_Varejistas> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Brokers_Varejistas>()
                .FirstOrDefaultAsync(bv => bv.IdSequencial == id);
        }

        /// <summary>
        /// Atualiza uma entidade Brokers_Varejistas existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Brokers_Varejistas a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Domain.Entities.Brokers_Varejistas entity)
        {
            _dbContext.Set<Domain.Entities.Brokers_Varejistas>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
