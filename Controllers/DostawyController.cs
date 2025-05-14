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
    public class DostawyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DostawyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dostawy
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dostawy.ToListAsync());
        }

        // GET: Dostawy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawy = await _context.Dostawy
                .FirstOrDefaultAsync(m => m.Id_dostawy == id);
            if (dostawy == null)
            {
                return NotFound();
            }

            return View(dostawy);
        }

        // GET: Dostawy/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Id_dostawcy = new SelectList(await _context.Dostawcy.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Dostawy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_dostawy,Ilosc_towaru,Data_dostawy,Id_dostawcy")] Dostawy dostawy)
        {
            if (ModelState.IsValid)
            {
                var dostawca = await _context.Dostawcy.FindAsync(dostawy.Id_dostawcy);
                dostawy.Dostawca = dostawca;
                _context.Add(dostawy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dostawy);
        }

        // GET: Dostawy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawy = await _context.Dostawy.FindAsync(id);
            if (dostawy == null)
            {
                return NotFound();
            }
            ViewBag.Id_dostawcy = new SelectList(await _context.Dostawcy.ToListAsync(), "Id", "Name");
            return View(dostawy);
        }

        // POST: Dostawy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_dostawy,Ilosc_towaru,Data_dostawy,Id_dostawcy")] Dostawy dostawy)
        {
            if (id != dostawy.Id_dostawy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dostawy.Dostawca = await _context.Dostawcy.FindAsync(dostawy.Id_dostawcy);
                    _context.Update(dostawy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DostawyExists(dostawy.Id_dostawy))
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
            return View(dostawy);
        }

        // GET: Dostawy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawy = await _context.Dostawy
                .FirstOrDefaultAsync(m => m.Id_dostawy == id);
            if (dostawy == null)
            {
                return NotFound();
            }

            return View(dostawy);
        }

        // POST: Dostawy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dostawy = await _context.Dostawy.FindAsync(id);
            if (dostawy != null)
            {
                _context.Dostawy.Remove(dostawy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DostawyExists(int id)
        {
            return _context.Dostawy.Any(e => e.Id_dostawy == id);
        }
    }
}
