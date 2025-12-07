using comp231_002__Team1_TeamUp_SportsBooking.Data;
using comp231_002__Team1_TeamUp_SportsBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Player
        public IActionResult Index()
        {
            return View();   // sẽ cần Views/Player/Index.cshtml
        }

        // GET: /Player/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Players = await _context.Players.ToListAsync();
            ViewBag.Courts = await _context.Courts.ToListAsync();

            var booking = new Booking
            {
                BookingDate = DateTime.Today
            };

            return View(booking);
        }

        // POST: /Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Players = await _context.Players.ToListAsync();
                ViewBag.Courts = await _context.Courts.ToListAsync();
                return View(booking);
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // ⭐ NEW: /Player/Bookings – My bookings
        [HttpGet]
        public async Task<IActionResult> Bookings()
        {
            // tạm thời lấy tất cả bookings, sau này m lọc theo PlayerID cũng được
            var bookings = await _context.Bookings
                .Include(b => b.Court)
                .ToListAsync();

            return View(bookings);   // dùng Views/Player/Bookings.cshtml
        }
    }
}
