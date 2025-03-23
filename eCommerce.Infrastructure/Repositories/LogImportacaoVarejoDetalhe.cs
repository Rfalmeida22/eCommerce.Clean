using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade LogImportacaoVarejoDetalhe.
    /// </summary>
    public class LogImportacaoVarejoDetalhe : ILogImportacaoVarejoDetalhe
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe LogImportacaoVarejoDetalhe.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public LogImportacaoVarejoDetalhe(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade LogImportacaoVarejoDetalhe ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade LogImportacaoVarejoDetalhe a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Domain.Entities.LogImportacaoVarejoDetalhe entity)
        {
            await _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade LogImportacaoVarejoDetalhe do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade LogImportacaoVarejoDetalhe a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade LogImportacaoVarejoDetalhe existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade LogImportacaoVarejoDetalhe.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>()
                .AnyAsync(l => l.Id == id);
        }

        /// <summary>
        /// Obtém todas as entidades LogImportacaoVarejoDetalhe do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades LogImportacaoVarejoDetalhe.</returns>
        public async Task<IEnumerable<Domain.Entities.LogImportacaoVarejoDetalhe>> GetAllAsync()
        {
            return await _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>().ToListAsync();
        }

        /// <summary>
        /// Obtém detalhes de importação por ID de log.
        /// </summary>
        /// <param name="logId">ID do Log.</param>
        /// <returns>Lista de detalhes de importação associados ao Log.</returns>
        public async Task<IEnumerable<Domain.Entities.LogImportacaoVarejoDetalhe>> GetByLogIdAsync(int logId)
        {
            return await _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>()
                .Where(l => l.IdLog == logId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém uma entidade LogImportacaoVarejoDetalhe pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade LogImportacaoVarejoDetalhe.</param>
        /// <returns>Entidade LogImportacaoVarejoDetalhe correspondente ao ID.</returns>
        public async Task<Domain.Entities.LogImportacaoVarejoDetalhe> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        /// <summary>
        /// Atualiza uma entidade LogImportacaoVarejoDetalhe existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade LogImportacaoVarejoDetalhe a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Domain.Entities.LogImportacaoVarejoDetalhe entity)
        {
            _dbContext.Set<Domain.Entities.LogImportacaoVarejoDetalhe>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
