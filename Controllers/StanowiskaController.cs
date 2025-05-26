using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;

namespace Projekt2.Controllers
{
    public class StanowiskaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StanowiskaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stanowiska
        public async Task<IActionResult> Index()
        {
            var stanowiska = await _context.Stanowiska
                .Include(d => d.Pracownicy)
                .AsNoTracking()
                .ToListAsync();
            foreach (var p in stanowiska)
            {
                p.LiczbaPracownikow = p.Pracownicy?.Count() ?? 0;
            }
            return View(stanowiska);
        }

        // GET: Stanowiska/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanowiska = await _context.Stanowiska
                .Include(d => d.Pracownicy)
                    .ThenInclude(s => s.Dzial)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id_stanowiska == id);
            if (stanowiska == null)
            {
                return NotFound();
            }

            return View(stanowiska);
        }

        // GET: Stanowiska/Statystyki
        public async Task<IActionResult> Statystyki(bool iframe = false)
        {
            var stanowiskaData = await _context.Stanowiska
                .Select(x => new
                {
                    x.Nazwa,
                    x.Pracownicy.Count
                })
                .AsNoTracking()
                .ToListAsync();

            ViewBag.ChartLabels3 = stanowiskaData.Select(x => x.Nazwa ?? string.Empty).ToList();
            ViewBag.ChartValues3 = stanowiskaData.Select(x => x.Count).ToList();

            ViewBag.DisableLayout = iframe;
            return View();
        }

        // GET: Stanowiska/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stanowiska/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_stanowiska,Nazwa")] Stanowiska stanowiska)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stanowiska);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stanowiska);
        }

        // GET: Stanowiska/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanowiska = await _context.Stanowiska.FindAsync(id);
            if (stanowiska == null)
            {
                return NotFound();
            }
            return View(stanowiska);
        }

        // POST: Stanowiska/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_stanowiska,Nazwa")] Stanowiska stanowiska)
        {
            if (id != stanowiska.Id_stanowiska)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stanowiska);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanowiskaExists(stanowiska.Id_stanowiska))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stanowiska);
        }

        // GET: Stanowiska/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanowiska = await _context.Stanowiska
                .FirstOrDefaultAsync(m => m.Id_stanowiska == id);
            if (stanowiska == null)
            {
                return NotFound();
            }

            return View(stanowiska);
        }

        // POST: Stanowiska/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stanowiska = await _context.Stanowiska.FindAsync(id);
            if (stanowiska != null)
            {
                _context.Stanowiska.Remove(stanowiska);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StanowiskaExists(int id)
        {
            return _context.Stanowiska.Any(e => e.Id_stanowiska == id);
        }
    }
}
