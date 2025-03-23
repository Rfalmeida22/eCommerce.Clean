using eCommerce.Domain.Events;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace eCommerce.Domain.Services
{
    /// <summary>
    /// Serviço de domínio para operações relacionadas a LogImportacaoVarejoDetalhe.
    /// </summary>
    public class LogImportacaoVarejoDetalhe : IDomainService
    {
        private readonly ILogImportacaoVarejoDetalhe _logImportacaoVarejoDetalheRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<LogImportacaoVarejoDetalhe> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe LogImportacaoVarejoDetalheService.
        /// </summary>
        /// <param name="logImportacaoVarejoDetalheRepository">Repositório de LogImportacaoVarejoDetalhe.</param>
        /// <param name="eventDispatcher">Despachante de eventos.</param>
        /// <exception cref="ArgumentNullException">Lançada quando qualquer dependência é nula.</exception>
        public LogImportacaoVarejoDetalhe(ILogImportacaoVarejoDetalhe logImportacaoVarejoDetalheRepository, IEventDispatcher eventDispatcher, ILogger<LogImportacaoVarejoDetalhe> logger)
        {
            _logImportacaoVarejoDetalheRepository = logImportacaoVarejoDetalheRepository ?? throw new ArgumentNullException(nameof(logImportacaoVarejoDetalheRepository));
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Valida se o CPF do comprador é válido.
        /// </summary>
        /// <param name="cpf">CPF do comprador.</param>
        /// <returns>True se o CPF é válido, caso contrário False.</returns>
        public bool ValidarCpf(string cpf)
        {
            return Cpf.IsValid(cpf);
        }

        /// <summary>
        /// Publica um evento de criação ou atualização de detalhe de importação.
        /// </summary>
        /// <param name="idDetalhe">ID do detalhe da importação.</param>
        /// <param name="broker">Nome do Broker.</param>
        /// <param name="cdCartao">Código do cartão.</param>
        /// <param name="cpfComprador">CPF do comprador.</param>
        /// <param name="userName">Nome do usuário que gerou o evento.</param>
        public async Task PublicarEventoLogImportacaoVarejoDetalheAsync(int idDetalhe, string broker, string cdCartao, string cpfComprador, string userName)
        {
            var evento = new Events.LogImportacaoVarejoDetalhe(idDetalhe, broker, cdCartao, cpfComprador, userName);
            await _eventDispatcher.PublishAsync(evento);
        }
    }
}

