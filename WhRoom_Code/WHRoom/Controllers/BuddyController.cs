using Microsoft.AspNetCore.Mvc;

namespace WHRoom.Controllers
{
    public class BuddyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
