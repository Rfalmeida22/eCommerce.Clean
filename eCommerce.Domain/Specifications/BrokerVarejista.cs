namespace eCommerce.Domain.Specifications
{
    public class BrokerVarejista : ISpecification<Entities.Brokers_Varejistas>
    {
        public bool IsSatisfiedBy(Entities.Brokers_Varejistas relacionamento)
        {
            return relacionamento.IdBroker > 0
                && relacionamento.IdVarejista > 0
                && relacionamento.IsActive;
        }
    }
} 