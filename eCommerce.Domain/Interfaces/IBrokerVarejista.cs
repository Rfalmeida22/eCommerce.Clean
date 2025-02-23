using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência do relacionamento entre Broker e Varejista
    /// </summary>
    public interface IBrokerVarejista : IRepository<Brokers_Varejistas>
    {
        /// <summary>
        /// Verifica se já existe relacionamento entre broker e varejista
        /// </summary>
        Task<bool> ExistsRelacionamentoAsync(int brokerId, int varejistaId);

        /// <summary>
        /// Obtém relacionamentos por broker
        /// </summary>
        Task<IEnumerable<Brokers_Varejistas>> GetByBrokerIdAsync(int brokerId);

        /// <summary>
        /// Obtém relacionamentos por varejista
        /// </summary>
        Task<IEnumerable<Brokers_Varejistas>> GetByVarejistaIdAsync(int varejistaId);
    }
} 