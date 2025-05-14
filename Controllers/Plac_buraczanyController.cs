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
    public class Plac_buraczanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Plac_buraczanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plac_buraczany
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plac_buraczany.ToListAsync());
        }

        // GET: Plac_buraczany/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plac_buraczany = await _context.Plac_buraczany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plac_buraczany == null)
            {
                return NotFound();
            }

            return View(plac_buraczany);
        }

        // GET: Plac_buraczany/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plac_buraczany/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_dostawy,Ilosc_burakow,Data_operacji")] Plac_buraczany plac_buraczany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plac_buraczany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plac_buraczany);
        }

        // GET: Plac_buraczany/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plac_buraczany = await _context.Plac_buraczany.FindAsync(id);
            if (plac_buraczany == null)
            {
                return NotFound();
            }
            return View(plac_buraczany);
        }

        // POST: Plac_buraczany/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_dostawy,Ilosc_burakow,Data_operacji")] Plac_buraczany plac_buraczany)
        {
            if (id != plac_buraczany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plac_buraczany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Plac_buraczanyExists(plac_buraczany.Id))
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
            return View(plac_buraczany);
        }

        // GET: Plac_buraczany/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plac_buraczany = await _context.Plac_buraczany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plac_buraczany == null)
            {
                return NotFound();
            }

            return View(plac_buraczany);
        }

        // POST: Plac_buraczany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plac_buraczany = await _context.Plac_buraczany.FindAsync(id);
            if (plac_buraczany != null)
            {
                _context.Plac_buraczany.Remove(plac_buraczany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Plac_buraczanyExists(int id)
        {
            return _context.Plac_buraczany.Any(e => e.Id == id);
        }
    }
}
