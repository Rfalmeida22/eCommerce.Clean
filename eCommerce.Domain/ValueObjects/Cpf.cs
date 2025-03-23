using eCommerce.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace eCommerce.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa um CPF
    /// </summary>
    public class Cpf : ValueObject
    {
        /// <summary>
        /// Valor do CPF
        /// </summary>
        public string Value { get; }

        private Cpf(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Cria uma nova instância de CPF
        /// </summary>
        public static Cpf Create(string value)
        {
            if (!IsValid(value))
                throw new DomainException("CPF inválido");

            return new Cpf(value);
        }

        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11)
                return false;

            // Implementar validação completa de CPF aqui
            return true;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
} 