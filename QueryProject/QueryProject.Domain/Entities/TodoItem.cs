namespace QueryProject.Domain.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Title { get; }
        public string Content { get; }
        public Guid UserId { get; }
        public TodoItemStatus Status { get; }

        public TodoItem(string title, string content, Guid userId)
        {
            Title = title;
            Content = content;
            UserId = userId;
            Status = TodoItemStatus.Open;
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
