using eCommerce.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface genérica para repositórios
    /// </summary>
    /// <typeparam name="T">Tipo da entidade que herda de BaseEntity</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Obtém uma entidade pelo seu ID
        /// </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Obtém todas as entidades
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adiciona uma nova entidade
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Atualiza uma entidade existente
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Remove uma entidade pelo seu ID
        /// </summary>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica se existe uma entidade com o ID especificado
        /// </summary>
        Task<bool> ExistsAsync(int id);
    }
} 