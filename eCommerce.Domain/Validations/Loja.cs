using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Validations
{
    public class Loja : Validation
    {
        private readonly Entities.Lojas _loja;

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Loja.
        /// </summary>
        /// <param name="loja"></param>
        public Loja(Entities.Lojas loja)
        {
            _loja = loja;

        }

        /// <summary>
        /// Inicializa uma nova instância da classe de validação Loja.
        /// </summary>
        public void Validate()
        {
            ValidateRequired(_loja.NmLoja, "Nome");
            ValidateMaxLength(_loja.NmLoja, 100, "Nome");
            ValidateCNPJ(_loja.CdCnpj, "CNPJ");
            ValidateMaxLength(_loja.CdLoja, 50, "Código");
            ValidateMaxLength(_loja.TxEndereco, 255, "Endereço");
            ValidateGreaterThanOrEqual(_loja.IdLojista, 0, "IdLojista");
            ValidateGreaterThanOrEqual(_loja.IdVarejista, 0, "IdVarejista");
        }
    }
} 