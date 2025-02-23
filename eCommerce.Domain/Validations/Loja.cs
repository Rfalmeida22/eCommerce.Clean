namespace eCommerce.Domain.Validations
{
    public class Loja : Validation
    {
        private readonly Lojas _loja;

        public Loja(Lojas loja)
        {
            _loja = loja;

            ValidateRequired(_loja.NmLoja, "Nome");
            ValidateMaxLength(_loja.NmLoja, 100, "Nome");
            ValidateCNPJ(_loja.CdCnpj, "CNPJ");
            ValidateMaxLength(_loja.CdLoja, 50, "Código");
            ValidateMaxLength(_loja.TxEndereco, 255, "Endereço");
        }
    }
} 