namespace eCommerce.Domain.Events
{
    public class BrokerVarejista : Event
    {
        public int BrokerId { get; }
        public int VarejistaId { get; }

        public BrokerVarejista(int brokerId, int varejistaId, string userName) 
            : base(userName)
        {
            BrokerId = brokerId;
            VarejistaId = varejistaId;
        }
    }
} 