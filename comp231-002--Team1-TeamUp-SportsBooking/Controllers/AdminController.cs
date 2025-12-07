using comp231_002__Team1_TeamUp_SportsBooking.Data;
using comp231_002__Team1_TeamUp_SportsBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ===============================
        // ADMIN DASHBOARD
        // ===============================
        public async Task<IActionResult> Index()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalCourts = await _context.Courts.CountAsync();
            var totalBookings = await _context.Bookings.CountAsync();

            ViewBag.Users = totalUsers;
            ViewBag.Courts = totalCourts;
            ViewBag.Bookings = totalBookings;

            return View();
        }

        // ===============================
        // USERS LIST
        // ===============================
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // ===============================
        // COURTS LIST
        // ===============================
        public async Task<IActionResult> Courts()
        {
            var courts = await _context.Courts.ToListAsync();
            return View(courts);
        }

        // ===============================
        // EDIT COURT (GET)
        // ===============================
        [HttpGet]
        public async Task<IActionResult> EditCourt(int id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
                return NotFound();

            return View(court);
        }

        // ===============================
        // EDIT COURT (POST)
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourt(int id, Court court)
        {
            if (id != court.CourtID)
                return NotFound();

            if (!ModelState.IsValid)
                return View(court);

            _context.Update(court);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Courts));
        }

        // ===============================
        // CREATE COURT
        // ===============================
        [HttpGet]
        public IActionResult CreateCourt()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourt(Court court)
        {
            if (!ModelState.IsValid)
                return View(court);

            _context.Courts.Add(court);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Courts));
        }

        // ===============================
        // DELETE COURT
        // ===============================
        [HttpPost]
        public async Task<IActionResult> DeleteCourt(int id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
                return NotFound();

            _context.Courts.Remove(court);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Courts));
        }
    }
}
