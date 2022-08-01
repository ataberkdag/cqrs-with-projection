using CommandProject.Application.Todo.Commands.CreateTodoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommandProject.API.Controllers
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

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
