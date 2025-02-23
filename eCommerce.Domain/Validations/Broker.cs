namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe de valida��o para a entidade Broker.
    /// </summary>
    public class Broker : Validation
    {
        private readonly Entities.Broker _broker;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe de valida��o Broker.
        /// </summary>
        /// <param name="broker">Inst�ncia da entidade Broker a ser validada.</param>
        public Broker(Entities.Broker broker)
        {
            _broker = broker;

        }

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe de valida��o Broker.
        /// </summary>
        public void Validate()
        {
            // Valida se o nome do broker � obrigat�rio
            ValidateRequired(_broker.NmBroker, "Nome");
            // Valida o comprimento m�ximo do nome do broker
            ValidateMaxLength(_broker.NmBroker, 100, "Nome");
        }
    }
} 