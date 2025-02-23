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
    /// Entidade que representa o relacionamento entre Brokers e Varejistas
    /// </summary>
    public class Brokers_Varejistas : BaseEntity
    {
        #region Constants
        private const string ID_BROKER_INVALID = "IdBroker deve ser maior que zero";
        private const string ID_VAREJISTA_INVALID = "IdVarejista deve ser maior que zero";
        #endregion

        #region Properties
        /// <summary>
        /// Identificador único do relacionamento
        /// </summary>
        public int IdSequencial { get; protected set; }

        /// <summary>
        /// Identificador do Broker
        /// </summary>
        public int IdBroker { get; protected set; }

        /// <summary>
        /// Identificador do Varejista
        /// </summary>
        public int IdVarejista { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected Brokers_Varejistas() { }

        /// <summary>
        /// Cria uma nova instância do relacionamento entre Broker e Varejista
        /// </summary>
        /// <param name="idBroker">ID do Broker</param>
        /// <param name="idVarejista">ID do Varejista</param>
        public Brokers_Varejistas(int idBroker, int idVarejista)
        {
            ValidarRelacionamentos(idBroker, idVarejista);
            IdBroker = idBroker;
            IdVarejista = idVarejista;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var erros = new List<string>();

            var resultadoRelacionamentos = ValidarRelacionamentos(IdBroker, IdVarejista);
            if (!resultadoRelacionamentos.IsValid)
                erros.AddRange(resultadoRelacionamentos.Errors);

            return new ValidationResult(erros.Count == 0, erros);
        }

        /// <summary>
        /// Valida os IDs do relacionamento
        /// </summary>
        /// <param name="idBroker">ID do Broker</param>
        /// <param name="idVarejista">ID do Varejista</param>
        /// <returns>Resultado da validação</returns>
        private ValidationResult ValidarRelacionamentos(int idBroker, int idVarejista)
        {
            var erros = new List<string>();

            if (idBroker <= 0)
                erros.Add(ID_BROKER_INVALID);

            if (idVarejista <= 0)
                erros.Add(ID_VAREJISTA_INVALID);

            return new ValidationResult(erros.Count == 0, erros);
        }

        /// <summary>
        /// Atualiza os relacionamentos entre Broker e Varejista
        /// </summary>
        /// <param name="brokerId">Novo ID do Broker</param>
        /// <param name="retailerId">Novo ID do Varejista</param>
        /// <param name="updatedBy">Usuário que realizou a atualização</param>
        public void AtualizarRelacionamentos(int brokerId, int retailerId, string updatedBy)
        {
            ValidarRelacionamentos(brokerId, retailerId);
            IdBroker = brokerId;
            IdVarejista = retailerId;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Cria uma nova instância do relacionamento
        /// </summary>
        /// <param name="idBroker">ID do Broker</param>
        /// <param name="idVarejista">ID do Varejista</param>
        /// <param name="createdBy">Usuário que criou o registro</param>
        /// <returns>Nova instância de Brokers_Varejistas</returns>
        public static Brokers_Varejistas Create(int idBroker, int idVarejista, string createdBy)
        {
            var relacionamento = new Brokers_Varejistas(idBroker, idVarejista);
            relacionamento.SetCreatedBy(createdBy);

            // Adicionar evento
            relacionamento.AddDomainEvent(new BrokerVarejista(
                idBroker,
                idVarejista,
                createdBy));

            return relacionamento;
        }

        /// <summary>
        /// Verifica se o relacionamento é único
        /// </summary>
        /// <param name="existingRelationships">Lista de relacionamentos existentes</param>
        /// <returns>True se o relacionamento é único, False caso contrário</returns>
        public bool IsUniqueRelationship(IEnumerable<Brokers_Varejistas> existingRelationships)
        {
            return !existingRelationships.Any(r =>
                r.IdBroker == IdBroker &&
                r.IdVarejista == IdVarejista &&
                r.Id != Id);
        }

        /// <summary>
        /// Verifica se o relacionamento é o mesmo
        /// </summary>
        /// <param name="brokerId">ID do Broker</param>
        /// <param name="varejistaId">ID do Varejista</param>
        /// <returns>True se o relacionamento é o mesmo, False caso contrário</returns>
        public bool HasSameRelationship(int brokerId, int varejistaId)
        {
            return IdBroker == brokerId && IdVarejista == varejistaId;
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Broker associado ao relacionamento
        /// </summary>
        public virtual Broker Broker { get; protected set; }

        /// <summary>
        /// Varejista associado ao relacionamento
        /// </summary>
        public virtual Varejista Varejista { get; protected set; }
        #endregion
    }
}
