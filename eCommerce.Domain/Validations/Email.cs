using System.Text.RegularExpressions;

namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe estática para validação de endereços de e-mail.
    /// </summary>
    public static class Email
    {
        private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        /// <summary>
        /// Valida se um endereço de e-mail é válido.
        /// </summary>
        /// <param name="email">Endereço de e-mail a ser validado.</param>
        /// <returns>True se o endereço de e-mail for válido, caso contrário, False.</returns>
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, EMAIL_REGEX);
        }
    }
} 