namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especificação para a entidade Lojas.
    /// Verifica se uma Loja satisfaz os critérios de negócio definidos.
    /// </summary>
    public class Loja : ISpecification<Entities.Lojas>
    {
        /// <summary>
        /// Verifica se a entidade Loja satisfaz os critérios de negócio.
        /// </summary>
        /// <param name="loja">Instância da entidade Lojas a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os critérios, False caso contrário.</returns>
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