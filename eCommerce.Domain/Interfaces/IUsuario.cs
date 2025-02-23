using eCommerce.Domain.Entities.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de Usuário
    /// </summary>
    public interface IUsuario : IRepository<Usuarios>
    {
        /// <summary>
        /// Busca usuário por email
        /// </summary>
        Task<Usuarios> GetByEmailAsync(string email);

        /// <summary>
        /// Verifica se existe usuário com o mesmo email
        /// </summary>
        Task<bool> ExistsByEmailAsync(string email, int? ignorarId = null);

        /// <summary>
        /// Obtém usuários por broker
        /// </summary>
        Task<IEnumerable<Usuarios>> GetByBrokerIdAsync(int brokerId);

        /// <summary>
        /// Obtém usuários por loja
        /// </summary>
        Task<IEnumerable<Usuarios>> GetByLojaIdAsync(int lojaId);

        /// <summary>
        /// Obtém usuários por varejista
        /// </summary>
        Task<IEnumerable<Usuarios>> GetByVarejistaIdAsync(int varejistaId);

        /// <summary>
        /// Obtém usuários ativos
        /// </summary>
        Task<IEnumerable<Usuarios>> GetAtivosAsync();
    }
} 