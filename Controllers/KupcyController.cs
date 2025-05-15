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
    public class KupcyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KupcyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kupcy
        public async Task<IActionResult> Index()
        {
            var kupcy = await _context.Kupcy
                .Include(d => d.Transakcje)
                .ToListAsync();
            foreach(var k in kupcy)
            {
                k.LiczbaTransakcji = k.Transakcje.Count();
            }
            return View(kupcy);
        }

        // GET: Kupcy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupcy = await _context.Kupcy
                .FirstOrDefaultAsync(m => m.Id_kupca == id);
            if (kupcy == null)
            {
                return NotFound();
            }

            return View(kupcy);
        }

        // GET: Kupcy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kupcy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_kupca,Nazwa,Nip,Adres,Nr_tel")] Kupcy kupcy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kupcy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kupcy);
        }

        // GET: Kupcy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupcy = await _context.Kupcy.FindAsync(id);
            if (kupcy == null)
            {
                return NotFound();
            }
            return View(kupcy);
        }

        // POST: Kupcy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_kupca,Nazwa,Nip,Adres,Nr_tel")] Kupcy kupcy)
        {
            if (id != kupcy.Id_kupca)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kupcy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KupcyExists(kupcy.Id_kupca))
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
            return View(kupcy);
        }

        // GET: Kupcy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupcy = await _context.Kupcy
                .FirstOrDefaultAsync(m => m.Id_kupca == id);
            if (kupcy == null)
            {
                return NotFound();
            }

            return View(kupcy);
        }

        // POST: Kupcy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kupcy = await _context.Kupcy.FindAsync(id);
            if (kupcy != null)
            {
                _context.Kupcy.Remove(kupcy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KupcyExists(int id)
        {
            return _context.Kupcy.Any(e => e.Id_kupca == id);
        }
    }
}
