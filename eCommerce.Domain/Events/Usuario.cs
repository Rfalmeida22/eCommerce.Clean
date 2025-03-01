namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de dom�nio que representa uma a��o relacionada a um Usu�rio.
    /// </summary>
    public class Usuario : Event
    {
        /// <summary>
        /// Identificador �nico do Usu�rio.
        /// </summary>
        public int UsuarioId { get; }

        /// <summary>
        /// Nome do Usu�rio.
        /// </summary>
        public string Nome { get; }

        /// <summary>
        /// Email do Usu�rio.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Inicializa uma nova inst�ncia do evento Usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador �nico do Usu�rio.</param>
        /// <param name="nome">Nome do Usu�rio.</param>
        /// <param name="email">Email do Usu�rio.</param>
        /// <param name="userName">Nome do usu�rio que gerou o evento.</param>
        public Usuario(int usuarioId, string nome, string email, string userName)
            : base(userName)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
        }
    }
} 