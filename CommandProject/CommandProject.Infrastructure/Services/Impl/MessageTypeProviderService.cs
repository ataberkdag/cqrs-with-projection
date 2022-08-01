using CommandProject.Domain.Events;
using CommandProject.Infrastructure.Services.Interfaces;
using Shared.Messages;
using Shared.Messages.Messages;

namespace CommandProject.Infrastructure.Services.Impl
{
    public class MessageTypeProviderService : IMessageTypeProviderService
    {
        private readonly Dictionary<Type, MessageTypeRegistry> _messageProvider;

        public MessageTypeProviderService()
        {
            _messageProvider = new Dictionary<Type, MessageTypeRegistry>();

            _messageProvider.Add(typeof(TodoItemCreated),new MessageTypeRegistry(typeof(TodoItemMessage), RabbitMqConsts.TodoItemCreatedQueueName));
        }

        public Type GetMessageType(Type eventType)
        {
            return _messageProvider[eventType].MessageType;
        }

        public string GetQueueName(Type eventType)
        {
            return _messageProvider[eventType].QueueName;
        }
    }

    public class MessageTypeRegistry
    {
        public Type MessageType { get; set; }
        public string QueueName { get; set; }

        public MessageTypeRegistry(Type messageType, string queueName)
        {
            MessageType = messageType;
            QueueName = queueName;
        }
    }
}
