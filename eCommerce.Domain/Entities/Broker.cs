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
        private const int NOME_MAX_LENGTH = 100;
        private const string NOME_REQUIRED_MESSAGE = "Nome do broker é obrigatório";
        private const string NOME_LENGTH_MESSAGE = "Nome do broker deve ter no máximo 100 caracteres";

        /// <summary>
        /// Identificador único do broker
        /// </summary>
        public int IdBroker { get; protected set; }

        /// <summary>
        /// Nome do broker
        /// </summary>
        public string NmBroker { get; protected set; }

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
            ValidarNome(nmBroker);
            NmBroker = nmBroker;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Valida o estado atual da entidade
        /// </summary>
        /// <returns>Resultado da validação</returns>
        public ValidationResult Validar()
        {
            var errors = new List<string>();

            ValidateNome(errors);
            ValidateId(errors);

            return new ValidationResult(errors.Count == 0, errors);
        }

        private void ValidateNome(List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(NmBroker))
                errors.Add(NOME_REQUIRED_MESSAGE);

            if (NmBroker?.Length > NOME_MAX_LENGTH)
                errors.Add(NOME_LENGTH_MESSAGE);
        }

        private void ValidateId(List<string> errors)
        {
            if (IdBroker <= 0)
                errors.Add("Id do broker deve ser maior que zero");
        }

        /// <summary>
        /// Atualiza o nome do broker
        /// </summary>
        /// <param name="novoNome">Novo nome a ser definido</param>
        /// <exception cref="DomainException">Lançada quando o novo nome é inválido</exception>
        public void AtualizarNome(string novoNome)
        {
            ValidarNome(novoNome);
            NmBroker = novoNome;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Valida o nome do broker
        /// </summary>
        /// <param name="nome">Nome a ser validado</param>
        /// <exception cref="DomainException">Lançada quando o nome é inválido</exception>
        private void ValidarNome(string nome)
        {
            if (nome == null)
                throw new DomainException("Nome do broker não pode ser nulo");

            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome do broker é obrigatório");

            if (nome.Length > 100)
                throw new DomainException("Nome do broker deve ter no máximo 100 caracteres");
        }

        /// <summary>
        /// Atualiza os dados do broker
        /// </summary>
        /// <param name="novoNome">Novo nome</param>
        /// <param name="updatedBy">Usuário que realizou a atualização</param>
        public void AtualizarDados(string novoNome, string updatedBy)
        {
            ValidarNome(novoNome);
            NmBroker = novoNome;
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
    }
}
