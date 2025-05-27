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

            if (!await _context.Dostawy.AnyAsync())
            {
                return View(new DostawyStatystykiViewModel());
            }

            var dostawy = await _context.Dostawy.AsNoTracking().ToListAsync();


            var model = new DostawyStatystykiViewModel
            {
                LiczbaDostaw = await _context.Dostawy.CountAsync(),
                SumaTowaru = await _context.Dostawy.SumAsync(d => d.Ilosc_towaru),
                SredniaIloscTowaru = await _context.Dostawy.AverageAsync(d => d.Ilosc_towaru),
                NajwiekszaDostawa = dostawy.OrderByDescending(d => d.Ilosc_towaru).Take(1).FirstOrDefault(),
                NajmniejszaDostawa = dostawy.OrderBy(d => d.Ilosc_towaru).Take(1).FirstOrDefault(),
                PierwszaDostawa = dostawy.OrderByDescending(d => d.Data_dostawy).Take(1).FirstOrDefault(),
                OstatniaDostawa = dostawy.OrderBy(d => d.Data_dostawy).Take(1).FirstOrDefault(),
                TopDostawcy = await _context.Dostawy
                    .GroupBy(d => new { d.Id_dostawcy, d.Dostawca.Name, d.Dostawca.Surname })
                    .Select(g => new DostawcaSumaTowaru
                    {
                        Dostawca = new Dostawcy { Id = g.Key.Id_dostawcy, Name = g.Key.Name, Surname = g.Key.Surname },
                        SumaTowaru = g.Sum(x => x.Ilosc_towaru)
                    })
                    .OrderByDescending(x => x.SumaTowaru)
                    .Take(5)
                    .AsNoTracking()
                    .ToListAsync()
            };

            var chartData1 = await _context.Dostawy
                .GroupBy(d => new { d.Data_dostawy.Year, d.Data_dostawy.Month })
                .Select(g => new
                {
                    Label = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Total = g.Sum(x => x.Ilosc_towaru)
                })
                .OrderBy(x => x.Label)
                .AsNoTracking()
                .ToListAsync();


            ViewBag.ChartLabels1 = chartData1.Select(x => x.Label).ToArray();
            ViewBag.ChartValues1 = chartData1.Select(x => x.Total).ToArray();



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

        private async Task<SelectList> GetDostawcySelectListAsync()
        {
            var dostawcy = await _context.Dostawcy
                .Select(d => new { d.Id, FullName = d.Name + " " + d.Surname })
                .ToListAsync();
            return new SelectList(dostawcy, "Id", "FullName");
        }


        // GET: Dostawy/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Id_dostawcy = GetDostawcySelectListAsync();
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
                await AddPlacBuraczanyAsync(dostawy.Id_dostawy, dostawy.Ilosc_towaru, dostawy.Data_dostawy);
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

            ViewBag.Id_dostawcy = GetDostawcySelectListAsync();
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
