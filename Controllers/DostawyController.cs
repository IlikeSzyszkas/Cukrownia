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
            var dostawy = await _context.Dostawy
                .Include(d => d.Dostawca)
                .ToListAsync();
            return View(dostawy);
        }

        // GET: Dostawy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawy = await _context.Dostawy
                .Include(d => d.Dostawca)
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
            var dostawcy = await _context.Dostawcy.ToListAsync();
            var selectList = dostawcy.Select(k => new
            {
                Id = k.Id,
                FullName = k.Name + " " + k.Surname
            });
            ViewBag.Id_dostawcy = new SelectList(selectList, "Id", "FullName");
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
                AddPlacBuraczanyAsync(dostawy.Id_dostawy, dostawy.Ilosc_towaru, dostawy.Data_dostawy);
                return RedirectToAction(nameof(Index));
            }
            return View(dostawy);
        }
        private async Task AddPlacBuraczanyAsync(int idDostawy, int ilosc, DateTime data)
        {
            var plac = new Plac_buraczany
            {
                Id_dostawy = idDostawy,
                Ilosc_burakow = ilosc,
                Data_operacji = data
            };

            _context.Plac_buraczany.Add(plac);
            await _context.SaveChangesAsync();
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
            var dostawcy = await _context.Dostawcy.ToListAsync();
            var selectList = dostawcy.Select(k => new
            {
                Id = k.Id,
                FullName = k.Name + " " + k.Surname
            });
            ViewBag.Id_dostawcy = new SelectList(selectList, "Id", "FullName");
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
                    await UpdatePlacBuraczanyAsync(dostawy.Id_dostawy, dostawy.Ilosc_towaru, dostawy.Data_dostawy);
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

        private async Task UpdatePlacBuraczanyAsync(int idDostawy, int ilosc, DateTime data)
        {
            var plac = await _context.Plac_buraczany
                .FirstOrDefaultAsync(p => p.Id_dostawy == idDostawy);

            if (plac != null)
            {
                plac.Ilosc_burakow = ilosc;
                plac.Data_operacji = data;
                _context.Plac_buraczany.Update(plac);
                await _context.SaveChangesAsync();
            }
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
