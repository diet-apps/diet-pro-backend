using Diet.Pro.AI.Aplication.Comands;
using Diet.Pro.AI.Infra.Shared.InputModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diet.Pro.AI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDataInputModel inputModel)
        {
            var result = await _mediator.Send(new CreateUserCommand(inputModel));

            return Created(string.Empty, result.Value);
        }

        [HttpPost("{userId}/physical-data")]
        public async Task<IActionResult> Create(string userId, [FromBody] UserPhysicalDataInputModel inputModel)
        {
            var result = await _mediator.Send(new CreateUserPhysicalDataCommand(userId, inputModel));

            if (!result.IsSuccess)
                return BadRequest(result.Exception);

            return Ok(result.Value);
        }
    }
}
