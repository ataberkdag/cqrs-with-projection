using CommandProject.Domain.Events;
using Shared.Domain.Base;

namespace CommandProject.Domain.Entities
{
    public class TodoItem : BaseRootEntity
    {
        public string Title { get; }
        public string Content { get; }
        public Guid UserId { get; }
        public TodoItemStatus Status { get; }

        // ORM
        private TodoItem()
        {

        }

        private TodoItem(string title, string content, Guid userId)
        {
            Title = title;
            Content = content;
            UserId = userId;
            Status = TodoItemStatus.Open;

            this.AddDomainEvent(new TodoItemCreated(Id, Title, Content, UserId, CreatedOn));
        }

        public static TodoItem CreateTodoItem(string title, string content, Guid userId)
        {
            return new TodoItem(title, content, userId);
        }
    }

    public enum TodoItemStatus
    {
        Open = 0,
        Working = 1,
        Completed = 2,
        Cancelled = 3
    }
}
