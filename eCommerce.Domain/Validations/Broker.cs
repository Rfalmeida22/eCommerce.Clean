namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe de validação para a entidade Broker.
    /// </summary>
    public class Broker : Validation
    {
        private readonly Entities.Broker _broker;

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Broker.
        /// </summary>
        /// <param name="broker">Instância da entidade Broker a ser validada.</param>
        public Broker(Entities.Broker broker)
        {
            _broker = broker;

        }

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Broker.
        /// </summary>
        public void Validate()
        {
            // Valida se o nome do broker é obrigatório
            ValidateRequired(_broker.NmBroker, "Nome");
            // Valida o comprimento máximo do nome do broker
            ValidateMaxLength(_broker.NmBroker, 100, "Nome");
        }
    }
} 