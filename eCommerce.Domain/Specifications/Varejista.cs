namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especifica��o para a entidade Varejista.
    /// Verifica se um varejista satisfaz os crit�rios de neg�cio definidos.
    /// </summary>
    public class Varejista : ISpecification<Entities.Varejista>
    {
        /// <summary>
        /// Verifica se a entidade Varejista satisfaz os crit�rios de neg�cio.
        /// </summary>
        /// <param name="varejista">Inst�ncia da entidade Varejista a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os crit�rios, False caso contr�rio.</returns>
        public bool IsSatisfiedBy(Entities.Varejista varejista)
        {
            return !string.IsNullOrEmpty(varejista.NmVarejista)
                && !string.IsNullOrEmpty(varejista.CdCnpj)
                && !string.IsNullOrEmpty(varejista.CdVarejista)
                && varejista.IsActive;
        }
    }
} 