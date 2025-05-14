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
    public class Silos_pakowniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Silos_pakowniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Silos_pakownia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Silos_pakownia.ToListAsync());
        }

        // GET: Silos_pakownia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silos_pakownia = await _context.Silos_pakownia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (silos_pakownia == null)
            {
                return NotFound();
            }

            return View(silos_pakownia);
        }

        // GET: Silos_pakownia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Silos_pakownia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_operacji,Id_partii,Ilosc_cukru_pobrana,Data_pobrania")] Silos_pakownia silos_pakownia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(silos_pakownia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(silos_pakownia);
        }

        // GET: Silos_pakownia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silos_pakownia = await _context.Silos_pakownia.FindAsync(id);
            if (silos_pakownia == null)
            {
                return NotFound();
            }
            return View(silos_pakownia);
        }

        // POST: Silos_pakownia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_operacji,Id_partii,Ilosc_cukru_pobrana,Data_pobrania")] Silos_pakownia silos_pakownia)
        {
            if (id != silos_pakownia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(silos_pakownia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Silos_pakowniaExists(silos_pakownia.Id))
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
            return View(silos_pakownia);
        }

        // GET: Silos_pakownia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silos_pakownia = await _context.Silos_pakownia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (silos_pakownia == null)
            {
                return NotFound();
            }

            return View(silos_pakownia);
        }

        // POST: Silos_pakownia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var silos_pakownia = await _context.Silos_pakownia.FindAsync(id);
            if (silos_pakownia != null)
            {
                _context.Silos_pakownia.Remove(silos_pakownia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Silos_pakowniaExists(int id)
        {
            return _context.Silos_pakownia.Any(e => e.Id == id);
        }
    }
}
