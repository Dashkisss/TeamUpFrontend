using Microsoft.AspNetCore.Mvc;
using comp231_002__Team1_TeamUp_SportsBooking.Services;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    [Route("api/notifications")]
    public class NotificationController : Controller
    {
        private readonly NotificationService _service;

        public NotificationController(NotificationService service)
        {
            _service = service;
        }

        // ⭐ This MUST match /api/notifications/list
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var notifications = await _service.GetAllNotificationsAsync();
            return Json(notifications);
        }
    }
}
