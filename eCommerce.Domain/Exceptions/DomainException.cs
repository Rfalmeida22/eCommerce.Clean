using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Exceptions
{
    /// <summary>
    /// Tipos de erros de domínio
    /// </summary>
    public enum DomainErrorType
    {
        /// <summary>
        /// Erro de validação de dados
        /// </summary>
        Validation,

        /// <summary>
        /// Violação de regra de negócio
        /// </summary>
        BusinessRule,

        /// <summary>
        /// Entidade não encontrada
        /// </summary>
        NotFound,

        /// <summary>
        /// Conflito de dados
        /// </summary>
        Conflict
    }

    /// <summary>
    /// Exceção personalizada para erros de domínio
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Nome do domínio onde ocorreu o erro
        /// </summary>
        public string Domain { get; }

        /// <summary>
        /// Nome da entidade relacionada ao erro
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Identificador da entidade
        /// </summary>
        public object EntityId { get; }

        /// <summary>
        /// Tipo do erro de domínio
        /// </summary>
        public DomainErrorType ErrorType { get; }

        /// <summary>
        /// Construtor básico para erros de validação
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public DomainException(string message)
            : base(message)
        {
            ErrorType = DomainErrorType.Validation;
        }

        /// <summary>
        /// Construtor completo para erros de domínio
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="domain">Nome do domínio</param>
        /// <param name="entityName">Nome da entidade</param>
        /// <param name="entityId">ID da entidade</param>
        /// <param name="errorType">Tipo do erro</param>
        public DomainException(
            string message = null,
            string domain = null,
            string entityName = null,
            object entityId = null,
            DomainErrorType errorType = DomainErrorType.Validation)
            : base(message)
        {
            Domain = domain;
            EntityName = entityName;
            EntityId = entityId;
            ErrorType = errorType;
        }

        /// <summary>
        /// Construtor para erros com exceção interna
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="innerException">Exceção interna</param>
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorType = DomainErrorType.Validation;
        }

        /// <summary>
        /// Cria uma exceção para entidade não encontrada
        /// </summary>
        /// <param name="entityName">Nome da entidade</param>
        /// <param name="id">ID da entidade</param>
        /// <returns>Nova instância de DomainException</returns>
        public static DomainException NotFound(string entityName, object id)
        {
            return new DomainException(
                message: $"Entidade {entityName} com id {id} não encontrada",
                entityName: entityName,
                entityId: id,
                errorType: DomainErrorType.NotFound);
        }

        /// <summary>
        /// Cria uma exceção para violação de regra de negócio
        /// </summary>
        /// <param name="message">Mensagem descrevendo a violação</param>
        /// <returns>Nova instância de DomainException</returns>
        public static DomainException BusinessRule(string message)
        {
            return new DomainException(
                message: message,
                errorType: DomainErrorType.BusinessRule);
        }

        /// <summary>
        /// Cria uma exceção para conflito de dados
        /// </summary>
        /// <param name="entityName">Nome da entidade</param>
        /// <param name="message">Mensagem descrevendo o conflito</param>
        /// <returns>Nova instância de DomainException</returns>
        public static DomainException Conflict(string entityName, string message)
        {
            return new DomainException(
                message: message,
                entityName: entityName,
                errorType: DomainErrorType.Conflict);
        }

        /// <summary>
        /// Retorna uma representação string da exceção
        /// </summary>
        public override string ToString()
        {
            var details = new StringBuilder();
            details.AppendLine($"Tipo do Erro: {ErrorType}");
            
            if (!string.IsNullOrEmpty(Domain))
                details.AppendLine($"Domínio: {Domain}");
            
            if (!string.IsNullOrEmpty(EntityName))
                details.AppendLine($"Entidade: {EntityName}");
            
            if (EntityId != null)
                details.AppendLine($"ID: {EntityId}");
            
            details.AppendLine($"Mensagem: {Message}");

            return details.ToString();
        }
    }
}
