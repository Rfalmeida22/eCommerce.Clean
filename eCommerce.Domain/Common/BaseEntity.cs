
using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Validations;
namespace eCommerce.Domain.Common
{
    /// <summary>
    /// Classe base abstrata que fornece funcionalidades comuns para todas as entidades do domínio.
    /// Implementa rastreamento de auditoria, gestão de eventos de domínio e comparação de entidades.
    /// </summary>
    public abstract class BaseEntity : Validation, IEntity
    {
        #region Constantes
        private const string ID_MUST_BE_GREATER_THAN_ZERO = "Id deve ser maior que zero";
        private const string UPDATED_DATE_CANNOT_BE_BEFORE_CREATED_DATE = "Data de atualização não pode ser anterior à data de criação";
        #endregion

        #region Propriedades
        /// <summary>
        /// Identificador único da entidade. Valores devem ser maiores que zero.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Data de criação do registro em UTC.
        /// É definida automaticamente no momento da criação da entidade.
        /// </summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Data da última atualização do registro em UTC.
        /// Null indica que o registro nunca foi atualizado.
        /// </summary>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Identificador do usuário que criou o registro.
        /// Não pode ser nulo ou vazio.
        /// </summary>
        public string CreatedBy { get; protected set; }

        /// <summary>
        /// Identificador do usuário que realizou a última atualização.
        /// Pode ser nulo se o registro nunca foi atualizado.
        /// </summary>
        public string UpdatedBy { get; protected set; }

        /// <summary>
        /// Indica se o registro está ativo no sistema.
        /// True por padrão na criação.
        /// </summary>
        public bool IsActive { get; protected set; } = true;
        #endregion

        #region Eventos
        /// <summary>
        /// Lista de eventos de domínio pendentes para publicação.
        /// Eventos são limpos após a publicação bem-sucedida.
        /// </summary>
        private readonly List<Event> _domainEvents = new();

