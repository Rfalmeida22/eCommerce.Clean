using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade Loja.
    /// </summary>
    public class Loja : ILoja
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe Loja.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public Loja(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade Loja ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Loja a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Lojas entity)
        {
            await _dbContext.Set<Lojas>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade Loja do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Loja a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Lojas>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade Loja existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Loja.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Lojas>().AnyAsync(l => l.IdLoja == id);
        }

        /// <summary>
        /// Verifica se existe uma Loja com o mesmo nome no mesmo varejista.
        /// </summary>
        /// <param name="nome">Nome da Loja.</param>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <param name="ignorarId">ID a ser ignorado na verificação (opcional).</param>
        /// <returns>True se existe uma loja com o mesmo nome, caso contrário False.</returns>
        public async Task<bool> ExistsByNomeAsync(string nome, int varejistaId, int? ignorarId = null)
        {
            return await _dbContext.Set<Lojas>()
                .AnyAsync(l => l.NmLoja == nome &&
                             l.IdVarejista == varejistaId &&
                             (!ignorarId.HasValue || l.IdLoja != ignorarId.Value));
        }

        /// <summary>
        /// Obtém todas as entidades Loja do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades Loja.</returns>
        public async Task<IEnumerable<Lojas>> GetAllAsync()
        {
            return await _dbContext.Set<Lojas>().ToListAsync();
        }

        /// <summary>
        /// Busca loja por CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ da loja.</param>
        /// <returns>Entidade Loja correspondente ao CNPJ.</returns>
        public async Task<Lojas> GetByCnpjAsync(string cnpj)
        {
            return await _dbContext.Set<Lojas>().FirstOrDefaultAsync(l => l.CdCnpj == cnpj);
        }

        /// <summary>
        /// Busca loja por código.
        /// </summary>
        /// <param name="codigo">Código da loja.</param>
        /// <returns>Entidade Loja correspondente ao código.</returns>
        public async Task<Lojas> GetByCodigoAsync(string codigo)
        {
            return await _dbContext.Set<Lojas>().FirstOrDefaultAsync(l => l.CdLoja == codigo);
        }

        /// <summary>
        /// Obtém uma entidade Loja pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Loja.</param>
        /// <returns>Entidade Loja correspondente ao ID.</returns>
        public async Task<Lojas> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Lojas>().FirstOrDefaultAsync(l => l.IdLoja == id);
        }

        /// <summary>
        /// Obtém lojas por lojista.
        /// </summary>
        /// <param name="lojistaId">ID do Lojista.</param>
        /// <returns>Lista de lojas associadas ao lojista.</returns>
        public async Task<IEnumerable<Lojas>> GetByLojistaIdAsync(int lojistaId)
        {
            return await _dbContext.Set<Lojas>()
                .Where(l => l.IdLojista == lojistaId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém lojas por varejista.
        /// </summary>
        /// <param name="varejistaId">ID do Varejista.</param>
        /// <returns>Lista de lojas associadas ao varejista.</returns>
        public async Task<IEnumerable<Lojas>> GetByVarejistaIdAsync(int varejistaId)
        {
            return await _dbContext.Set<Lojas>()
                .Where(l => l.IdVarejista == varejistaId)
                .ToListAsync();
        }

        /// <summary>
        /// Atualiza uma entidade Loja existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Loja a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Lojas entity)
        {
            _dbContext.Set<Lojas>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
