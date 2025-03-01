namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de dom�nio que representa uma a��o relacionada a um Varejista.
    /// </summary>
    public class Varejista : Event
    {
        /// <summary>
        /// Identificador �nico do Varejista.
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
        /// Inicializa uma nova inst�ncia do evento Varejista.
        /// </summary>
        /// <param name="varejistaId">Identificador �nico do Varejista.</param>
        /// <param name="nome">Nome do Varejista.</param>
        /// <param name="cnpj">CNPJ do Varejista.</param>
        /// <param name="userName">Nome do usu�rio que gerou o evento.</param>
        public Varejista(int varejistaId, string nome, string cnpj, string userName)
            : base(userName)
        {
            VarejistaId = varejistaId;
            Nome = nome;
            Cnpj = cnpj;
        }
    }
} 