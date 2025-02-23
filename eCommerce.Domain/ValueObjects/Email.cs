using eCommerce.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace eCommerce.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa um Email
    /// </summary>
    public class Email : ValueObject
    {
        private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        /// <summary>
        /// Endereço de email
        /// </summary>
        public string Address { get; }

        private Email(string address)
        {
            Address = address;
        }

        /// <summary>
        /// Cria uma nova instância de Email
        /// </summary>
        public static Email Create(string address)
        {
            if (!IsValid(address))
                throw new DomainException("Email inválido");

            return new Email(address);
        }

        private static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, EMAIL_REGEX);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }

        public override string ToString() => Address;
    }
} 