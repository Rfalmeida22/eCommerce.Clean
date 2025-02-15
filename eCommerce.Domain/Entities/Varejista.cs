using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class Varejista : BaseEntity
    {
        public int IdVarejista { get; protected set; }
        public string CdBanner { get; protected set; }
        public string CdCorFundo { get; protected set; }
        public string CdVarejista { get; protected set; }
        public string NmVarejista { get; protected set; }
        public string TxLinkSite { get; protected set; }

        protected Varejista() { }

        public Varejista(
            string cdBanner,
            string cdCorFundo,
            string cdVarejista,
            string nmVarejista,
            string txLinkSite)
        {
            ValidarDados(cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite);

            CdBanner = cdBanner;
            CdCorFundo = cdCorFundo;
            CdVarejista = cdVarejista;
            NmVarejista = nmVarejista;
            TxLinkSite = txLinkSite;
        }

        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoDados = ValidarDados(
                CdBanner,
                CdCorFundo,
                CdVarejista,
                NmVarejista,
                TxLinkSite);

            if (!resultadoDados.EhValido)
                erros.AddRange(resultadoDados.Erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private ValidationResult ValidarDados(
            string cdBanner,
            string cdCorFundo,
            string cdVarejista,
            string nmVarejista,
            string txLinkSite)
        {
            var erros = new List<string>();

            if (!string.IsNullOrEmpty(cdBanner) && cdBanner.Length > 50)
                erros.Add("CdBanner deve ter no máximo 50 caracteres");

            if (!string.IsNullOrEmpty(cdCorFundo) && cdCorFundo.Length > 50)
                erros.Add("CdCorFundo deve ter no máximo 50 caracteres");

            if (!string.IsNullOrEmpty(cdVarejista) && cdVarejista.Length > 50)
                erros.Add("CdVarejista deve ter no máximo 50 caracteres");

            if (string.IsNullOrWhiteSpace(nmVarejista))
                erros.Add("NmVarejista é obrigatório");

            if (!string.IsNullOrEmpty(nmVarejista) && nmVarejista.Length > 100)
                erros.Add("NmVarejista deve ter no máximo 100 caracteres");

            if (!string.IsNullOrEmpty(txLinkSite) && txLinkSite.Length > 255)
                erros.Add("TxLinkSite deve ter no máximo 255 caracteres");

            return new ValidationResult(erros.Count == 0, erros);
        }

        public void AtualizarDados(
            string cdBanner,
            string cdCorFundo,
            string cdVarejista,
            string nmVarejista,
            string txLinkSite)
        {
            ValidarDados(cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite);

            CdBanner = cdBanner;
            CdCorFundo = cdCorFundo;
            CdVarejista = cdVarejista;
            NmVarejista = nmVarejista;
            TxLinkSite = txLinkSite;

            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
