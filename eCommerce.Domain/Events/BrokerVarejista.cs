namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada ao relacionamento entre um Broker e um Varejista.
    /// </summary>
    public class BrokerVarejista : Event
    {
        /// <summary>
        /// Identificador único do Broker.
        /// </summary>
        public int BrokerId { get; }

        /// <summary>
        /// Identificador único do Varejista.
        /// </summary>
        public int VarejistaId { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento BrokerVarejista.
        /// </summary>
        /// <param name="brokerId">Identificador único do Broker.</param>
        /// <param name="varejistaId">Identificador único do Varejista.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public BrokerVarejista(int brokerId, int varejistaId, string userName)
            : base(userName)
        {
            BrokerId = brokerId;
            VarejistaId = varejistaId;
        }
    }
} 