namespace eCommerce.Domain.Validations
{
    /// <summary>
    /// Classe de validação para a entidade Varejista.
    /// </summary>
    public class Varejista : Validation
    {
        private readonly Entities.Varejista _varejista;

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Varejista.
        /// </summary>
        /// <param name="varejista">Instância da entidade Varejista a ser validada.</param>
        public Varejista(Entities.Varejista varejista)
        {
            _varejista = varejista;
        }

        /// <summary>
        /// Executa todas as validações para a entidade Varejista.
        /// </summary>
        public void Validate()
        {
            // Valida se o nome do varejista é obrigatório
            ValidateRequired(_varejista.NmVarejista, "Nome");
            // Valida o comprimento máximo do nome do varejista
            ValidateMaxLength(_varejista.NmVarejista, 100, "Nome");
            // Valida se o CNPJ é válido
            ValidateCNPJ(_varejista.CdCnpj, "CNPJ");
            // Valida o comprimento máximo do código do banner
            ValidateMaxLength(_varejista.CdBanner, 50, "Banner");
            // Valida o comprimento máximo do código da cor de fundo
            ValidateMaxLength(_varejista.CdCorFundo, 50, "Cor de Fundo");
            // Valida o comprimento máximo do código do varejista
            ValidateMaxLength(_varejista.CdVarejista, 50, "Código");
            // Valida o comprimento máximo do link do site
            ValidateMaxLength(_varejista.TxLinkSite, 255, "Link do Site");
        }
    }
} 