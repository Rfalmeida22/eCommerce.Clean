namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de dom�nio que representa uma a��o relacionada a uma Loja.
    /// </summary>
    public class Loja : Event
    {
        /// <summary>
        /// Identificador �nico da Loja.
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
        /// Identificador �nico do Varejista associado � Loja.
        /// </summary>
        public int VarejistaId { get; }

        /// <summary>
        /// Inicializa uma nova inst�ncia do evento Loja.
        /// </summary>
        /// <param name="lojaId">Identificador �nico da Loja.</param>
        /// <param name="nome">Nome da Loja.</param>
        /// <param name="cnpj">CNPJ da Loja.</param>
        /// <param name="varejistaId">Identificador �nico do Varejista associado � Loja.</param>
        /// <param name="userName">Nome do usu�rio que gerou o evento.</param>
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