
using Azure.Messaging;
using Azure.Messaging.ServiceBus;
using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;
using Mas.Infrastructure.Data;
using Mas.WebApi.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Mas.WebApi.HostedService
{
    public class UpdateMemberListener : IHostedService
    {
        private readonly ServiceBusProcessor _busProcessor;
        private readonly ILogger<UpdateMemberListener> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UpdateMemberListener(
            ILogger<UpdateMemberListener> logger,
            ServiceBusClient busClient, 
            IServiceProvider serviceProvider)
        {
            _busProcessor = busClient.CreateProcessor("applicationcreatedtopic", "MasSubscription", new ServiceBusProcessorOptions { });
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _busProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _busProcessor.ProcessErrorAsync += ProcessErrorAsync;
            await _busProcessor.StartProcessingAsync(cancellationToken).ConfigureAwait(false);
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Error processing message {message}", args.Exception.Message);
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = message.Body.ToString();

            var cloudEvent = JsonSerializer.Deserialize<CloudEvent>(body);
            var messageEvent = cloudEvent.Data!.ToObjectFromJson<ApplicationCreatedEvent>();

            if (messageEvent.ApplicationId.ToString().StartsWith("aaa"))
                throw new Exception("Esto es un error gordisimo");
            else if (messageEvent.ApplicationId.ToString().StartsWith("b"))
                throw new ArgumentException("Esto es un argumento invalido");

            var application = new Application(messageEvent.ApplicationId, DateTime.Today,
               new Person(messageEvent.Name, "Rodes", "jan.rodes@gmail.com", "89899"), MembershipType.OnlyGym, "whatever_emaillocation");

            using var scope = _serviceProvider.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var existingApplication = await dbContext.Applications.SingleOrDefaultAsync(x=>x.Id == messageEvent.ApplicationId);

            if (existingApplication == null)
            {
                await args.DeadLetterMessageAsync(args.Message,
                                             "NonExistingApplication",
                                             "There is no a application to be updated");
                return;
            }
            var newPerson = new Person(messageEvent.Name, existingApplication.Person.Name.LastName, existingApplication.Person.Email, existingApplication.Person.Phone);
            existingApplication.UpdateDetails(newPerson, existingApplication.MembershipType);
            await dbContext.SaveChangesAsync();

           

            _logger.LogInformation($"Message received for application {messageEvent.ApplicationId}");

            await args.CompleteMessageAsync(message);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busProcessor.StopProcessingAsync(cancellationToken).ConfigureAwait(false);

        }
    }
}
