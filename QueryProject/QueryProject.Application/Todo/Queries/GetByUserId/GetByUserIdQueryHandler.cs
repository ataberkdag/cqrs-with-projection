using MediatR;
using QueryProject.Application.Common;
using QueryProject.Application.Services.Interfaces;
using QueryProject.Domain.Entities;

namespace QueryProject.Application.Todo.Queries.GetByUserId
{
    public class GetByUserIdQueryHandler : IRequestHandler<GetByUserIdQuery, BaseResult<GetByUserIdQueryResult>>
    {
        private readonly ICacheService _cacheService;

        public GetByUserIdQueryHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<BaseResult<GetByUserIdQueryResult>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var todoItems = await _cacheService.Get<List<TodoItem>>($"Todo_{request.UserId.ToString()}");

            var todoViewItemList = todoItems?.Select((todoItem) => {
                return new TodoViewItem(todoItem.Title, todoItem.Content);
            }).ToList();

            return BaseResult<GetByUserIdQueryResult>.Succeeded(new GetByUserIdQueryResult(todoViewItemList));

        }
    }
}
