namespace CommandProject.Application.Todo.Commands.CreateTodoItem
{
    public class CreateTodoItemResult
    {
        public Guid Id { get; set; }

        public CreateTodoItemResult(Guid id)
        {
            Id = id;
        }
    }
}
