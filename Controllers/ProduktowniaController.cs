using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;

namespace Projekt2.Controllers
{
    public class ProduktowniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktowniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produktownia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produktownia.ToListAsync());
        }

        // GET: Produktownia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktownia = await _context.Produktownia
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (produktownia == null)
            {
                return NotFound();
            }

            return View(produktownia);
        }

        // GET: Produktownia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produktownia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Produktownia produktownia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produktownia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produktownia);
        }

        // GET: Produktownia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktownia = await _context.Produktownia.FindAsync(id);
            if (produktownia == null)
            {
                return NotFound();
            }
            return View(produktownia);
        }

        // POST: Produktownia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Produktownia produktownia)
        {
            if (id != produktownia.Id_partii)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produktownia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktowniaExists(produktownia.Id_partii))
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
            return View(produktownia);
        }

        // GET: Produktownia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktownia = await _context.Produktownia
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (produktownia == null)
            {
                return NotFound();
            }

            return View(produktownia);
        }

        // POST: Produktownia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produktownia = await _context.Produktownia.FindAsync(id);
            if (produktownia != null)
            {
                _context.Produktownia.Remove(produktownia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktowniaExists(int id)
        {
            return _context.Produktownia.Any(e => e.Id_partii == id);
        }
    }
}
