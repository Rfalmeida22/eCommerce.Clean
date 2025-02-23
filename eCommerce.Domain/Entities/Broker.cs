using eCommerce.Domain.Common;
using eCommerce.Domain.Exceptions;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um corretor (broker) no sistema
    /// </summary>
    public class Broker : BaseEntity
    {
        #region Propriedades
        /// <summary>
        /// Identificador único do broker
        /// </summary>
        public int IdBroker { get; protected set; }

        /// <summary>
        /// Nome do broker
        /// </summary>
        public string NmBroker { get; protected set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor protegido para uso do Entity Framework
        /// </summary>
        protected Broker() { }

        /// <summary>
        /// Cria uma nova instância de Broker
        /// </summary>
        /// <param name="nmBroker">Nome do broker</param>
        /// <exception cref="DomainException">Lançada quando o nome é inválido</exception>
        public Broker(string nmBroker)
        {
            NmBroker = nmBroker;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var validation = new Validations.Broker(this);
            validation.Validate();
            return validation.GetValidationResult();
        }

        /// <summary>
        /// Atualiza o nome do broker
        /// </summary>
        /// <param name="novoNome">Novo nome a ser definido</param>
        /// <exception cref="DomainException">Lançada quando o novo nome é inválido</exception>
        public void AtualizarNome(string novoNome)
        {
            NmBroker = novoNome;

            var validationResult = Validar();

            if (!validationResult.IsValid)
                throw new DomainException(string.Join(", ", validationResult.Errors));

            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Atualiza os dados do broker
        /// </summary>
        /// <param name="novoNome">Novo nome</param>
        /// <param name="updatedBy">Usuário que realizou a atualização</param>
        public void AtualizarDados(string novoNome, string updatedBy)
        {
            AtualizarNome(novoNome);
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Cria uma nova instância de Broker
        /// </summary>
        /// <param name="nmBroker">Nome do broker</param>
        /// <param name="createdBy">Usuário que criou o registro</param>
        /// <returns>Nova instância de Broker</returns>
        public static Broker Create(string nmBroker, string createdBy)
        {
            var broker = new Broker(nmBroker);
            broker.SetCreatedBy(createdBy);

            // Adicionar evento
            broker.AddDomainEvent(new Events.Broker(
                broker.IdBroker,
                broker.NmBroker,
                createdBy));

            return broker;
        }
        #endregion
    }
}
