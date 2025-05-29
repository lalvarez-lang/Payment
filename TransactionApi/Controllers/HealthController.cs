using Microsoft.AspNetCore.Mvc;

namespace Transaction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("TransactionService is running");

    }
}
