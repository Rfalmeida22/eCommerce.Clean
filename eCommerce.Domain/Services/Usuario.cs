namespace eCommerce.Domain.Services
{
    public class Usuario : IDomainService
    {
        private readonly IUsuario _usuarioRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public Usuario(IUsuario usuarioRepository, IEventDispatcher eventDispatcher)
        {
            _usuarioRepository = usuarioRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<bool> ValidarAcessoAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null) return false;

            // Implementar lógica de validação de senha
            return true;
        }

        public async Task<bool> VerificarPermissaoAsync(int usuarioId, int varejistaId)
        {
            var usuarios = await _usuarioRepository.GetByVarejistaIdAsync(varejistaId);
            return usuarios.Any(u => u.Usuarios_Cod == usuarioId);
        }
    }
} 