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
    public class PakowniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PakowniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pakownia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pakownia.ToListAsync());
        }

        // GET: Pakownia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pakownia = await _context.Pakownia
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (pakownia == null)
            {
                return NotFound();
            }

            return View(pakownia);
        }

        // GET: Pakownia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pakownia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Pakownia pakownia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pakownia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pakownia);
        }

        // GET: Pakownia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pakownia = await _context.Pakownia.FindAsync(id);
            if (pakownia == null)
            {
                return NotFound();
            }
            return View(pakownia);
        }

        // POST: Pakownia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Pakownia pakownia)
        {
            if (id != pakownia.Id_partii)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pakownia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PakowniaExists(pakownia.Id_partii))
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
            return View(pakownia);
        }

        // GET: Pakownia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pakownia = await _context.Pakownia
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (pakownia == null)
            {
                return NotFound();
            }

            return View(pakownia);
        }

        // POST: Pakownia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pakownia = await _context.Pakownia.FindAsync(id);
            if (pakownia != null)
            {
                _context.Pakownia.Remove(pakownia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PakowniaExists(int id)
        {
            return _context.Pakownia.Any(e => e.Id_partii == id);
        }
    }
}
