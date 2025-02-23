using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.ValueObjects
{
    /// <summary>
    /// Classe base abstrata para implementação de Value Objects
    /// seguindo os princípios do Domain-Driven Design (DDD)
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Método abstrato que deve ser implementado pelas classes derivadas
        /// para fornecer os componentes que determinam a igualdade do objeto
        /// </summary>
        /// <returns>Coleção de objetos que compõem a identidade do Value Object</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Sobrescreve o método Equals para comparação de Value Objects
        /// </summary>
        /// <param name="obj">Objeto a ser comparado</param>
        /// <returns>True se os objetos são iguais, False caso contrário</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Sobrescreve o método GetHashCode para garantir que objetos iguais
        /// tenham o mesmo hash code
        /// </summary>
        /// <returns>Hash code baseado nos componentes de igualdade</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Operador de igualdade para Value Objects
        /// </summary>
        /// <param name="left">Objeto da esquerda</param>
        /// <param name="right">Objeto da direita</param>
        /// <returns>True se os objetos são iguais, False caso contrário</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Operador de desigualdade para Value Objects
        /// </summary>
        /// <param name="left">Objeto da esquerda</param>
        /// <param name="right">Objeto da direita</param>
        /// <returns>True se os objetos são diferentes, False caso contrário</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Cria uma cópia do Value Object
        /// </summary>
        /// <returns>Uma nova instância com os mesmos valores</returns>
        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }
    }
}
