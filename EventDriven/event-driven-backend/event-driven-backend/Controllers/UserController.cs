using Microsoft.AspNetCore.Mvc;

namespace event_driven_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private private Context _context;

        public UserController(ILogger<UserController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> imefunkcije()
        {
            return Ok();
        }


    }
}
