namespace eCommerce.Domain.Specifications
{
    public class Loja : ISpecification<Entities.Lojas>
    {
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