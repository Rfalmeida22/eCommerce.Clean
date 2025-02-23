namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especifica��o para a entidade Lojas.
    /// Verifica se uma Loja satisfaz os crit�rios de neg�cio definidos.
    /// </summary>
    public class Loja : ISpecification<Entities.Lojas>
    {
        /// <summary>
        /// Verifica se a entidade Loja satisfaz os crit�rios de neg�cio.
        /// </summary>
        /// <param name="loja">Inst�ncia da entidade Lojas a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os crit�rios, False caso contr�rio.</returns>
        public bool IsSatisfiedBy(Entities.Lojas loja)
        {
            return !string.IsNullOrEmpty(loja.NmLoja)
                && !string.IsNullOrEmpty(loja.CdCnpj)
                && !string.IsNullOrEmpty(loja.CdLoja)
                && loja.IdVarejista > 0
                && loja.IsActive;
        }
    }
} 