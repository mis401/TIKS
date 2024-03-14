using Microsoft.AspNetCore.Mvc;

namespace event_driven_backend.Controllers;

    [ApiController]
    [Route("[controller]")]

    public class CommunityController : ControllerBase
    {
        private readonly ILogger<CommunityController> _logger;

        public CommunityController(ILogger<CommunityController> logger)
        {
            _logger = logger;
        }

        
        
    }

