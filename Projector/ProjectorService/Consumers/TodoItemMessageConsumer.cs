using MassTransit;
using ProjectorService.Entities;
using ProjectorService.Services.Interfaces;
using Shared.Messages.Messages;

namespace ProjectorService.Consumers
{
    public class TodoItemMessageConsumer : IConsumer<TodoItemMessage>
    {
        private readonly ICacheService _cacheService;

        public TodoItemMessageConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<TodoItemMessage> context)
        {
            var currentItems = await _cacheService.Get<List<TodoItem>>($"Todo_{context.Message.UserId.ToString()}");

            if (currentItems == null)
            {
                currentItems = new List<TodoItem> { new TodoItem(context.Message.Title, context.Message.Content, context.Message.UserId) };
            }
            else
            {
                currentItems.Add(new TodoItem(context.Message.Title, context.Message.Content, context.Message.UserId));
            }

            await _cacheService.Set<List<TodoItem>>($"Todo_{context.Message.UserId.ToString()}", currentItems, null);
        }
    }
}
