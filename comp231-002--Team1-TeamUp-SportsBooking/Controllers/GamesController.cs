using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using comp231_002__Team1_TeamUp_SportsBooking.Data;
using comp231_002__Team1_TeamUp_SportsBooking.Models;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        // Общий список игр (можно привязать к "Games" или admin-страницам)
        public async Task<IActionResult> Index()
        {
            var games = await _context.Games
                .OrderByDescending(g => g.GameDate)
                .ToListAsync();

            return View(games);
        }

        // GET: Games/MyGames
        // Страница "My Games" из navbar.
        // Пока показывает все игры, позже бэкенд добавит фильтр по текущему пользователю.
        public async Task<IActionResult> MyGames()
        {
            var games = await _context.Games
                .OrderByDescending(g => g.GameDate)
                .ToListAsync();

            return View(games);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.GameID == id);

            if (game == null)
                return NotFound();

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameID,TeamId,CourtID,GameDate,Status")] Game game)
        {
            if (!ModelState.IsValid)
                return View(game);

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameID,TeamId,CourtID,GameDate,Status")] Game game)
        {
            if (id != game.GameID)
                return NotFound();

            if (!ModelState.IsValid)
                return View(game);

            try
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.GameID))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.GameID == id);

            if (game == null)
                return NotFound();

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameID == id);
        }
    }
}
