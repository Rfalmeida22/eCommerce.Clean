using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Interface para operações de persistência de Historicos
    /// </summary>
    public interface IHistoricos : IRepository<Historicos>
    {
        /// <summary>
        /// Obtém históricos por código de usuário
        /// </summary>
        /// <param name="usuariosCod">int</param>
        /// <returns></returns>
        Task<IEnumerable<Historicos>> GetByUsuarioCodAsync(int usuariosCod);

        /// <summary>
        /// Obtém históricos por código de empresa
        /// </summary>
        /// <param name="idEmpresa">int</param>
        /// <returns></returns>
        Task<IEnumerable<Historicos>> GetByEmpresaIdAsync(int idEmpresa);
    }
}
