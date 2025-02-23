namespace eCommerce.Domain.Events
{
    public class Usuario : Event
    {
        public int UsuarioId { get; }
        public string Nome { get; }
        public string Email { get; }

        public Usuario(int usuarioId, string nome, string email, string userName) 
            : base(userName)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
        }
    }
} 