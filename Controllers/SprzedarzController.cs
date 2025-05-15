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
    public class SprzedarzController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SprzedarzController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sprzedarz
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sprzedarz
                .Include(k => k.Kupiec)
                .ToListAsync()
                );
        }

        // GET: Sprzedarz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprzedarz = await _context.Sprzedarz
                .Include(k => k.Kupiec)
                .FirstOrDefaultAsync(m => m.Id_transakcji == id);
            if (sprzedarz == null)
            {
                return NotFound();
            }

            return View(sprzedarz);
        }

        // GET: Sprzedarz/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Id_kupca = new SelectList(await _context.Kupcy.ToListAsync(), "Id_kupca", "Nazwa");
            return View();
        }

        // POST: Sprzedarz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_transakcji,Ilosc_opakowan,Data_odbioru,Id_kupca")] Sprzedarz sprzedarz)
        {
            if (ModelState.IsValid)
            {
                var kupiec = await _context.Kupcy.FindAsync(sprzedarz.Id_kupca);
                sprzedarz.Kupiec = kupiec;
                _context.Add(sprzedarz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sprzedarz);
        }

        // GET: Sprzedarz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Id_kupca = new SelectList(await _context.Kupcy.ToListAsync(), "Id_kupca", "Nazwa");
            if (id == null)
            {
                return NotFound();
            }

            var sprzedarz = await _context.Sprzedarz.FindAsync(id);
            if (sprzedarz == null)
            {
                return NotFound();
            }
            return View(sprzedarz);
        }

        // POST: Sprzedarz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_transakcji,Ilosc_opakowan,Data_odbioru,Id_kupca")] Sprzedarz sprzedarz)
        {
            if (id != sprzedarz.Id_transakcji)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var kupiec = await _context.Kupcy.FindAsync(sprzedarz.Id_kupca);
                    sprzedarz.Kupiec = kupiec;
                    _context.Update(sprzedarz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SprzedarzExists(sprzedarz.Id_transakcji))
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
            return View(sprzedarz);
        }

        // GET: Sprzedarz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprzedarz = await _context.Sprzedarz
                .Include(k => k.Kupiec)
                .FirstOrDefaultAsync(m => m.Id_transakcji == id);
            if (sprzedarz == null)
            {
                return NotFound();
            }

            return View(sprzedarz);
        }

        // POST: Sprzedarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sprzedarz = await _context.Sprzedarz.FindAsync(id);
            if (sprzedarz != null)
            {
                _context.Sprzedarz.Remove(sprzedarz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SprzedarzExists(int id)
        {
            return _context.Sprzedarz.Any(e => e.Id_transakcji == id);
        }
    }
}
