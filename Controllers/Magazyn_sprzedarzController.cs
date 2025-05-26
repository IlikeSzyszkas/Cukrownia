using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;

namespace Projekt2.Controllers
{
    public class Magazyn_sprzedarzController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Magazyn_sprzedarzController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Magazyn_sprzedarz
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 50;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)_context.Magazyn_sprzedarz.Count() / pageSize);

            return View(await _context.Magazyn_sprzedarz
                .OrderBy(m => m.Id_operacji)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Magazyn_sprzedarz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazyn_sprzedarz = await _context.Magazyn_sprzedarz
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazyn_sprzedarz == null)
            {
                return NotFound();
            }

            return View(magazyn_sprzedarz);
        }

        // GET: Magazyn_sprzedarz/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Magazyn_sprzedarz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_operacji,Id_transakcji,Ilosc_opakowan_sprzedanych")] Magazyn_sprzedarz magazyn_sprzedarz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magazyn_sprzedarz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazyn_sprzedarz);
        }

        // GET: Magazyn_sprzedarz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazyn_sprzedarz = await _context.Magazyn_sprzedarz.FindAsync(id);
            if (magazyn_sprzedarz == null)
            {
                return NotFound();
            }
            return View(magazyn_sprzedarz);
        }

        // POST: Magazyn_sprzedarz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_operacji,Id_transakcji,Ilosc_opakowan_sprzedanych")] Magazyn_sprzedarz magazyn_sprzedarz)
        {
            if (id != magazyn_sprzedarz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazyn_sprzedarz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Magazyn_sprzedarzExists(magazyn_sprzedarz.Id))
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
            return View(magazyn_sprzedarz);
        }

        // GET: Magazyn_sprzedarz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazyn_sprzedarz = await _context.Magazyn_sprzedarz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazyn_sprzedarz == null)
            {
                return NotFound();
            }

            return View(magazyn_sprzedarz);
        }

        // POST: Magazyn_sprzedarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazyn_sprzedarz = await _context.Magazyn_sprzedarz.FindAsync(id);
            if (magazyn_sprzedarz != null)
            {
                _context.Magazyn_sprzedarz.Remove(magazyn_sprzedarz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Magazyn_sprzedarzExists(int id)
        {
            return _context.Magazyn_sprzedarz.Any(e => e.Id == id);
        }
    }
}
