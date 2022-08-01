using CommandProject.Application.Common;
using CommandProject.Domain.Entities;
using CommandProject.Domain.Interfaces;
using MediatR;

namespace CommandProject.Application.Todo.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, BaseResult<CreateTodoItemResult>>
    {
        private readonly ICommandProjectUnitOfWork _uow;
        public CreateTodoItemCommandHandler(ICommandProjectUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<CreateTodoItemResult>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = TodoItem.CreateTodoItem(request.Title, request.Content, request.UserId);

            await _uow.TodoItems.AddAsync(todoItem);

            await _uow.SaveChangesAsync();

            return BaseResult<CreateTodoItemResult>.Succeeded(new CreateTodoItemResult(todoItem.Id));
        }
    }
}
