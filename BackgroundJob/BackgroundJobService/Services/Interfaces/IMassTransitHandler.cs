namespace BackgroundJobService.Services.Interfaces
{
    public interface IMassTransitHandler
    {
        Task Publish(object @event);

        Task Send(string queueName, object @event);
    }
}
