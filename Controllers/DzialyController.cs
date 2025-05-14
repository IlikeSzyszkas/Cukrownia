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
    public class DzialyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DzialyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dzialies
        public async Task<IActionResult> Index()
        {
            var dzial = await _context.Dzialy
                .Include(d => d.Pracownicy)
                .ToListAsync();

            foreach (var p in dzial)
            {
                p.LiczbaPracownikow = p.Pracownicy?.Count ?? 0;
            }
            return View(dzial);
        }

        // GET: Dzialies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dzialy = await _context.Dzialy
                .FirstOrDefaultAsync(m => m.Id_dzialu == id);
            if (dzialy == null)
            {
                return NotFound();
            }

            return View(dzialy);
        }

        // GET: Dzialies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dzialies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_dzialu,Nazwa")] Dzialy dzialy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dzialy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dzialy);
        }

        // GET: Dzialies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dzialy = await _context.Dzialy.FindAsync(id);
            if (dzialy == null)
            {
                return NotFound();
            }
            return View(dzialy);
        }

        // POST: Dzialies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_dzialu,Nazwa")] Dzialy dzialy)
        {
            if (id != dzialy.Id_dzialu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dzialy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DzialyExists(dzialy.Id_dzialu))
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
            return View(dzialy);
        }

        // GET: Dzialies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dzialy = await _context.Dzialy
                .FirstOrDefaultAsync(m => m.Id_dzialu == id);
            if (dzialy == null)
            {
                return NotFound();
            }

            return View(dzialy);
        }

        // POST: Dzialies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dzialy = await _context.Dzialy.FindAsync(id);
            if (dzialy != null)
            {
                _context.Dzialy.Remove(dzialy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DzialyExists(int id)
        {
            return _context.Dzialy.Any(e => e.Id_dzialu == id);
        }
    }
}
