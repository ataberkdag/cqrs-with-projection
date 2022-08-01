using Shared.Domain.Base;

namespace CommandProject.Domain.Events
{
    public class TodoItemCreated : DomainEvent
    {
        public Guid TodoItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }

        public TodoItemCreated(Guid todoItemId, string title, string content, Guid userId, DateTime createdOn)
        {
            TodoItemId = todoItemId;
            Title = title;
            Content = content;
            UserId = userId;
            CreatedOn = createdOn;
        }
    }
}
