using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class Lojas : BaseEntity
    {
        public int IdLoja { get; protected set; }
        public string CdCnpj { get; protected set; }
        public string CdLoja { get; protected set; }
        public int IdLojista { get; protected set; }
        public int IdVarejista { get; protected set; }
        public string NmLoja { get; protected set; }
        public string TxEndereco { get; protected set; }

        protected Lojas() { }

        public Lojas(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco)
        {
            ValidarDados(cdCnpj, cdLoja, idLojista, idVarejista, nmLoja, txEndereco);

            CdCnpj = cdCnpj;
            CdLoja = cdLoja;
            IdLojista = idLojista;
            IdVarejista = idVarejista;
            NmLoja = nmLoja;
            TxEndereco = txEndereco;
        }

        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoDados = ValidarDados(
                CdCnpj,
                CdLoja,
                IdLojista,
                IdVarejista,
                NmLoja,
                TxEndereco);

            if (!resultadoDados.EhValido)
                erros.AddRange(resultadoDados.Erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarDados(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco)
        {
            var erros = new List<string>();

            if (!string.IsNullOrEmpty(cdCnpj) && cdCnpj.Length > 50)
                erros.Add("CdCnpj deve ter no máximo 50 caracteres");

            if (!string.IsNullOrEmpty(cdLoja) && cdLoja.Length > 50)
                erros.Add("CdLoja deve ter no máximo 50 caracteres");

            if (idLojista < 0)
                erros.Add("IdLojista deve ser maior ou igual a zero");

            if (idVarejista < 0)
                erros.Add("IdVarejista deve ser maior ou igual a zero");

            if (string.IsNullOrWhiteSpace(nmLoja))
                erros.Add("NmLoja é obrigatório");

            if (!string.IsNullOrEmpty(nmLoja) && nmLoja.Length > 100)
                erros.Add("NmLoja deve ter no máximo 100 caracteres");

            if (!string.IsNullOrEmpty(txEndereco) && txEndereco.Length > 255)
                erros.Add("TxEndereco deve ter no máximo 255 caracteres");

            return new ValidationResult(erros.Count == 0, erros);
        }

        public void AtualizarDados(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco)
        {
            ValidarDados(cdCnpj, cdLoja, idLojista, idVarejista, nmLoja, txEndereco);

            CdCnpj = cdCnpj;
            CdLoja = cdLoja;
            IdLojista = idLojista;
            IdVarejista = idVarejista;
            NmLoja = nmLoja;
            TxEndereco = txEndereco;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
