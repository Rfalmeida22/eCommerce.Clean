using Microsoft.Extensions.DependencyInjection;
using eCommerce.Domain.EventHandlers;

namespace eCommerce.Domain.Events
{
    public static class EventHandlerRegistration
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<Broker>, EventHandlers.Broker>();
            services.AddScoped<IEventHandler<BrokerVarejista>, EventHandlers.BrokerVarejista>();
            services.AddScoped<IEventHandler<Loja>, EventHandlers.Loja>();
            services.AddScoped<IEventHandler<Usuario>, EventHandlers.Usuario>();
            services.AddScoped<IEventHandler<Varejista>, EventHandlers.Varejista>();
            
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            return services;
        }
    }
} 