        /// <summary>
        /// Serviço responsável pela publicação dos eventos de domínio.
        /// </summary>
        private readonly IEventDispatcher _eventDispatcher;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor para criar uma nova entidade com um despachante de eventos
        /// </summary>
        /// <param name="eventDispatcher">Despachante de eventos</param>
        /// <exception cref="ArgumentNullException">Lançada quando o despachante de eventos é nulo</exception>
        protected BaseEntity(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Construtor para criar uma nova entidade com ID específico e um despachante de eventos
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <param name="eventDispatcher">Despachante de eventos</param>
        /// <exception cref="ArgumentException">Lançada quando o ID é menor ou igual a zero</exception>
        protected BaseEntity(int id, IEventDispatcher eventDispatcher) : this(eventDispatcher)
        {
            if (id <= 0)
                throw new ArgumentException(ID_MUST_BE_GREATER_THAN_ZERO);

            Id = id;
        }

        /// <summary>
        /// Construtor sem parâmetros para o EF Core
        /// </summary>
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Define o usuário que criou o registro e registra o momento da criação.
        /// </summary>
        /// <param name="userName">Nome do usuário que está criando o registro.</param>
        /// <exception cref="ArgumentException">Lançada quando userName é nulo ou vazio.</exception>
        public void SetCreatedBy(string userName)
        {
            ValidateRequired(userName, "UserName");
            CreatedBy = userName.Trim();
        }

        /// <summary>
        /// Define o usuário que atualizou o registro e atualiza a data de modificação
        /// </summary>
        /// <param name="userName">Nome do usuário</param>
        /// <exception cref="ArgumentException">Lançada quando o nome do usuário é vazio ou nulo</exception>
        public void SetUpdatedBy(string userName)
        {
            ValidateRequired(userName, "UserName");
            UpdatedBy = userName.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Desativa o registro e registra o usuário que realizou a operação
        /// </summary>
        /// <param name="updatedBy">Nome do usuário que realizou a desativação</param>
        public void Deactivate(string updatedBy)
        {
            IsActive = false;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Ativa o registro e registra o usuário que realizou a operação
        /// </summary>
        /// <param name="updatedBy">Nome do usuário que realizou a ativação</param>
        public void Activate(string updatedBy)
        {
            IsActive = true;
            SetUpdatedBy(updatedBy);
        }

        /// <summary>
        /// Compara se duas entidades são iguais baseado em seus IDs
        /// </summary>
        /// <param name="obj">Objeto a ser comparado</param>
        /// <returns>True se as entidades têm o mesmo ID, False caso contrário</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is not BaseEntity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        /// <summary>
        /// Gera um código hash baseado no ID da entidade
        /// </summary>
        /// <returns>Hash code do ID da entidade</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Obtém os eventos de domínio associados à entidade
        /// </summary>
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adiciona um novo evento de domínio à lista de eventos pendentes.
        /// </summary>
        /// <param name="event">Evento a ser adicionado.</param>
        /// <exception cref="ArgumentNullException">Lançada quando o evento é nulo.</exception>
        public void AddDomainEvent(Event @event)
        {
            _domainEvents.Add(@event);
        }

        /// <summary>
        /// Remove um evento específico da lista de eventos pendentes.
        /// </summary>
        /// <param name="event">Evento a ser removido.</param>
        public void RemoveDomainEvent(Event @event)
        {
            _domainEvents.Remove(@event);
        }

        /// <summary>
        /// Publica todos os eventos pendentes através do despachante de eventos.
        /// Após a publicação bem-sucedida, a lista de eventos é limpa.
        /// </summary>
        /// <exception cref="InvalidOperationException">Lançada quando o despachante de eventos não está inicializado.</exception>
        public async Task PublishEventsAsync()
        {
            if (_eventDispatcher != null)
            {
                foreach (var @event in _domainEvents)
                {
                    await _eventDispatcher.PublishAsync(@event);
                }
            }
            _domainEvents.Clear();
        }

        /// <summary>
        /// Realiza a auditoria de criação do registro.
        /// </summary>
        /// <param name="userName">Nome do usuário responsável pela criação.</param>
        /// <exception cref="ArgumentNullException">Lançada quando userName é nulo.</exception>
        protected void AuditCreate(string userName)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = userName ?? throw new ArgumentNullException(nameof(userName));
            IsActive = true;
        }

        /// <summary>
        /// Realiza a auditoria de atualização do registro.
        /// </summary>
        /// <param name="userName">Nome do usuário responsável pela atualização.</param>
        /// <exception cref="ArgumentNullException">Lançada quando userName é nulo.</exception>
        protected void AuditUpdate(string userName)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        /// <summary>
        /// Valida a consistência das datas de criação e atualização.
        /// </summary>
        /// <exception cref="InvalidOperationException">Lançada quando a data de atualização é anterior à data de criação.</exception>
        protected void ValidateDates()
        {
            if (UpdatedAt.HasValue && UpdatedAt.Value < CreatedAt)
                AddError(UPDATED_DATE_CANNOT_BE_BEFORE_CREATED_DATE);
        }

        /// <summary>
        /// Operador de igualdade para comparação entre duas entidades.
        /// Realiza uma comparação segura contra null e utiliza o método Equals para comparação.
        /// </summary>
        /// <param name="left">Entidade do lado esquerdo da comparação</param>
        /// <param name="right">Entidade do lado direito da comparação</param>
        /// <returns>
        /// True se:
        /// - Ambas as entidades são null
        /// - Ambas as entidades são não-null e são iguais segundo o método Equals
        /// False caso contrário
        /// </returns>
        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        /// <summary>
        /// Operador de desigualdade para comparação entre duas entidades.
        /// Complemento lógico do operador de igualdade.
        /// </summary>
        /// <param name="left">Entidade do lado esquerdo da comparação</param>
        /// <param name="right">Entidade do lado direito da comparação</param>
        /// <returns>
        /// True se as entidades são diferentes (não são iguais segundo o operador ==)
        /// False se as entidades são iguais
        /// </returns>
        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }
        #endregion
    }
}
