using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;
using Projekt2.ViewModels;

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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 50;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)_context.Dostawy.Count() / pageSize);

            return View(await _context.Dostawy
                .Include(d => d.Dostawca)
                .OrderBy(m => m.Data_dostawy)
                .ThenBy(u => u.Id_dostawy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync());
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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id_dostawy == id);
            if (dostawy == null)
            {
                return NotFound();
            }

            return View(dostawy);
        }
        // GET: Dostawy/Statystyki
        public async Task<IActionResult> Statystyki()
        {
            var wszystkieDostawy = await _context.Dostawy
                .Include(d => d.Dostawca)
                .ToListAsync();

            if (!wszystkieDostawy.Any())
            {
                return View(new DostawyStatystykiViewModel());
            }

            var model = new DostawyStatystykiViewModel
            {
                LiczbaDostaw = wszystkieDostawy.Count,
                SumaTowaru = wszystkieDostawy.Sum(d => d.Ilosc_towaru),
                SredniaIloscTowaru = wszystkieDostawy.Average(d => d.Ilosc_towaru),
                NajwiekszaDostawa = _context.Dostawy.OrderByDescending(d => d.Ilosc_towaru).Take(1).FirstOrDefault(),
                NajmniejszaDostawa = _context.Dostawy.OrderBy(d => d.Ilosc_towaru).Take(1).FirstOrDefault(),
                PierwszaDostawa = _context.Dostawy.OrderByDescending(d => d.Data_dostawy).Take(1).FirstOrDefault(),
                OstatniaDostawa = _context.Dostawy.OrderBy(d => d.Data_dostawy).Take(1).FirstOrDefault(),
                TopDostawcy = wszystkieDostawy
                    .GroupBy(d => d.Dostawca)
                    .Select(g => new DostawcaSumaTowaru
                    {
                        Dostawca = g.Key!,
                        SumaTowaru = g.Sum(x => x.Ilosc_towaru)
                    })
                    .OrderByDescending(x => x.SumaTowaru)
                    .Take(5)
                    .ToList()
            };

            var chartData1 = await _context.Dostawy
                .GroupBy(d => new { d.Data_dostawy.Year, d.Data_dostawy.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(x => x.Ilosc_towaru)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .AsNoTracking()
                .ToListAsync(); 

            var formattedChartData1 = chartData1
                .Select(x => new
                {
                    Label = $"{x.Year}-{x.Month:D2}",
                    x.Total
                })
                .ToList();

            ViewBag.ChartLabels1 = formattedChartData1.Select(x => x.Label).ToArray();
            ViewBag.ChartValues1 = formattedChartData1.Select(x => x.Total).ToArray();



            var chartData2 = await _context.Dostawcy
                .Include(d => d.Dostawy)
                .Where(d => d.Ilosc_ha_pola > 0 && d.Dostawy.Any())
                .Select(d => new
                {
                    Name = d.Name + " " + d.Surname,
                    Efficiency = (double)d.Dostawy.Sum(x => x.Ilosc_towaru) / d.Ilosc_ha_pola
                })
                .OrderByDescending(d => d.Efficiency)
                .Take(10)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.ChartLabels2 = chartData2.Select(x => x.Name).ToArray();
            ViewBag.ChartValues2 = chartData2.Select(x => x.Efficiency).ToArray();

            return View(model);
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
