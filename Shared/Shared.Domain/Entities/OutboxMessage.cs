using Shared.Domain.Base;

namespace Shared.Domain.Entities
{
    public class OutboxMessage : BaseRootEntity
    {
        public string Type { get; }
        public string Data { get; }
        public string QueueName { get; }

        // ORM
        private OutboxMessage()
        {

        }

        private OutboxMessage(string type, string data, string queueName)
        {
            Type = type;
            Data = data;
            QueueName = queueName;
        }

        public static OutboxMessage CreateOutboxMessage(string type, string data, string queueName)
        {
            return new OutboxMessage(type, data, queueName);
        }
    }
}
