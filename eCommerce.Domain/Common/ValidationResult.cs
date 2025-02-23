using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerce.Domain.Common
{
    /// <summary>
    /// Classe que representa o resultado de uma validação de domínio
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Indica se a validação foi bem-sucedida
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Coleção somente leitura contendo as mensagens de erro da validação
        /// </summary>
        public IReadOnlyCollection<string> Errors { get; }

        /// <summary>
        /// Construtor que inicializa um novo resultado de validação
        /// </summary>
        /// <param name="isValid">Indica se é válido</param>
        /// <param name="errors">Coleção de mensagens de erro</param>
        public ValidationResult(bool isValid, IEnumerable<string> errors)
        {
            IsValid = isValid;
            Errors = errors?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
        }

        /// <summary>
        /// Cria um resultado de validação bem-sucedido
        /// </summary>
        /// <returns>ValidationResult indicando sucesso sem erros</returns>
        public static ValidationResult Success()
            => new ValidationResult(true, new List<string>());

        /// <summary>
        /// Cria um resultado de validação com falha contendo uma única mensagem de erro
        /// </summary>
        /// <param name="error">Mensagem de erro</param>
        /// <returns>ValidationResult indicando falha com a mensagem de erro especificada</returns>
        public static ValidationResult Failure(string error)
            => new ValidationResult(false, new[] { error });

        /// <summary>
        /// Cria um resultado de validação com falha contendo múltiplas mensagens de erro
        /// </summary>
        /// <param name="errors">Coleção de mensagens de erro</param>
        /// <returns>ValidationResult indicando falha com as mensagens de erro especificadas</returns>
        public static ValidationResult Failure(IEnumerable<string> errors)
            => new ValidationResult(false, errors);

        /// <summary>
        /// Combina múltiplos resultados de validação em um único resultado
        /// </summary>
        /// <param name="validationResults">Resultados a serem combinados</param>
        /// <returns>Novo ValidationResult combinando todos os resultados</returns>
        public static ValidationResult Combine(params ValidationResult[] validationResults)
        {
            var isValid = validationResults.All(r => r.IsValid);
            var errors = validationResults
                .SelectMany(r => r.Errors)
                .ToList();
            
            return new ValidationResult(isValid, errors);
        }

        /// <summary>
        /// Verifica se contém um erro específico
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro a ser procurada</param>
        /// <returns>True se a mensagem de erro existe, False caso contrário</returns>
        public bool ContainsError(string errorMessage)
            => Errors.Contains(errorMessage);

        /// <summary>
        /// Retorna uma string representando o resultado da validação
        /// </summary>
        public override string ToString()
            => IsValid 
                ? "Validation Successful" 
                : $"Validation Failed: {string.Join(", ", Errors)}";
    }
}
