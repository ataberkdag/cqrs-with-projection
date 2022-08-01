namespace Shared.Messages.Messages
{
    public class TodoItemMessage
    {
        public Guid TodoItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
    }
}
