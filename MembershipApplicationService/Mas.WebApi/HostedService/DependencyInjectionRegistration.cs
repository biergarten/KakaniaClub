using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace Mas.WebApi.HostedService
{
    public static class DependencyInjectionRegistration
    { 
        public static IServiceCollection AddNewApplicationListener(this IServiceCollection services)
        {
            var credential = new DefaultAzureCredential();
            services.AddSingleton(_ => new ServiceBusClient("kakania-club-shared.servicebus.windows.net", credential));

            //services.AddHostedService<NewMemberListener>();
            services.AddHostedService<UpdateMemberListener>();
            return services;
        }
    }
}
