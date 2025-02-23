using eCommerce.Domain.Common;
using eCommerce.Domain.Exceptions;
using eCommerce.Domain.Events;

namespace eCommerce.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um corretor (broker) no sistema
    /// </summary>
    public class Broker : BaseEntity
    {
        #region Constants
        private const int NOME_MAX_LENGTH = 100;
        private const string NOME_REQUIRED_MESSAGE = "Nome do broker é obrigatório";
        private const string NOME_LENGTH_MESSAGE = "Nome do broker deve ter no máximo 100 caracteres";
        private const string ID_BROKER_REQUIRED_MESSAGE = "Id do broker deve ser maior que zero";
        #endregion

        #region Properties
        /// <summary>
        /// Identificador único do broker
        /// </summary>
        public int IdBroker { get; protected set; }

        /// <summary>
        /// Nome do broker
        /// </summary>
        public string NmBroker { get; protected set; }
        #endregion

        #region Constructors
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
            var errors = new List<string>();
            ValidateNome(nmBroker, errors);

            if (errors.Any())
                throw new DomainException(string.Join(", ", errors));

            NmBroker = nmBroker;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var errors = new List<string>();

            ValidateNome(NmBroker, errors);
            ValidateId(errors);

            return new ValidationResult(errors.Count == 0, errors);
        }

        private void ValidateNome(string nome, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(nome))
                errors.Add(NOME_REQUIRED_MESSAGE);

            if (nome?.Length > 100)
                errors.Add(NOME_LENGTH_MESSAGE);
        }

        private void ValidateId(List<string> errors)
        {
            if (IdBroker <= 0)
                errors.Add(ID_BROKER_REQUIRED_MESSAGE);
        }

        /// <summary>
        /// Atualiza o nome do broker
        /// </summary>
        /// <param name="novoNome">Novo nome a ser definido</param>
        /// <exception cref="DomainException">Lançada quando o novo nome é inválido</exception>
        public void AtualizarNome(string novoNome)
        {
            var errors = new List<string>();
            ValidateNome(novoNome, errors);

            if (errors.Any())
                throw new DomainException(string.Join(", ", errors));

            NmBroker = novoNome;
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
            broker.AddDomainEvent(new Broker(
                broker.IdBroker,
                broker.NmBroker,
                createdBy));

            return broker;
        }
        #endregion
    }
}
