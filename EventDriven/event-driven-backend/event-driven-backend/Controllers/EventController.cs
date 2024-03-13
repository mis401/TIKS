using Microsoft.AspNetCore.Mvc;

namespace event_driven_backend.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
