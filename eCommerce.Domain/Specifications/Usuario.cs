namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especificação para a entidade Usuarios.
    /// Verifica se um usuário satisfaz os critérios de negócio definidos.
    /// </summary>
    public class Usuario : ISpecification<Entities.Usuarios>
    {
        /// <summary>
        /// Verifica se a entidade Usuarios satisfaz os critérios de negócio.
        /// </summary>
        /// <param name="usuario">Instância da entidade Usuarios a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os critérios, False caso contrário.</returns>
        public bool IsSatisfiedBy(Entities.Usuarios usuario)
        {
            return usuario.Usuarios_Ati
                && !string.IsNullOrEmpty(usuario.Usuarios_Ema)
                && !string.IsNullOrEmpty(usuario.Usuarios_Sen);
        }
    }
} 