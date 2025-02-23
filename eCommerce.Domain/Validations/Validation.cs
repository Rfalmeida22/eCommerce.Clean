using eCommerce.Domain.Common;

namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe abstrata que fornece métodos de validação genéricos.
    /// </summary>
    public abstract class Validation
    {
        private readonly List<string> _errors = new();

        /// <summary>
        /// Adiciona uma mensagem de erro à lista de erros.
        /// </summary>
        /// <param name="error">Mensagem de erro a ser adicionada.</param>
        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        /// <summary>
        /// Valida se o CPF é válido.
        /// </summary>
        /// <param name="cpf">CPF a ser validado.</param>
        /// <param name="campo">Nome do campo para a mensagem de erro.</param>
        protected void ValidateCPF(string cpf, string campo)
        {
            if (!Document.ValidateCPF(cpf))
                AddError($"{campo} inválido");
        }

        /// <summary>
        /// Valida se o CNPJ é válido.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado.</param>
        /// <param name="campo">Nome do campo para a mensagem de erro.</param>
        protected void ValidateCNPJ(string cnpj, string campo)
        {
            if (!Document.ValidateCNPJ(cnpj))
                AddError($"{campo} inválido");
        }

        /// <summary>
        /// Valida se o e-mail é válido.
        /// </summary>
        /// <param name="email">E-mail a ser validado.</param>
        /// <param name="campo">Nome do campo para a mensagem de erro.</param>
        protected void ValidateEmail(string email, string campo)
        {
            if (!Email.ValidateEmail(email))
                AddError($"{campo} inválido");
        }

        /// <summary>
        /// Valida se o valor é obrigatório.
        /// </summary>
        /// <param name="value">Valor a ser validado.</param>
        /// <param name="campo">Nome do campo para a mensagem de erro.</param>
        protected void ValidateRequired(string value, string campo)
        {
            if (string.IsNullOrWhiteSpace(value))
                AddError($"{campo} é obrigatório");
        }

        /// <summary>
        /// Valida se o comprimento do valor não excede o comprimento máximo permitido.
        /// </summary>
        /// <param name="value">Valor a ser validado.</param>
        /// <param name="maxLength">Comprimento máximo permitido.</param>
        /// <param name="campo">Nome do campo para a mensagem de erro.</param>
        protected void ValidateMaxLength(string value, int maxLength, string campo)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
                AddError($"{campo} deve ter no máximo {maxLength} caracteres");
        }

        /// <summary>
        /// Valida se o valor é maior ou igual ao valor mínimo permitido.
        /// </summary>
        /// <param name="value">Valor a ser validado.</param>
        /// <param name="minValue">Valor mínimo permitido.</param>
        /// <param name="campo">Nome do campo para a mensagem de erro.</param>
        protected void ValidateGreaterThanOrEqual(int value, int minValue, string campo)
        {
            if (value < minValue)
                AddError($"{campo} deve ser maior ou igual a {minValue}");
        }

        /// <summary>
        /// Obtém o resultado da validação.
        /// </summary>
        /// <returns>Resultado da validação contendo a lista de erros, se houver.</returns>
        public ValidationResult GetValidationResult()
        {
            return new ValidationResult(_errors.Count == 0, _errors);
        }
    }
} 