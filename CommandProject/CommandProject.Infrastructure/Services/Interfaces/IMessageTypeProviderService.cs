namespace CommandProject.Infrastructure.Services.Interfaces
{
    public interface IMessageTypeProviderService
    {
        Type GetMessageType(Type eventType);

        string GetQueueName(Type eventType);
    }
}
