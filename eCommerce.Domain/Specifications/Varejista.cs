namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especificação para a entidade Varejista.
    /// Verifica se um varejista satisfaz os critérios de negócio definidos.
    /// </summary>
    public class Varejista : ISpecification<Entities.Varejista>
    {
        /// <summary>
        /// Verifica se a entidade Varejista satisfaz os critérios de negócio.
        /// </summary>
        /// <param name="varejista">Instância da entidade Varejista a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os critérios, False caso contrário.</returns>
        public bool IsSatisfiedBy(Entities.Varejista varejista)
        {
            return !string.IsNullOrEmpty(varejista.NmVarejista)
                && !string.IsNullOrEmpty(varejista.CdCnpj)
                && !string.IsNullOrEmpty(varejista.CdVarejista)
                && varejista.IsActive;
        }
    }
} 