namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de dom�nio que representa uma a��o relacionada ao relacionamento entre um Broker e um Varejista.
    /// </summary>
    public class BrokerVarejista : Event
    {
        /// <summary>
        /// Identificador �nico do Broker.
        /// </summary>
        public int BrokerId { get; }

        /// <summary>
        /// Identificador �nico do Varejista.
        /// </summary>
        public int VarejistaId { get; }

        /// <summary>
        /// Inicializa uma nova inst�ncia do evento BrokerVarejista.
        /// </summary>
        /// <param name="brokerId">Identificador �nico do Broker.</param>
        /// <param name="varejistaId">Identificador �nico do Varejista.</param>
        /// <param name="userName">Nome do usu�rio que gerou o evento.</param>
        public BrokerVarejista(int brokerId, int varejistaId, string userName)
            : base(userName)
        {
            BrokerId = brokerId;
            VarejistaId = varejistaId;
        }
    }
} 