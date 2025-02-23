using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para gerenciar transações
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Confirma todas as alterações feitas na transação atual
        /// </summary>
        /// <returns>True se as alterações foram salvas com sucesso</returns>
        Task<bool> CommitAsync();

        /// <summary>
        /// Desfaz todas as alterações feitas na transação atual
        /// </summary>
        Task RollbackAsync();
    }
} 