namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada a um Usuário.
    /// </summary>
    public class Usuario : Event
    {
        /// <summary>
        /// Identificador único do Usuário.
        /// </summary>
        public int UsuarioId { get; }

        /// <summary>
        /// Nome do Usuário.
        /// </summary>
        public string Nome { get; }

        /// <summary>
        /// Email do Usuário.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento Usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador único do Usuário.</param>
        /// <param name="nome">Nome do Usuário.</param>
        /// <param name="email">Email do Usuário.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public Usuario(int usuarioId, string nome, string email, string userName)
            : base(userName)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
        }
    }
} 