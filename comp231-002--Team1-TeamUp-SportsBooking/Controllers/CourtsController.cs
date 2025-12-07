using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using comp231_002__Team1_TeamUp_SportsBooking.Data;
using comp231_002__Team1_TeamUp_SportsBooking.Models;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class CourtsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourtsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courts
        // Страница со списком кортов (используется в navbar)
        public async Task<IActionResult> Index()
        {
            var courts = await _context.Courts
                .OrderBy(c => c.Location)
                .ToListAsync();

            return View(courts);
        }

        // GET: Courts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var court = await _context.Courts
                .FirstOrDefaultAsync(m => m.CourtID == id);

            if (court == null)
                return NotFound();

            return View(court);
        }

        // GET: Courts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourtID,Location,SurfaceType,IsAvailable")] Court court)
        {
            if (!ModelState.IsValid)
                return View(court);

            _context.Courts.Add(court);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Courts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var court = await _context.Courts.FindAsync(id);
            if (court == null)
                return NotFound();

            return View(court);
        }

        // POST: Courts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourtID,Location,SurfaceType,IsAvailable")] Court court)
        {
            if (id != court.CourtID)
                return NotFound();

            if (!ModelState.IsValid)
                return View(court);

            try
            {
                _context.Update(court);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourtExists(court.CourtID))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Courts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var court = await _context.Courts
                .FirstOrDefaultAsync(m => m.CourtID == id);

            if (court == null)
                return NotFound();

            return View(court);
        }

        // POST: Courts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court != null)
            {
                _context.Courts.Remove(court);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Дополнительное действие под кнопку "Book this court" (будет удобно бэкенду)
        // GET: Courts/Book/5
        public async Task<IActionResult> Book(int? id)
        {
            if (id == null)
                return NotFound();

            var court = await _context.Courts
                .FirstOrDefaultAsync(c => c.CourtID == id);

            if (court == null)
                return NotFound();

            // Сейчас просто показываем форму/подтверждение.
            // Бэкенд потом добавит сюда реальную логику бронирования.
            return View(court);
        }

        private bool CourtExists(int id)
        {
            return _context.Courts.Any(e => e.CourtID == id);
        }
    }
}
