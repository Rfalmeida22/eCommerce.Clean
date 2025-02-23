using System.Text.RegularExpressions;

namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe est�tica para valida��o de endere�os de e-mail.
    /// </summary>
    public static class Email
    {
        private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        /// <summary>
        /// Valida se um endere�o de e-mail � v�lido.
        /// </summary>
        /// <param name="email">Endere�o de e-mail a ser validado.</param>
        /// <returns>True se o endere�o de e-mail for v�lido, caso contr�rio, False.</returns>
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, EMAIL_REGEX);
        }
    }
} 