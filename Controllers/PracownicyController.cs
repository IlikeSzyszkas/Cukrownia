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
    public class PracownicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracownicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pracownicy
        public async Task<IActionResult> Index()
        {
            var pracownicy = await _context.Pracownicy
                .Include(p => p.Dzial)
                .Include(g => g.Stanowisko)
                .ToListAsync();

            foreach (var d in pracownicy)
            {
                d.LiczbaZmian_pak = d.Zmiany_pak?.Count ?? 0;
                d.LiczbaZmian_prod = d.Zmiany_prod?.Count ?? 0;
            }

            return View(pracownicy);
        }

        // GET: Pracownicy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownicy = await _context.Pracownicy
                .Include(p => p.Dzial)
                .Include(g => g.Stanowisko)
                .Include(a => a.Zmiany_pak)
                .Include(r => r.Zmiany_prod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownicy == null)
            {
                return NotFound();
            }

            return View(pracownicy);
        }

        // GET: Pracownicy/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Id_dzialu = new SelectList(await _context.Dzialy.ToListAsync(), "Id_dzialu", "Nazwa");
            ViewBag.Id_stanowiska = new SelectList(await _context.Stanowiska.ToListAsync(), "Id_stanowiska", "Nazwa");
            return View();
        }

        // POST: Pracownicy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Addres,Nr_tel,Id_stanowiska,Id_dzialu")] Pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                var stanowisko = await _context.Stanowiska.FindAsync(pracownicy.Id_stanowiska);
                var dzial = await _context.Dzialy.FindAsync(pracownicy.Id_dzialu);
                pracownicy.Stanowisko = stanowisko;
                pracownicy.Dzial = dzial;
                _context.Add(pracownicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pracownicy);
        }

        // GET: Pracownicy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownicy = await _context.Pracownicy.FindAsync(id);
            if (pracownicy == null)
            {
                return NotFound();
            }
            ViewBag.Id_dzialu = new SelectList(await _context.Dzialy.ToListAsync(), "Id_dzialu", "Nazwa");
            ViewBag.Id_stanowiska = new SelectList(await _context.Stanowiska.ToListAsync(), "Id_stanowiska", "Nazwa");
            return View(pracownicy);
        }

        // POST: Pracownicy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Addres,Nr_tel,Id_stanowiska,Id_dzialu")] Pracownicy pracownicy)
        {
            if (id != pracownicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var stanowisko = await _context.Stanowiska.FindAsync(pracownicy.Id_stanowiska);
                    var dzial = await _context.Dzialy.FindAsync(pracownicy.Id_dzialu);
                    pracownicy.Stanowisko = stanowisko;
                    pracownicy.Dzial = dzial;
                    _context.Update(pracownicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracownicyExists(pracownicy.Id))
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
            return View(pracownicy);
        }

        // GET: Pracownicy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownicy = await _context.Pracownicy
                .Include(p => p.Dzial)
                .Include(g => g.Stanowisko)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownicy == null)
            {
                return NotFound();
            }

            return View(pracownicy);
        }

        // POST: Pracownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pracownicy = await _context.Pracownicy.FindAsync(id);
            if (pracownicy != null)
            {
                _context.Pracownicy.Remove(pracownicy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracownicyExists(int id)
        {
            return _context.Pracownicy.Any(e => e.Id == id);
        }
    }
}
