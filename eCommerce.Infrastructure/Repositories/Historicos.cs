using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade Historicos.
    /// </summary>
    public class Historicos : IHistoricos
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe Historicos.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public Historicos(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade Historicos ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Historicos a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Domain.Entities.Historicos entity)
        {
            await _dbContext.Set<Domain.Entities.Historicos>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade Historicos do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Historicos a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Domain.Entities.Historicos>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade Historicos existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Historicos.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Historicos>()
                .AnyAsync(h => h.Historicos_Cod == id);
        }

        /// <summary>
        /// Obtém todas as entidades Historicos do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades Historicos.</returns>
        public async Task<IEnumerable<Domain.Entities.Historicos>> GetAllAsync()
        {
            return await _dbContext.Set<Domain.Entities.Historicos>().ToListAsync();
        }

        /// <summary>
        /// Obtém históricos por código de usuário.
        /// </summary>
        /// <param name="usuariosCod">Código do usuário.</param>
        /// <returns>Lista de históricos associados ao usuário.</returns>
        public async Task<IEnumerable<Domain.Entities.Historicos>> GetByUsuarioCodAsync(int usuariosCod)
        {
            return await _dbContext.Set<Domain.Entities.Historicos>()
                .Where(h => h.Usuarios_Cod == usuariosCod)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém históricos por código de empresa.
        /// </summary>
        /// <param name="idEmpresa">Código da empresa.</param>
        /// <returns>Lista de históricos associados à empresa.</returns>
        public async Task<IEnumerable<Domain.Entities.Historicos>> GetByEmpresaIdAsync(int idEmpresa)
        {
            return await _dbContext.Set<Domain.Entities.Historicos>()
                .Where(h => h.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém uma entidade Historicos pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Historicos.</param>
        /// <returns>Entidade Historicos correspondente ao ID.</returns>
        public async Task<Domain.Entities.Historicos> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Historicos>()
                .FirstOrDefaultAsync(h => h.Historicos_Cod == id);
        }

        /// <summary>
        /// Atualiza uma entidade Historicos existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Historicos a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Domain.Entities.Historicos entity)
        {
            _dbContext.Set<Domain.Entities.Historicos>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

