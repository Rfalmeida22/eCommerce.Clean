namespace eCommerce.Domain.Validations
{
    public class Broker : Validation
    {
        private readonly Entities.Broker _broker;

        public Broker(Entities.Broker broker)
        {
            _broker = broker;

            ValidateRequired(_broker.NmBroker, "Nome");
            ValidateMaxLength(_broker.NmBroker, 100, "Nome");
        }
    }
} 