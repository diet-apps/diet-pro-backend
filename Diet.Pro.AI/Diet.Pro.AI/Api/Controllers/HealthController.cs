using Microsoft.AspNetCore.Mvc;

namespace Diet.Pro.AI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("OK");
    }
}
