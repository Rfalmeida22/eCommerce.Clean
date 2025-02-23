using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a Usuário.
    /// </summary>
    public class Usuario : IDomainService
    {
        private readonly IUsuario _usuarioRepository;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Inicializa uma nova instância da classe Usuario.
        /// </summary>
        /// <param name="usuarioRepository">Repositório de Usuário.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public Usuario(IUsuario usuarioRepository, IEventDispatcher eventDispatcher)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        /// <summary>
        /// Valida o acesso de um usuário com base no email e senha.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="senha">Senha do usuário.</param>
        /// <returns>True se o acesso é válido, caso contrário False.</returns>
        public async Task<bool> ValidarAcessoAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null) return false;

            // Implementar lógica de validação de senha
            return true;
        }

        /// <summary>
        /// Verifica se um usuário tem permissão para acessar um varejista específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <param name="varejistaId">ID do varejista.</param>
        /// <returns>True se o usuário tem permissão, caso contrário False.</returns>
        public async Task<bool> VerificarPermissaoAsync(int usuarioId, int varejistaId)
        {
            var usuarios = await _usuarioRepository.GetByVarejistaIdAsync(varejistaId);
            return usuarios.Any(u => u.Usuarios_Cod == usuarioId);
        }
    }
} 