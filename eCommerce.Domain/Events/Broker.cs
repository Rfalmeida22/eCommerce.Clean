namespace eCommerce.Domain.Events
{
    public class Broker : Event
    {
        public int BrokerId { get; }
        public string Nome { get; }

        public Broker(int brokerId, string nome, string userName) 
            : base(userName)
        {
            BrokerId = brokerId;
            Nome = nome;
        }
    }
} 