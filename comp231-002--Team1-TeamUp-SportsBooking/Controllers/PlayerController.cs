using Microsoft.AspNetCore.Mvc;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class PlayerController : Controller
    {
        // GET: /Player
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Player/Edit
        // This is a frontend-only placeholder. Backend team will later bind real profile data.
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
