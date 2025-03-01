using eCommerce.Domain.Common;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma loja no sistema
    /// </summary>
    public class Lojas : BaseEntity
    {

        #region Properties
        /// <summary>
        /// Identificador único da loja
        /// </summary>
        public int IdLoja { get; protected set; }

        /// <summary>
        /// CNPJ da loja
        /// </summary>
        public string CdCnpj 
        { 
            get => Cnpj?.Value; 
            protected set => Cnpj = Cnpj.Create(value); 
        }

        // Value Object interno
        private Cnpj Cnpj { get; set; }

        /// <summary>
        /// Código da loja
        /// </summary>
        public string CdLoja { get; protected set; }

        /// <summary>
        /// Identificador do lojista
        /// </summary>
        public int IdLojista { get; protected set; }

        /// <summary>
        /// Identificador do varejista
        /// </summary>
        public int IdVarejista { get; protected set; }

        /// <summary>
        /// Nome da loja
        /// </summary>
        public string NmLoja { get; protected set; }

        /// <summary>
        /// Endereço da loja
        /// </summary>
        public string TxEndereco { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected Lojas() { }

        /// <summary>
        /// Cria uma nova instância de Loja
        /// </summary>
        public Lojas(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco)
        {
            // Usando Value Object
            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object

            CdLoja = cdLoja;
            IdLojista = idLojista;
            IdVarejista = idVarejista;
            NmLoja = nmLoja;
            TxEndereco = txEndereco;

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
            var validation = new Validations.Loja(this);
            validation.Validate();
            return validation.GetValidationResult();
        }

        /// <summary>
        /// Atualiza os dados da loja
        /// </summary>
        public void AtualizarDados(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco,
            string updatedBy)
        {
            CdCnpj = cdCnpj; // Usa o setter que valida através do Value Object
            CdLoja = cdLoja;
            IdLojista = idLojista;
            IdVarejista = idVarejista;
            NmLoja = nmLoja;
            TxEndereco = txEndereco;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));


            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Cria uma nova instância de Loja
        /// </summary>
        public static Lojas Create(
            string cdCnpj,
            string cdLoja,
            int idLojista,
            int idVarejista,
            string nmLoja,
            string txEndereco,
            string createdBy)
        {
            var loja = new Lojas(cdCnpj, cdLoja, idLojista, idVarejista, nmLoja, txEndereco);
            loja.SetCreatedBy(createdBy);

            // Adicionar evento
            loja.AddDomainEvent(new Events.Loja(
                loja.IdLoja,
                loja.NmLoja,
                loja.CdCnpj,
                loja.IdVarejista,
                createdBy));

            return loja;
        }
        #endregion


        #region Navigation Properties
        /// <summary>
        /// Navegação virtual para o Lojista (Entity Framework)
        /// </summary>
        //public virtual Lojista Lojista { get; protected set; }

        /// <summary>
        /// Navegação virtual para o Varejista (Entity Framework)
        /// </summary>
        public virtual Varejista Varejista { get; protected set; }
        #endregion
    }
}
