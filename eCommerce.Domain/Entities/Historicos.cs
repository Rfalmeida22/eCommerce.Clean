using eCommerce.Domain.Common;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.ValueObjects;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Classe que representa os históricos de ações realizadas no sistema.
    /// </summary>
    public class Historicos : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Código do histórico.
        /// </summary>
        public int Historicos_Cod { get; protected set; }

        /// <summary>
        /// Ação realizada.
        /// </summary>
        public string Historicos_Aca { get; protected set; }

        /// <summary>
        /// Data da ação.
        /// </summary>
        public DateTime Historicos_Dat { get; protected set; }

        /// <summary>
        /// Detalhes da ação.
        /// </summary>
        public string Historicos_Det { get; protected set; }

        /// <summary>
        /// Nome da tabela afetada.
        /// </summary>
        public string Historicos_Tab { get; protected set; }

        /// <summary>
        /// Código da empresa.
        /// </summary>
        public int IdEmpresa { get; protected set; }

        /// <summary>
        /// Código do usuário que realizou a ação.
        /// </summary>
        public int Usuarios_Cod { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor protegido para uso do Entity Framework.
        /// </summary>
        protected Historicos() { }

        /// <summary>
        /// Construtor para criar um novo histórico.
        /// </summary>
        /// <param name="action">Ação realizada.</param>
        /// <param name="description">Descrição da ação.</param>
        /// <param name="tableName">Nome da tabela afetada.</param>
        /// <param name="idEmpresa">Código da empresa.</param>
        /// <param name="usuariosCod">Código do usuário que realizou a ação.</param>
        public Historicos(string action, string description, string tableName, int idEmpresa, int usuariosCod)
        {

            Historicos_Aca = action;
            Historicos_Det = description;
            Historicos_Tab = tableName;
            IdEmpresa = idEmpresa;
            Usuarios_Cod = usuariosCod;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

            Historicos_Dat = DateTime.UtcNow;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Valida os dados do histórico.
        /// </summary>
        /// <returns>Resultado da validação.</returns>
        public ValidationResult Validar()
        {
            var validation = new Validations.Historicos(this);
            validation.Validate();
            return validation.GetValidationResult();

        }


        /// <summary>
        /// Atualiza os dados do histórico.
        /// </summary>
        /// <param name="action">Ação realizada.</param>
        /// <param name="description">Descrição da ação.</param>
        /// <param name="tableName">Nome da tabela afetada.</param>
        /// <param name="idEmpresa">Código da empresa.</param>
        /// <param name="usuariosCod">Código do usuário que realizou a ação.</param>
        public void AtualizarDados(string action, string description, string tableName, int idEmpresa, int usuariosCod)
        {
            Historicos_Aca = action;
            Historicos_Det = description;
            Historicos_Tab = tableName;
            IdEmpresa = idEmpresa;
            Usuarios_Cod = usuariosCod;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

            Historicos_Dat = DateTime.UtcNow;
        }
        #endregion
    }
}
