namespace eCommerce.Domain.Specifications
{
    /// <summary>
    /// Especifica��o para a entidade Usuarios.
    /// Verifica se um usu�rio satisfaz os crit�rios de neg�cio definidos.
    /// </summary>
    public class Usuario : ISpecification<Entities.Usuarios>
    {
        /// <summary>
        /// Verifica se a entidade Usuarios satisfaz os crit�rios de neg�cio.
        /// </summary>
        /// <param name="usuario">Inst�ncia da entidade Usuarios a ser verificada.</param>
        /// <returns>True se a entidade satisfaz os crit�rios, False caso contr�rio.</returns>
        public bool IsSatisfiedBy(Entities.Usuarios usuario)
        {
            return usuario.Usuarios_Ati
                && !string.IsNullOrEmpty(usuario.Usuarios_Ema)
                && !string.IsNullOrEmpty(usuario.Usuarios_Sen);
        }
    }
} 