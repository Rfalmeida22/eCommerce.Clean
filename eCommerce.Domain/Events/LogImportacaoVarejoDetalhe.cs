namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada a um LogImportacaoVarejoDetalhe.
    /// </summary>
    public class LogImportacaoVarejoDetalhe : Event
    {
        /// <summary>
        /// Identificador único do detalhe da importação.
        /// </summary>
        public int IdDetalhe { get; }

        /// <summary>
        /// Nome do Broker.
        /// </summary>
        public string Broker { get; }

        /// <summary>
        /// Código do cartão.
        /// </summary>
        public string CdCartao { get; }

        /// <summary>
        /// CPF do comprador.
        /// </summary>
        public string CpfComprador { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento LogImportacaoVarejoDetalheEvent.
        /// </summary>
        /// <param name="idDetalhe">Identificador único do detalhe da importação.</param>
        /// <param name="broker">Nome do Broker.</param>
        /// <param name="cdCartao">Código do cartão.</param>
        /// <param name="cpfComprador">CPF do comprador.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public LogImportacaoVarejoDetalhe(int idDetalhe, string broker, string cdCartao, string cpfComprador, string userName)
            : base(userName)
        {
            IdDetalhe = idDetalhe;
            Broker = broker;
            CdCartao = cdCartao;
            CpfComprador = cpfComprador;
        }
    }
}


