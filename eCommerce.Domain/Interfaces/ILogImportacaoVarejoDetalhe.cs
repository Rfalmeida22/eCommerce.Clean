using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de LogImportacaoVarejoDetalhe
    /// </summary>
    public interface ILogImportacaoVarejoDetalhe : IRepository<LogImportacaoVarejoDetalhe>
    {
        /// <summary>
        /// Obtém detalhes de importação por ID de log
        /// </summary>
        Task<IEnumerable<LogImportacaoVarejoDetalhe>> GetByLogIdAsync(int logId);
    }
}
