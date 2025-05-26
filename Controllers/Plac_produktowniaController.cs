using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;

namespace Projekt2.Controllers
{
    public class Plac_produktowniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Plac_produktowniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plac_produktownia
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 50;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)_context.Plac_produktownia.Count() / pageSize);

            return View(await _context.Plac_produktownia
                .OrderBy(m => m.Data_pobrania)
                .ThenBy(u => u.Id_dostawy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Plac_produktownia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plac_produktownia = await _context.Plac_produktownia
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plac_produktownia == null)
            {
                return NotFound();
            }

            return View(plac_produktownia);
        }

        // GET: Plac_produktownia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plac_produktownia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_dostawy,Id_partii,Ilosc_burakow_pobrana,Data_pobrania")] Plac_produktownia plac_produktownia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plac_produktownia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plac_produktownia);
        }

        // GET: Plac_produktownia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plac_produktownia = await _context.Plac_produktownia.FindAsync(id);
            if (plac_produktownia == null)
            {
                return NotFound();
            }
            return View(plac_produktownia);
        }

        // POST: Plac_produktownia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_dostawy,Id_partii,Ilosc_burakow_pobrana,Data_pobrania")] Plac_produktownia plac_produktownia)
        {
            if (id != plac_produktownia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plac_produktownia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Plac_produktowniaExists(plac_produktownia.Id))
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
            return View(plac_produktownia);
        }

        // GET: Plac_produktownia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plac_produktownia = await _context.Plac_produktownia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plac_produktownia == null)
            {
                return NotFound();
            }

            return View(plac_produktownia);
        }

        // POST: Plac_produktownia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plac_produktownia = await _context.Plac_produktownia.FindAsync(id);
            if (plac_produktownia != null)
            {
                _context.Plac_produktownia.Remove(plac_produktownia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Plac_produktowniaExists(int id)
        {
            return _context.Plac_produktownia.Any(e => e.Id == id);
        }
    }
}
