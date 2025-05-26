using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;

namespace Projekt2.Controllers
{
    public class SilosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SilosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Silos
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 50;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)_context.Silos.Count() / pageSize);

            return View(await _context.Silos
                .OrderBy(m => m.Data_skladowania)
                .ThenBy(u => u.Id_operacji)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Silos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silos = await _context.Silos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (silos == null)
            {
                return NotFound();
            }

            return View(silos);
        }

        // GET: Silos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Silos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_operacji,Ilosc_cukru,Data_skladowania")] Silos silos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(silos);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(silos);
        }


        // GET: Silos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silos = await _context.Silos.FindAsync(id);
            if (silos == null)
            {
                return NotFound();
            }
            return View(silos);
        }

        // POST: Silos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_operacji,Ilosc_cukru,Data_skladowania")] Silos silos)
        {
            if (id != silos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(silos);
                    await _context.SaveChangesAsync();
                    UpdateSilosAsync(silos.Id_operacji, silos.Ilosc_cukru, silos.Data_skladowania);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SilosExists(silos.Id))
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
            return View(silos);
        }
        private async Task UpdateSilosAsync(int id_operacji, int ilosc, DateTime data)
        {
            var silos = await _context.Silos
                .FirstOrDefaultAsync(p => p.Id_operacji == id_operacji);

            if (silos != null)
            {
                silos.Id_operacji = id_operacji;
                silos.Ilosc_cukru = ilosc;
                silos.Data_skladowania = data;
                _context.Silos.Update(silos);
                await _context.SaveChangesAsync();
            }
        }
        // GET: Silos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silos = await _context.Silos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (silos == null)
            {
                return NotFound();
            }

            return View(silos);
        }

        // POST: Silos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var silos = await _context.Silos.FindAsync(id);
            if (silos != null)
            {
                _context.Silos.Remove(silos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SilosExists(int id)
        {
            return _context.Silos.Any(e => e.Id == id);
        }
    }
}
