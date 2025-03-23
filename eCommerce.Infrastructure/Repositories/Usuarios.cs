using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade Usuarios.
    /// </summary>
    public class Usuarios : IUsuario
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe Usuarios.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public Usuarios(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade Usuarios ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Usuarios a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Domain.Entities.Usuarios entity)
        {
            await _dbContext.Set<Domain.Entities.Usuarios>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade Usuarios do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Usuarios a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Domain.Entities.Usuarios>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade Usuarios existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Usuarios.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .AnyAsync(u => u.Usuarios_Cod == id);
        }

        /// <summary>
        /// Verifica se existe uma entidade Usuarios com o mesmo email, ignorando um ID específico.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="ignorarId">ID a ser ignorado na verificação (opcional).</param>
        /// <returns>True se existe uma entidade com o mesmo email, caso contrário False.</returns>
        public async Task<bool> ExistsByEmailAsync(string email, int? ignorarId = null)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .AnyAsync(u => u.Usuarios_Ema == email &&
                           (!ignorarId.HasValue || u.Usuarios_Cod != ignorarId.Value));
        }

        /// <summary>
        /// Obtém todas as entidades Usuarios do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades Usuarios.</returns>
        public async Task<IEnumerable<Domain.Entities.Usuarios>> GetAllAsync()
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>().ToListAsync();
        }

        /// <summary>
        /// Obtém usuários por loja.
        /// </summary>
        /// <param name="lojaId">ID da Loja.</param>
        /// <returns>Lista de usuários associados à Loja.</returns>
        public async Task<IEnumerable<Domain.Entities.Usuarios>> GetByLojaIdAsync(int lojaId)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .Where(u => u.IdLoja == lojaId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém usuários por varejista.
        /// </summary>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>Lista de usuários associados ao Varejista.</returns>
        public async Task<IEnumerable<Domain.Entities.Usuarios>> GetByVarejistaIdAsync(int varejistaId)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .Where(u => u.IdVarejista == varejistaId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém uma entidade Usuarios pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Usuarios.</param>
        /// <returns>Entidade Usuarios correspondente ao ID.</returns>
        public async Task<Domain.Entities.Usuarios> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .FirstOrDefaultAsync(u => u.Usuarios_Cod == id);
        }

        /// <summary>
        /// Atualiza uma entidade Usuarios existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Usuarios a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Domain.Entities.Usuarios entity)
        {
            _dbContext.Set<Domain.Entities.Usuarios>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Busca um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>Entidade Usuarios correspondente ao email.</returns>
        public async Task<Domain.Entities.Usuarios> GetByEmailAsync(string email)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .FirstOrDefaultAsync(u => u.Usuarios_Ema == email);
        }


        /// <summary>
        /// Obtém usuários por broker.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <returns>Lista de usuários associados ao Broker.</returns>
        public async Task<IEnumerable<Domain.Entities.Usuarios>> GetByBrokerIdAsync(int brokerId)
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .Where(u => u.IdBroker == brokerId)
                .ToListAsync();
        }


        /// <summary>
        /// Obtém usuários ativos.
        /// </summary>
        /// <returns>Lista de usuários ativos.</returns>
        public async Task<IEnumerable<Domain.Entities.Usuarios>> GetAtivosAsync()
        {
            return await _dbContext.Set<Domain.Entities.Usuarios>()
                .Where(u => u.Usuarios_Ati)
                .ToListAsync();
        }

    }
}

