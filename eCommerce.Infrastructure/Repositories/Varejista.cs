using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade Varejista.
    /// </summary>
    public class Varejista : IVarejista
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Inicializa uma nova instância da classe Varejista.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados.</param>
        public Varejista(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adiciona uma nova entidade Varejista ao banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Varejista a ser adicionada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task AddAsync(Domain.Entities.Varejista entity)
        {
            await _dbContext.Set<Domain.Entities.Varejista>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma entidade Varejista do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Varejista a ser removida.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<Domain.Entities.Varejista>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se uma entidade Varejista existe no banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Varejista.</param>
        /// <returns>True se a entidade existe, caso contrário False.</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Varejista>()
                .AnyAsync(v => v.IdVarejista == id);
        }

        /// <summary>
        /// Verifica se existe uma entidade Varejista com o mesmo nome, ignorando um ID específico.
        /// </summary>
        /// <param name="nome">Nome do Varejista.</param>
        /// <param name="ignorarId">ID a ser ignorado na verificação (opcional).</param>
        /// <returns>True se existe uma entidade com o mesmo nome, caso contrário False.</returns>
        public async Task<bool> ExistsByNomeAsync(string nome, int? ignorarId = null)
        {
            return await _dbContext.Set<Domain.Entities.Varejista>()
                .AnyAsync(v => v.NmVarejista == nome &&
                           (!ignorarId.HasValue || v.IdVarejista != ignorarId.Value));
        }

        /// <summary>
        /// Obtém todas as entidades Varejista do banco de dados.
        /// </summary>
        /// <returns>Lista de todas as entidades Varejista.</returns>
        public async Task<IEnumerable<Domain.Entities.Varejista>> GetAllAsync()
        {
            return await _dbContext.Set<Domain.Entities.Varejista>().ToListAsync();
        }

        /// <summary>
        /// Obtém varejistas associados a um Broker específico.
        /// </summary>
        /// <param name="brokerId">ID do Broker.</param>
        /// <returns>Lista de varejistas associados ao Broker.</returns>
        public async Task<IEnumerable<Domain.Entities.Varejista>> GetByBrokerIdAsync(int brokerId)
        {
            return await _dbContext.Set<Domain.Entities.Varejista>()
                .Include(v => v.BrokersVarejistas)
                .Where(v => v.BrokersVarejistas.Any(bv => bv.IdBroker == brokerId))
                .ToListAsync();
        }

        /// <summary>
        /// Busca um varejista pelo CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ do varejista.</param>
        /// <returns>Entidade Varejista correspondente ao CNPJ.</returns>
        public async Task<Domain.Entities.Varejista> GetByCnpjAsync(string cnpj)
        {
            return await _dbContext.Set<Domain.Entities.Varejista>()
                .FirstOrDefaultAsync(v => v.CdCnpj == cnpj);
        }

        /// <summary>
        /// Obtém uma entidade Varejista pelo seu ID.
        /// </summary>
        /// <param name="id">ID da entidade Varejista.</param>
        /// <returns>Entidade Varejista correspondente ao ID.</returns>
        public async Task<Domain.Entities.Varejista> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Domain.Entities.Varejista>()
                .Include(v => v.BrokersVarejistas)
                .Include(v => v.Lojas)
                .FirstOrDefaultAsync(v => v.IdVarejista == id);
        }

        /// <summary>
        /// Atualiza uma entidade Varejista existente no banco de dados.
        /// </summary>
        /// <param name="entity">Entidade Varejista a ser atualizada.</param>
        /// <returns>Tarefa representando a operação assíncrona.</returns>
        public async Task UpdateAsync(Domain.Entities.Varejista entity)
        {
            _dbContext.Set<Domain.Entities.Varejista>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

/*
 * Repositório Varejista (eCommerce.Infrastructure/Repositories/Varejista.cs)
 * 
 * Propósito:
 * Esta classe implementa a interface IVarejista e é responsável por todas as operações de
 * persistência relacionadas à entidade Varejista no banco de dados.
 * 
 * Principais Funcionalidades:
 * 1. Operações CRUD básicas:
 *    - AddAsync: Adiciona um novo varejista
 *    - UpdateAsync: Atualiza um varejista existente
 *    - DeleteAsync: Remove um varejista pelo ID
 *    - GetByIdAsync: Obtém um varejista específico pelo ID
 *    - GetAllAsync: Lista todos os varejistas
 *    
 * 2. Operações Específicas:
 *    - GetByCnpjAsync: Busca um varejista pelo CNPJ
 *    - GetByBrokerIdAsync: Lista varejistas por broker
 *    - ExistsByNomeAsync: Verifica duplicidade de nome
 * 
 * Exemplos de Uso:
 * 
 * 1. Adicionar um novo Varejista:
 *    var varejista = Varejista.Create("CNPJ", "Banner", "CorFundo", "Codigo", "Nome", "Site", "Usuario");
 *    await _varejistaRepository.AddAsync(varejista);
 * 
 * 2. Atualizar um Varejista existente:
 *    var varejista = await _varejistaRepository.GetByIdAsync(1);
 *    varejista.AtualizarDados("CNPJ", "Banner", "CorFundo", "Codigo", "Nome", "Site", "Usuario");
 *    await _varejistaRepository.UpdateAsync(varejista);
 * 
 * 3. Verificar duplicidade de nome:
 *    var exists = await _varejistaRepository.ExistsByNomeAsync("Nome", ignorarId: 1);
 * 
 * 4. Buscar Varejistas de um Broker:
 *    var varejistas = await _varejistaRepository.GetByBrokerIdAsync(brokerId);
 * 
 * Dependências:
 * - DbContext: Contexto do Entity Framework para acesso ao banco de dados
 * - IVarejista: Interface que define as operações do repositório
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
