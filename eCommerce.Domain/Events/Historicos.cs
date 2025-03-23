using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Evento de domínio que representa uma ação relacionada a um Histórico.
    /// </summary>
    public class Historicos : Event
    {
        /// <summary>
        /// Identificador único do Histórico.
        /// </summary>
        public int HistoricosCod { get; }

        /// <summary>
        /// Ação realizada.
        /// </summary>
        public string Acao { get; }

        /// <summary>
        /// Data da ação.
        /// </summary>
        public DateTime Data { get; }

        /// <summary>
        /// Inicializa uma nova instância do evento Historicos
        /// </summary>
        /// <param name="historicosCod">Identificador único do Histórico.</param>
        /// <param name="acao">Ação realizada.</param>
        /// <param name="data">Data da ação.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public Historicos(int historicosCod, string acao, DateTime data, string userName)
            : base(userName)
        {
            HistoricosCod = historicosCod;
            Acao = acao;
            Data = data;
        }
    }
}
