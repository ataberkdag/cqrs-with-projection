using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryProject.Application.Todo.Queries.GetByUserId;

namespace QueryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId([FromQuery] GetByUserIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
