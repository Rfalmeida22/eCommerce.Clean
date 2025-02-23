namespace eCommerce.Domain.Specifications
{
    public class Usuario : ISpecification<Entities.Usuarios>
    {
        public bool IsSatisfiedBy(Entities.Usuarios usuario)
        {
            return usuario.Usuarios_Ati 
                && !string.IsNullOrEmpty(usuario.Usuarios_Ema)
                && !string.IsNullOrEmpty(usuario.Usuarios_Sen);
        }
    }
} 