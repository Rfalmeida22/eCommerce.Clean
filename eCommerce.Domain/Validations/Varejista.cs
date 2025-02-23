namespace eCommerce.Domain.Validations
{
    public class Varejista : Validation
    {
        private readonly Entities.Varejista _varejista;

        public Varejista(Entities.Varejista varejista)
        {
            _varejista = varejista;

            ValidateRequired(_varejista.NmVarejista, "Nome");
            ValidateMaxLength(_varejista.NmVarejista, 100, "Nome");
            ValidateCNPJ(_varejista.CdCnpj, "CNPJ");
            ValidateMaxLength(_varejista.CdBanner, 50, "Banner");
            ValidateMaxLength(_varejista.CdCorFundo, 50, "Cor de Fundo");
            ValidateMaxLength(_varejista.CdVarejista, 50, "Código");
            ValidateMaxLength(_varejista.TxLinkSite, 255, "Link do Site");
        }
    }
} 