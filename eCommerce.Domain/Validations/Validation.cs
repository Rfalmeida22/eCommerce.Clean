using eCommerce.Domain.Common;

namespace eCommerce.Domain.Validations
{
    public abstract class Validation
    {
        private readonly List<string> _errors = new();
        
        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        protected void ValidateCPF(string cpf, string campo)
        {
            if (!Document.ValidateCPF(cpf))
                AddError($"{campo} inválido");
        }

        protected void ValidateCNPJ(string cnpj, string campo)
        {
            if (!Document.ValidateCNPJ(cnpj))
                AddError($"{campo} inválido");
        }

        protected void ValidateEmail(string email, string campo)
        {
            if (!Email.ValidateEmail(email))
                AddError($"{campo} inválido");
        }

        protected void ValidateRequired(string value, string campo)
        {
            if (string.IsNullOrWhiteSpace(value))
                AddError($"{campo} é obrigatório");
        }

        protected void ValidateMaxLength(string value, int maxLength, string campo)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
                AddError($"{campo} deve ter no máximo {maxLength} caracteres");
        }

        public ValidationResult GetValidationResult()
        {
            return new ValidationResult(_errors.Count == 0, _errors);
        }
    }
} 