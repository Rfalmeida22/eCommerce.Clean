namespace eCommerce.Domain.Validations
{
    public class Usuario : Validation
    {
        private readonly Usuarios _usuario;

        public Usuario(Usuarios usuario)
        {
            _usuario = usuario;

            ValidateRequired(_usuario.Usuarios_Nom, "Nome");
            ValidateMaxLength(_usuario.Usuarios_Nom, 100, "Nome");
            ValidateEmail(_usuario.Usuarios_Ema, "Email");
            ValidateRequired(_usuario.Usuarios_Sen, "Senha");
            ValidateMaxLength(_usuario.Usuarios_Sen, 200, "Senha");
            ValidateCPF(_usuario.Usuarios_Cpf, "CPF");
        }
    }
} 