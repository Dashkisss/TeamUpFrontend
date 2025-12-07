using System;
using System.Linq;
using System.Threading.Tasks;
using comp231_002__Team1_TeamUp_SportsBooking.Data;
using comp231_002__Team1_TeamUp_SportsBooking.Models;
using comp231_002__Team1_TeamUp_SportsBooking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class CourtsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NotificationService _notificationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourtsController(
            ApplicationDbContext context,
            NotificationService notificationService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        // GET: /Courts
        public async Task<IActionResult> Index()
        {
            var courts = await _context.Courts.ToListAsync();
            return View(courts);
        }

        // GET: /Courts/Book/5
        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var court = await _context.Courts
                .FirstOrDefaultAsync(c => c.CourtID == id);

            if (court == null) return NotFound();

            ViewBag.CourtId = court.CourtID;
            ViewBag.CourtName = court.Location;

            var booking = new Booking
            {
                CourtID = court.CourtID,
                BookingDate = DateTime.Today
            };

            return View(booking);
        }

        // POST: /Courts/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(Booking booking)
        {
            var court = await _context.Courts
                .FirstOrDefaultAsync(c => c.CourtID == booking.CourtID);

            if (court == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.CourtId = booking.CourtID;
                ViewBag.CourtName = court.Location;
                return View(booking);
            }

            // TEMP: PlayerID = 1 because Player identity not wired
            booking.PlayerID = 1;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // ⭐ ALWAYS create a notification (logged in OR not logged in)
            string userId = User.Identity.IsAuthenticated
                ? _userManager.GetUserId(User)
                : "system"; // saves even when not logged in

            await _notificationService.CreateNotificationAsync(
                userId,
                $"You booked Court #{booking.CourtID} on {booking.BookingDate:MMM dd yyyy} at {booking.TimeSlot}."
            );

            TempData["SuccessMessage"] = "Your booking has been created successfully!";

            return RedirectToAction("Index");
        }

    }
}
