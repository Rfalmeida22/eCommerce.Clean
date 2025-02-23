namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Classe base abstrata para todos os eventos de dom�nio.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Data e hora em que o evento ocorreu.
        /// </summary>
        public DateTime OccurredOn { get; }

        /// <summary>
        /// Nome do usu�rio que gerou o evento.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Construtor protegido para inicializar um novo evento de dom�nio.
        /// </summary>
        /// <param name="userName">Nome do usu�rio que gerou o evento.</param>
        protected Event(string userName)
        {
            OccurredOn = DateTime.UtcNow;
            UserName = userName;
        }
    }
} 