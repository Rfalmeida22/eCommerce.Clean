using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de Varejista
    /// </summary>
    public interface IVarejista : IRepository<Varejista>
    {
        /// <summary>
        /// Busca varejista por CNPJ
        /// </summary>
        Task<Varejista> GetByCnpjAsync(string cnpj);

        /// <summary>
        /// Obtém varejistas por broker
        /// </summary>
        Task<IEnumerable<Varejista>> GetByBrokerIdAsync(int brokerId);

        /// <summary>
        /// Verifica se existe varejista com o mesmo nome
        /// </summary>
        Task<bool> ExistsByNomeAsync(string nome, int? ignorarId = null);
    }
} 