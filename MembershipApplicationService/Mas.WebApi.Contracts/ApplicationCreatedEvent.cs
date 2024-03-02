namespace Mas.WebApi.Contracts
{
    public record ApplicationCreatedEvent(Guid ApplicationId, string Name, DateTime ReceivedAt);
}
