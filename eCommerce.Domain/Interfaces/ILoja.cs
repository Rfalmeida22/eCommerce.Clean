using eCommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de Loja
    /// </summary>
    public interface ILoja : IRepository<Lojas>
    {
        /// <summary>
        /// Busca loja por CNPJ
        /// </summary>
        Task<Lojas> GetByCnpjAsync(string cnpj);

        /// <summary>
        /// Busca loja por código
        /// </summary>
        Task<Lojas> GetByCodigoAsync(string codigo);

        /// <summary>
        /// Obtém lojas por varejista
        /// </summary>
        Task<IEnumerable<Lojas>> GetByVarejistaIdAsync(int varejistaId);

        /// <summary>
        /// Obtém lojas por lojista
        /// </summary>
        Task<IEnumerable<Lojas>> GetByLojistaIdAsync(int lojistaId);

        /// <summary>
        /// Verifica se existe loja com o mesmo nome
        /// </summary>
        Task<bool> ExistsByNomeAsync(string nome, int varejistaId, int? ignorarId = null);
    }
} 