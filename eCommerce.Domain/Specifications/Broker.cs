namespace eCommerce.Domain.Specifications
{
    public class Broker : ISpecification<Entities.Broker>
    {
        public bool IsSatisfiedBy(Entities.Broker broker)
        {
            return !string.IsNullOrEmpty(broker.NmBroker)
                && broker.IsActive;
        }
    }
} 