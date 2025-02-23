namespace eCommerce.Domain.Events
{
    public abstract class Event
    {
        public DateTime OccurredOn { get; }
        public string UserName { get; }

        protected Event(string userName)
        {
            OccurredOn = DateTime.UtcNow;
            UserName = userName;
        }
    }
} 