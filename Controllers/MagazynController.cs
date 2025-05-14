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
    public class MagazynController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MagazynController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Magazyn
        public async Task<IActionResult> Index()
        {
            return View(await _context.Magazyn.ToListAsync());
        }

        // GET: Magazyn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazyn = await _context.Magazyn
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazyn == null)
            {
                return NotFound();
            }

            return View(magazyn);
        }

        // GET: Magazyn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Magazyn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_operacji,Ilosc_opakowan,Data_operacji")] Magazyn magazyn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magazyn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazyn);
        }

        // GET: Magazyn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazyn = await _context.Magazyn.FindAsync(id);
            if (magazyn == null)
            {
                return NotFound();
            }
            return View(magazyn);
        }

        // POST: Magazyn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_operacji,Ilosc_opakowan,Data_operacji")] Magazyn magazyn)
        {
            if (id != magazyn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazyn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazynExists(magazyn.Id))
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
            return View(magazyn);
        }

        // GET: Magazyn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazyn = await _context.Magazyn
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazyn == null)
            {
                return NotFound();
            }

            return View(magazyn);
        }

        // POST: Magazyn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazyn = await _context.Magazyn.FindAsync(id);
            if (magazyn != null)
            {
                _context.Magazyn.Remove(magazyn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazynExists(int id)
        {
            return _context.Magazyn.Any(e => e.Id == id);
        }
    }
}
