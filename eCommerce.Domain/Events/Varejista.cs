namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada a um Varejista.
    /// </summary>
    public class Varejista : Event
    {
        /// <summary>
        /// Identificador único do Varejista.
        /// </summary>
        public int VarejistaId { get; }

        /// <summary>
        /// Nome do Varejista.
        /// </summary>
        public string Nome { get; }

        /// <summary>
        /// CNPJ do Varejista.
        /// </summary>
        public string Cnpj { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento Varejista.
        /// </summary>
        /// <param name="varejistaId">Identificador único do Varejista.</param>
        /// <param name="nome">Nome do Varejista.</param>
        /// <param name="cnpj">CNPJ do Varejista.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public Varejista(int varejistaId, string nome, string cnpj, string userName)
            : base(userName)
        {
            VarejistaId = varejistaId;
            Nome = nome;
            Cnpj = cnpj;
        }
    }
} 