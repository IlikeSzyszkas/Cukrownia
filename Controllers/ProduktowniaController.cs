using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;
using Projekt2.ViewModels;

namespace Projekt2.Controllers
{
    public class ProduktowniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktowniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produktownia
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 50;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)_context.Produktownia.Count() / pageSize);

            return View(await _context.Produktownia
                .Include(m => m.Kierownik_zmiany)
                .OrderBy(m => m.Data_zmiany)
                .ThenBy(m => m.Id_partii)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Produktownia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktownia = await _context.Produktownia
                .Include(m => m.Kierownik_zmiany)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (produktownia == null)
            {
                return NotFound();
            }

            return View(produktownia);
        }
        // GET: Produktownia/Statystyki
        public async Task<IActionResult> Statystyki()
        {

            var chartData = await _context.Produktownia
                .GroupBy(d => new { d.Data_zmiany.Year, d.Data_zmiany.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Stat = g.Sum(x => x.Ilosc_towaru_wejscowego) == 0
                        ? 0
                        : (double)g.Sum(x => x.Ilosc_towaru_wyjscowego) / g.Sum(x => x.Ilosc_towaru_wejscowego) * 100
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .AsNoTracking()
                .ToListAsync();

            // Prepare month labels in Polish
            var culture = new CultureInfo("pl-PL");
            var monthLabels = Enumerable.Range(9, 4)
                .Select(m => new DateTime(2000, m, 1).ToString("MMMM", culture))
                .ToArray();


            // Prepare datasets grouped by year
            var datasets = chartData
                .GroupBy(x => x.Year)
                .Select(g => new ProduktowniaStatystykiViewModel
                {
                    Year = g.Key,
                    Data = Enumerable.Range(9, 4)
                        .Select(month =>
                        {
                            var match = g.FirstOrDefault(x => x.Month == month);
                            return match != null ? (double?)match.Stat : null;
                        })
                        .ToArray()
                })
                .ToList();

            var viewModel = new ProduktowniaStatystykiPageViewModel
            {
                MonthLabels = monthLabels,
                Datasets = datasets
            };

            return View(viewModel);
        }

        // GET: Produktownia/Create
        public async Task<IActionResult> CreateAsync()
        {
            var kierownicy = await _context.Pracownicy
                .Include(p => p.Stanowisko)
                .Where(p => p.Stanowisko.Nazwa == "Kierownik")
                .Where(p => p.Dzial.Nazwa == "Produktownia")
                .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                k.Id,
                FullName = k.Name + " " + k.Surname
            });
            ViewBag.Id_kierownika_zmiany = new SelectList(selectList, "Id", "FullName");
            return View();
        }

        // POST: Produktownia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Produktownia produktownia)
        {
            if (ModelState.IsValid)
            {
                var kierownik = await _context.Pracownicy.FindAsync(produktownia.Id_kierownika_zmiany);
                produktownia.Kierownik_zmiany = kierownik;
                _context.Add(produktownia);
                await _context.SaveChangesAsync();
                await AddPlacProduktowniaAsync(produktownia.Id_partii, produktownia.Ilosc_towaru_wejscowego, produktownia.Data_zmiany);
                await AddSilosAsync(produktownia.Id_partii, produktownia.Ilosc_towaru_wyjscowego, produktownia.Data_zmiany);
                return RedirectToAction(nameof(Index));
            }
            return View(produktownia);
        }
        private async Task AddPlacProduktowniaAsync(int id_partii, int ilosc, DateTime data)
        {
            var dostawa = await _context.Plac_buraczany
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();

            if (dostawa == null)
            {
                throw new Exception("Brak dostępnych operacji magazynowych.");
            }
            var plac = new Plac_produktownia
            {
                Id_dostawy = dostawa.Id_dostawy,
                Id_partii = id_partii,
                Ilosc_burakow_pobrana = ilosc,
                Data_pobrania = data
            };

            _context.Plac_produktownia.Add(plac);
            await _context.SaveChangesAsync();
        }
        private async Task AddSilosAsync(int id_operacji, int ilosc, DateTime data)
        {
            var silos = new Silos
            {
                Id_operacji = id_operacji,
                Ilosc_cukru = ilosc,
                Data_skladowania = data
            };

            _context.Silos.Add(silos);
            await _context.SaveChangesAsync();
        }

        // GET: Produktownia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktownia = await _context.Produktownia.FindAsync(id);
            if (produktownia == null)
            {
                return NotFound();
            }

            var kierownicy = await _context.Pracownicy
                .Include(p => p.Stanowisko)
                .Where(p => p.Stanowisko.Nazwa == "Kierownik")
                .Where(p => p.Dzial.Nazwa == "Produktownia")
                .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                Id = k.Id,
                FullName = k.Name + " " + k.Surname
            });
            ViewBag.Id_kierownika_zmiany = new SelectList(selectList, "Id", "FullName");
            return View(produktownia);
        }

        // POST: Produktownia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Produktownia produktownia)
        {
            if (id != produktownia.Id_partii)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var kierownik = await _context.Pracownicy.FindAsync(produktownia.Id_kierownika_zmiany);
                    produktownia.Kierownik_zmiany = kierownik;
                    _context.Update(produktownia);
                    await _context.SaveChangesAsync();
                    await UpdateSilosAsync(produktownia.Id_partii, produktownia.Ilosc_towaru_wyjscowego, produktownia.Data_zmiany);
                    await UpdatePlacProduktowniaAsync(produktownia.Id_partii, produktownia.Id_partii, produktownia.Ilosc_towaru_wejscowego, produktownia.Data_zmiany);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktowniaExists(produktownia.Id_partii))
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
            return View(produktownia);
        }
        private async Task UpdatePlacProduktowniaAsync(int id_partii, int id_dostawy, int ilosc, DateTime data)
        {
            var plac = await _context.Plac_produktownia
                .FirstOrDefaultAsync(p => p.Id_partii == id_partii);

            if (plac != null)
            {
                plac.Id_dostawy = id_dostawy;
                plac.Id_partii = id_partii;
                plac.Ilosc_burakow_pobrana = ilosc;
                plac.Data_pobrania = data;
                _context.Plac_produktownia.Update(plac);
                await _context.SaveChangesAsync();
            }
        }
        private async Task UpdateSilosAsync(int id_operacji, int ilosc, DateTime data)
        {
            var silos = await _context.Silos
                .FirstOrDefaultAsync(p => p.Id_operacji == id_operacji);

            if (silos != null)
            {
                silos.Id_operacji = id_operacji;
                silos.Ilosc_cukru = ilosc;
                silos.Data_skladowania = data;
                _context.Silos.Update(silos);
                await _context.SaveChangesAsync();
            }
        }

        // GET: Produktownia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktownia = await _context.Produktownia
                .Include(m => m.Kierownik_zmiany)
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (produktownia == null)
            {
                return NotFound();
            }

            return View(produktownia);
        }

        // POST: Produktownia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produktownia = await _context.Produktownia.FindAsync(id);
            if (produktownia != null)
            {
                _context.Produktownia.Remove(produktownia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktowniaExists(int id)
        {
            return _context.Produktownia.Any(e => e.Id_partii == id);
        }
    }
}
