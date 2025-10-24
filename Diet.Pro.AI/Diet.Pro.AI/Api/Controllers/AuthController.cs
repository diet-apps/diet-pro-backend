using Diet.Pro.AI.Aplication.Comands.Login.Handlers;
using Diet.Pro.AI.Aplication.Common.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diet.Pro.AI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(new LoginCommand(request));
            return Ok(result);
        }
    }
}
