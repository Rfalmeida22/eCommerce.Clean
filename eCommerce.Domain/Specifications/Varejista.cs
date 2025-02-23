namespace eCommerce.Domain.Specifications
{
    public class Varejista : ISpecification<Entities.Varejista>
    {
        public bool IsSatisfiedBy(Entities.Varejista varejista)
        {
            return !string.IsNullOrEmpty(varejista.NmVarejista)
                && !string.IsNullOrEmpty(varejista.CdCnpj)
                && !string.IsNullOrEmpty(varejista.CdVarejista)
                && varejista.IsActive;
        }
    }
} 