using CommandProject.Application.Common;
using MediatR;

namespace CommandProject.Application.Todo.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<BaseResult<CreateTodoItemResult>>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
    }
}
