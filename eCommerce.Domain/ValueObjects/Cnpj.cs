using System.Text.RegularExpressions;

namespace eCommerce.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa um CNPJ
    /// </summary>
    public class Cnpj : ValueObject
    {
        /// <summary>
        /// Valor do CNPJ
        /// </summary>
        public string Value { get; }

        private Cnpj(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Cria uma nova instância de CNPJ
        /// </summary>
        public static Cnpj Create(string value)
        {
            if (!IsValid(value))
                throw new DomainException("CNPJ inválido");

            return new Cnpj(value);
        }

        private static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove caracteres não numéricos
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            if (cnpj.Length != 14)
                return false;

            // Implementar validação completa de CNPJ aqui
            return true;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
} 