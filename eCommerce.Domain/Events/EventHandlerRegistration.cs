using Microsoft.Extensions.DependencyInjection;
using eCommerce.Domain.EventHandlers;

namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Classe estática responsável por registrar os manipuladores de eventos e o despachante de eventos no contêiner de injeção de dependência.
    /// </summary>
    public static class EventHandlerRegistration
    {
        /// <summary>
        /// Adiciona os manipuladores de eventos e o despachante de eventos ao contêiner de serviços.
        /// </summary>
        /// <param name="services">A coleção de serviços para adicionar os manipuladores de eventos.</param>
        /// <returns>A coleção de serviços com os manipuladores de eventos adicionados.</returns>
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            // Registra os manipuladores de eventos
            services.AddScoped<IEventHandler<Broker>, EventHandlers.Broker>();
            services.AddScoped<IEventHandler<BrokerVarejista>, EventHandlers.BrokerVarejista>();
            services.AddScoped<IEventHandler<Loja>, EventHandlers.Loja>();
            services.AddScoped<IEventHandler<Usuario>, EventHandlers.Usuario>();
            services.AddScoped<IEventHandler<Varejista>, EventHandlers.Varejista>();

            // Registra o despachante de eventos
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            return services;
        }
    }
}

/*
 * Sobre EventHandlerRegistration:
 * 
 * A classe EventHandlerRegistration é uma classe estática que facilita o registro de manipuladores de eventos e do despachante de eventos
 * no contêiner de injeção de dependência. Isso garante que os eventos de domínio possam ser resolvidos e manipulados corretamente em todo o sistema.
 * 
 * O método AddEventHandlers estende IServiceCollection e adiciona os manipuladores de eventos e o despachante de eventos ao contêiner de serviços
 * com o tempo de vida Scoped. Isso significa que uma nova instância dos manipuladores de eventos será criada para cada solicitação de escopo.
 * 
 * Exemplo de Uso:
 * 
 * public void ConfigureServices(IServiceCollection services)
 * {
 *     services.AddEventHandlers();
 *     // Outros registros de serviços
 * }
 */