using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de Broker
    /// </summary>
    public interface IBroker : IRepository<Broker>
    {
        /// <summary>
        /// Busca broker por nome
        /// </summary>
        Task<Broker> GetByNomeAsync(string nome);

        /// <summary>
        /// Verifica se existe broker com o mesmo nome
        /// </summary>
        Task<bool> ExistsByNomeAsync(string nome, int? ignorarId = null);

        /// <summary>
        /// Obtém brokers por varejista
        /// </summary>
        Task<IEnumerable<Broker>> GetByVarejistaIdAsync(int varejistaId);
    }
} 