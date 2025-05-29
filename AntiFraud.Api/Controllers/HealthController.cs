using Microsoft.AspNetCore.Mvc;

namespace AntiFraud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("AntiFraudService is running");
    }
}
