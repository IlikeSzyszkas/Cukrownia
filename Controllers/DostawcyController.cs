using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;
using Projekt2.ViewModels;

namespace Projekt2.Controllers
{
    public class DostawcyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DostawcyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dostawcy
        public async Task<IActionResult> Index()
        {
            var dostawcy  = await _context.Dostawcy
                .Include(d => d.Dostawy)
                .ToListAsync();

            foreach (var d in dostawcy)
            {
                d.LiczbaDostaw = d.Dostawy?.Count ?? 0;
            }
            return View(dostawcy);
        }

        // GET: Dostawcy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawcy = await _context.Dostawcy
                .Include(d => d.Dostawy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dostawcy == null)
            {
                return NotFound();
            }

            return View(dostawcy);
        }
        // GET: Dostawcy/Statystyki
        public async Task<IActionResult> Statystyki()
        {
            var bestDostawca = await _context.Dostawcy
                .Include(d => d.Dostawy)
                .OrderByDescending(d => d.Dostawy.Sum(d => d.Ilosc_towaru))
                .FirstOrDefaultAsync();
            
            var worstDostawca = await _context.Dostawcy
                .Include(d => d.Dostawy)
                .OrderBy(d => d.Dostawy.Sum(d => d.Ilosc_towaru))
                .FirstOrDefaultAsync();

            var bestPole = await _context.Dostawcy
                .Include(d => d.Dostawy)
                .Where(d => d.Ilosc_ha_pola > 0 && d.Dostawy.Any())
                .Select(d => new
                {
                    Dostawca = d,
                    Wydajnosc = d.Dostawy.Sum(x => x.Ilosc_towaru) / d.Ilosc_ha_pola
                })
                .OrderByDescending(x => x.Wydajnosc)
                .Select(x => x.Dostawca)
                .FirstOrDefaultAsync();

            var model = new DostawcyStatystykiViewModel
            {
                BestDostawca = bestDostawca,
                WorstDostawca = worstDostawca,
                BestPole = bestPole
            };

            return View(model);
        }


        // GET: Dostawcy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dostawcy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Addres,Nr_tel,Ilosc_ha_pola")] Dostawcy dostawcy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dostawcy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dostawcy);
        }

        // GET: Dostawcy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawcy = await _context.Dostawcy.FindAsync(id);
            if (dostawcy == null)
            {
                return NotFound();
            }
            return View(dostawcy);
        }

        // POST: Dostawcy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Addres,Nr_tel,Ilosc_ha_pola")] Dostawcy dostawcy)
        {
            if (id != dostawcy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dostawcy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DostawcyExists(dostawcy.Id))
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
            return View(dostawcy);
        }

        // GET: Dostawcy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawcy = await _context.Dostawcy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dostawcy == null)
            {
                return NotFound();
            }

            return View(dostawcy);
        }

        // POST: Dostawcy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dostawcy = await _context.Dostawcy.FindAsync(id);
            if (dostawcy != null)
            {
                _context.Dostawcy.Remove(dostawcy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DostawcyExists(int id)
        {
            return _context.Dostawcy.Any(e => e.Id == id);
        }
    }
}
