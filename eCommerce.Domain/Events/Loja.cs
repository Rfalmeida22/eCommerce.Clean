namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada a uma Loja.
    /// </summary>
    public class Loja : Event
    {
        /// <summary>
        /// Identificador único da Loja.
        /// </summary>
        public int LojaId { get; }

        /// <summary>
        /// Nome da Loja.
        /// </summary>
        public string Nome { get; }

        /// <summary>
        /// CNPJ da Loja.
        /// </summary>
        public string Cnpj { get; }

        /// <summary>
        /// Identificador único do Varejista associado à Loja.
        /// </summary>
        public int VarejistaId { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento Loja.
        /// </summary>
        /// <param name="lojaId">Identificador único da Loja.</param>
        /// <param name="nome">Nome da Loja.</param>
        /// <param name="cnpj">CNPJ da Loja.</param>
        /// <param name="varejistaId">Identificador único do Varejista associado à Loja.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public Loja(int lojaId, string nome, string cnpj, int varejistaId, string userName)
            : base(userName)
        {
            LojaId = lojaId;
            Nome = nome;
            Cnpj = cnpj;
            VarejistaId = varejistaId;
        }
    }
} 