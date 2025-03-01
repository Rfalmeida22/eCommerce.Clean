using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Domain.Events;
using eCommerce.Domain.ValueObjects;
using eCommerce.Domain.Exceptions;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um varejista no sistema
    /// </summary>
    public class Varejista : BaseEntity
    {
        #region Properties
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
        #endregion

        #region Constructors
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
            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object
            CdBanner = cdBanner;
            CdCorFundo = cdCorFundo;
            CdVarejista = cdVarejista;
            NmVarejista = nmVarejista;
            TxLinkSite = txLinkSite;
            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var validation = new Validations.Varejista(this);
            validation.Validate();
            return validation.GetValidationResult();
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
            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object
            CdBanner = cdBanner;
            CdCorFundo = cdCorFundo;
            CdVarejista = cdVarejista;
            NmVarejista = nmVarejista;
            TxLinkSite = txLinkSite;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

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
            varejista.AddDomainEvent(new Events.Varejista(
                varejista.IdVarejista,
                varejista.NmVarejista,
                varejista.CdCnpj,
                createdBy));

            return varejista;
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Coleção de lojas do varejista
        /// </summary>
        public virtual ICollection<Lojas> Lojas { get; protected set; }

        /// <summary>
        /// Coleção de relacionamentos com brokers
        /// </summary>
        public virtual ICollection<Brokers_Varejistas> BrokersVarejistas { get; protected set; }
        #endregion
    }
}
