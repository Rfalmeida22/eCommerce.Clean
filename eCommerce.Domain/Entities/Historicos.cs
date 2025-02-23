using eCommerce.Domain.Common;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerce.Domain.Entities
{
    public class Historicos : BaseEntity
    {
        public int Historicos_Cod { get; protected set; }
        public string Historicos_Aca { get; protected set; }
        public DateTime Historicos_Dat { get; protected set; }
        public string Historicos_Det { get; protected set; }
        public string Historicos_Tab { get; protected set; }
        public int IdEmpresa { get; protected set; }
        public int Usuarios_Cod { get; protected set; }

        protected Historicos() { }

        public Historicos(string action, string description, string tableName, int idEmpresa, int usuariosCod)
        {
            ValidarDados(action, description, tableName, idEmpresa, usuariosCod);

            Historicos_Aca = action;
            Historicos_Det = description;
            Historicos_Tab = tableName;
            IdEmpresa = idEmpresa;
            Usuarios_Cod = usuariosCod;
            Historicos_Dat = DateTime.UtcNow;
        }

        public ValidationResult Validar()
        {
            return ValidarDados(  Historicos_Aca, Historicos_Det, Historicos_Tab, IdEmpresa, Usuarios_Cod);
        }

        private ValidationResult ValidarDados(string action, string description, string tableName, int idEmpresa, int usuariosCod)
        {
            var erros = new List<string>();

            ValidateAction(action, erros);

            ValidateDescription(description, erros);

            ValidateTableName(tableName, erros);

            ValidateCompanyId(idEmpresa, erros);

            ValidateUserId(usuariosCod, erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private void ValidateAction(string action, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(action))
                errors.Add("Ação é obrigatória");

            if (!string.IsNullOrEmpty(action) && action.Length != 1)
                errors.Add("Ação deve ter exatamente 1 caractere");
        }

        private void ValidateDescription(string description, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(description))
                errors.Add("Descrição é obrigatória");
            
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
                errors.Add("Descrição deve ter no máximo 500 caracteres");
        }

        private void ValidateTableName(string tableName, List<string> errors)
        {
            if (!string.IsNullOrEmpty(tableName) && tableName.Length > 150)
                errors.Add("Tabela deve ter no máximo 150 caracteres");
        }

        private void ValidateCompanyId(int companyId, List<string> errors)
        {
            if (companyId < 0)
                errors.Add("IdEmpresa deve ser maior ou igual a zero");
        }

        private void ValidateUserId(int userId, List<string> errors)
        {
            if (userId < 0)
                errors.Add("Usuarios_Cod deve ser maior ou igual a zero");
        }

        public void AtualizarDados(string action, string description, string tableName, int idEmpresa, int usuariosCod)
        {
            ValidarDados(action, description, tableName, idEmpresa, usuariosCod);

            Acao = action;
            Descricao = description;
            NomeTabela = tableName;
            IdEmpresa = idEmpresa;
            IdUsuario = usuariosCod;
            UpdatedAt = DateTime.UtcNow;
        }

        // Value Object para Action
        public class HistoryAction : ValueObject
        {
            public string Value { get; }

            public static readonly string INSERT = "I";
            public static readonly string UPDATE = "U";
            public static readonly string DELETE = "D";

            private HistoryAction(string value)
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainException("Action é obrigatório");

                if (value.Length != 1)
                    throw new DomainException("Action é necessário ter ao menos 1 caractere");

                Value = value.ToUpper();
            }

            public static HistoryAction Create(string value)
                => new HistoryAction(value);

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }
    }
}
