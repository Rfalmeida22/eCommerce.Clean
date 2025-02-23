
namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe de validação para a entidade Historicos.
    /// </summary>
    public class Historicos : Validation
    {
        private readonly Entities.Historicos _historicos;

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Historicos.
        /// </summary>
        /// <param name="historicos">Instância da entidade Historicos a ser validada.</param>
        public Historicos(Entities.Historicos historicos)
        {
            _historicos = historicos;

        }

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Historicos.
        /// </summary>
        public void Validate()
        {
            // Valida se a ação é obrigatória
            ValidateRequired(_historicos.Historicos_Aca, "Ação");
            // Valida o comprimento exato da ação
            ValidateMaxLength(_historicos.Historicos_Aca, 1, "Ação");
            // Valida se a descrição é obrigatória
            ValidateRequired(_historicos.Historicos_Det, "Descrição");
            // Valida o comprimento máximo da descrição
            ValidateMaxLength(_historicos.Historicos_Det, 500, "Descrição");
            // Valida o comprimento máximo do nome da tabela
            ValidateMaxLength(_historicos.Historicos_Tab, 150, "Tabela");
            // Valida se o IdEmpresa é maior ou igual a zero
            ValidateGreaterThanOrEqual(_historicos.IdEmpresa, 0, "IdEmpresa");
            // Valida se o Usuarios_Cod é maior ou igual a zero
            ValidateGreaterThanOrEqual(_historicos.Usuarios_Cod, 0, "Usuarios_Cod");
        }


    }
}
