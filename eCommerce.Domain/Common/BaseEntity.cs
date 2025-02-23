using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Domain.Events;

namespace eCommerce.Domain.Common
{
    /// <summary>
    /// Classe base para todas as entidades do domínio
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador único da entidade
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Data da última atualização do registro
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Usuário que criou o registro
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// Usuário que realizou a última atualização
        /// </summary>
        public string UpdatedBy { get; private set; }

        /// <summary>
        /// Indica se o registro está ativo
        /// </summary>
        public bool IsActive { get; protected set; } = true;

        private readonly List<Event> _domainEvents = new();
        private readonly IEventDispatcher _eventDispatcher;

        protected BaseEntity(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Construtor para criar uma nova entidade com ID específico
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <exception cref="ArgumentException">Lançada quando o ID é menor ou igual a zero</exception>
        protected BaseEntity(int id, IEventDispatcher eventDispatcher) : this(eventDispatcher)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero");

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

        /// <summary>
        /// Define o usuário que criou o registro
        /// </summary>
        /// <param name="userName">Nome do usuário</param>
        /// <exception cref="ArgumentException">Lançada quando o nome do usuário é vazio ou nulo</exception>
        public void SetCreatedBy(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("UserName não pode ser vazio");

            CreatedBy = userName;
        }

        /// <summary>
        /// Define o usuário que atualizou o registro e atualiza a data de modificação
        /// </summary>
        /// <param name="userName">Nome do usuário</param>
        /// <exception cref="ArgumentException">Lançada quando o nome do usuário é vazio ou nulo</exception>
        public void SetUpdatedBy(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("UserName não pode ser vazio");

            UpdatedBy = userName;
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

        public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(Event @event)
        {
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(Event @event)
        {
            _domainEvents.Remove(@event);
        }

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
    }
}
