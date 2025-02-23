using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Domain.Events;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um varejista no sistema
    /// </summary>
    public class Varejista : BaseEntity
    {
        private const int BANNER_MAX_LENGTH = 50;
        private const int COR_FUNDO_MAX_LENGTH = 50;
        private const int CODIGO_MAX_LENGTH = 50;
        private const int NOME_MAX_LENGTH = 100;
        private const int LINK_SITE_MAX_LENGTH = 255;
        private const string NOME_REQUIRED_MESSAGE = "Nome do varejista é obrigatório";

        /// <summary>
        /// Identificador único do varejista
        /// </summary>
        public int IdVarejista { get; protected set; }

        /// <summary>
        /// CNPJ do varejista
        /// </summary>
        public string CdCnpj 
        { 
            get => Cnpj?.Value; 
            protected set => Cnpj = Cnpj.Create(value); 
        }

        // Value Object interno
        private Cnpj Cnpj { get; set; }

        /// <summary>
        /// Código do banner do varejista
        /// </summary>
        public string CdBanner { get; protected set; }

        /// <summary>
        /// Código da cor de fundo
        /// </summary>
        public string CdCorFundo { get; protected set; }

        /// <summary>
        /// Código do varejista
        /// </summary>
        public string CdVarejista { get; protected set; }

        /// <summary>
        /// Nome do varejista
        /// </summary>
        public string NmVarejista { get; protected set; }

        /// <summary>
        /// Link do site do varejista
        /// </summary>
        public string TxLinkSite { get; protected set; }

        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected Varejista() { }

        /// <summary>
        /// Cria uma nova instância de Varejista
        /// </summary>
        public Varejista(
            string cdCnpj,
            string cdBanner,
            string cdCorFundo,
            string cdVarejista,
            string nmVarejista,
            string txLinkSite)
        {
            ValidarDados(cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite);
            
            // Usando Value Object
            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object

            CdBanner = cdBanner;
            CdCorFundo = cdCorFundo;
            CdVarejista = cdVarejista;
            NmVarejista = nmVarejista;
            TxLinkSite = txLinkSite;
        }

        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var erros = new List<string>();

            try
            {
                // Revalidar usando Value Object
                if (!string.IsNullOrEmpty(CdCnpj))
                    Cnpj = Cnpj.Create(CdCnpj);

                ValidarDados(CdBanner, CdCorFundo, CdVarejista, NmVarejista, TxLinkSite);
            }
            catch (DomainException ex)
            {
                erros.Add(ex.Message);
            }

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

            ValidateBanner(cdBanner, erros);
            ValidateCorFundo(cdCorFundo, erros);
            ValidateCodigo(cdVarejista, erros);
            ValidateNome(nmVarejista, erros);
            ValidateLinkSite(txLinkSite, erros);

            return new ValidationResult(erros.Count == 0, erros);
        }

        private void ValidateBanner(string banner, List<string> errors)
        {
            if (!string.IsNullOrEmpty(banner) && banner.Length > BANNER_MAX_LENGTH)
                errors.Add($"Banner deve ter no máximo {BANNER_MAX_LENGTH} caracteres");
        }

        private void ValidateCorFundo(string corFundo, List<string> errors)
        {
            if (!string.IsNullOrEmpty(corFundo) && corFundo.Length > COR_FUNDO_MAX_LENGTH)
                errors.Add($"Cor de fundo deve ter no máximo {COR_FUNDO_MAX_LENGTH} caracteres");

            if (!string.IsNullOrEmpty(corFundo) && !IsValidHexColor(corFundo))
                errors.Add("Cor de fundo deve ser um código hexadecimal válido");
        }

        private void ValidateCodigo(string codigo, List<string> errors)
        {
            if (!string.IsNullOrEmpty(codigo) && codigo.Length > CODIGO_MAX_LENGTH)
                errors.Add($"Código do varejista deve ter no máximo {CODIGO_MAX_LENGTH} caracteres");
        }

        private void ValidateNome(string nome, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(nome))
                errors.Add(NOME_REQUIRED_MESSAGE);

            if (!string.IsNullOrEmpty(nome) && nome.Length > NOME_MAX_LENGTH)
                errors.Add($"Nome do varejista deve ter no máximo {NOME_MAX_LENGTH} caracteres");
        }

        private void ValidateLinkSite(string link, List<string> errors)
        {
            if (!string.IsNullOrEmpty(link))
            {
                if (link.Length > LINK_SITE_MAX_LENGTH)
                    errors.Add($"Link do site deve ter no máximo {LINK_SITE_MAX_LENGTH} caracteres");

                if (!Uri.TryCreate(link, UriKind.Absolute, out _))
                    errors.Add("Link do site deve ser uma URL válida");
            }
        }

        private bool IsValidHexColor(string color)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(color, "^#(?:[0-9a-fA-F]{3}){1,2}$");
        }

        /// <summary>
        /// Atualiza os dados do varejista
        /// </summary>
        public void AtualizarDados(
            string cdCnpj,
            string cdBanner,
            string cdCorFundo,
            string cdVarejista,
            string nmVarejista,
            string txLinkSite,
            string updatedBy)
        {
            ValidarDados(cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite);

            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object
            CdBanner = cdBanner;
            CdCorFundo = cdCorFundo;
            CdVarejista = cdVarejista;
            NmVarejista = nmVarejista;
            TxLinkSite = txLinkSite;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Cria uma nova instância de Varejista
        /// </summary>
        public static Varejista Create(
            string cdCnpj,
            string cdBanner,
            string cdCorFundo,
            string cdVarejista,
            string nmVarejista,
            string txLinkSite,
            string createdBy)
        {
            var varejista = new Varejista(cdCnpj, cdBanner, cdCorFundo, cdVarejista, nmVarejista, txLinkSite);
            varejista.SetCreatedBy(createdBy);

            // Adicionar evento
            varejista.AddDomainEvent(new Varejista(
                varejista.IdVarejista,
                varejista.NmVarejista,
                varejista.CdCnpj,
                createdBy));

            return varejista;
        }

        /// <summary>
        /// Coleção de lojas do varejista
        /// </summary>
        public virtual ICollection<Lojas> Lojas { get; protected set; }

        /// <summary>
        /// Coleção de relacionamentos com brokers
        /// </summary>
        public virtual ICollection<Brokers_Varejistas> BrokersVarejistas { get; protected set; }
    }
}
