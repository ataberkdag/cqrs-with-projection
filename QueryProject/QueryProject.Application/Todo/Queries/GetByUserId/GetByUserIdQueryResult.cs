namespace QueryProject.Application.Todo.Queries.GetByUserId
{
    public class GetByUserIdQueryResult
    {
        public List<TodoViewItem> TodoItems { get; set; }

        public GetByUserIdQueryResult(List<TodoViewItem> todoItems)
        {
            TodoItems = todoItems;
        }
    }

    public class TodoViewItem
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public TodoViewItem(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
