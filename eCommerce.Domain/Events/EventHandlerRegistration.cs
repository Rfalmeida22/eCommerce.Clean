using Microsoft.Extensions.DependencyInjection;
using eCommerce.Domain.EventHandlers;

namespace eCommerce.Domain.Events
{
    /// <summary>
    /// Classe est�tica respons�vel por registrar os manipuladores de eventos e o despachante de eventos no cont�iner de inje��o de depend�ncia.
    /// </summary>
    public static class EventHandlerRegistration
    {
        /// <summary>
        /// Adiciona os manipuladores de eventos e o despachante de eventos ao cont�iner de servi�os.
        /// </summary>
        /// <param name="services">A cole��o de servi�os para adicionar os manipuladores de eventos.</param>
        /// <returns>A cole��o de servi�os com os manipuladores de eventos adicionados.</returns>
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
 * A classe EventHandlerRegistration � uma classe est�tica que facilita o registro de manipuladores de eventos e do despachante de eventos
 * no cont�iner de inje��o de depend�ncia. Isso garante que os eventos de dom�nio possam ser resolvidos e manipulados corretamente em todo o sistema.
 * 
 * O m�todo AddEventHandlers estende IServiceCollection e adiciona os manipuladores de eventos e o despachante de eventos ao cont�iner de servi�os
 * com o tempo de vida Scoped. Isso significa que uma nova inst�ncia dos manipuladores de eventos ser� criada para cada solicita��o de escopo.
 * 
 * Exemplo de Uso:
 * 
 * public void ConfigureServices(IServiceCollection services)
 * {
 *     services.AddEventHandlers();
 *     // Outros registros de servi�os
 * }
 */