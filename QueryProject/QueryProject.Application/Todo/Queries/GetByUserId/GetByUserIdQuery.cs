using MediatR;
using QueryProject.Application.Common;

namespace QueryProject.Application.Todo.Queries.GetByUserId
{
    public class GetByUserIdQuery : IRequest<BaseResult<GetByUserIdQueryResult>>
    {
        public Guid UserId { get; set; }
    }
}
