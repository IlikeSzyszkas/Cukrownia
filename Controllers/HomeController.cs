using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;
using Projekt2.ViewModels;

namespace Projekt2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stan_w_magazynie = await _context.Magazyn_sprzedarz
                .Include(m => m.Operacja)
                .AsNoTracking()
                .ToListAsync();

            var stan_w_silosie = await _context.Silos_pakownia
                .Include(m => m.Operacja)
                .AsNoTracking()
                .ToListAsync();

            var stan_w_plac = await _context.Plac_buraczany
                .GroupBy(d => d.Data_operacji.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Stat = g.Sum(m => m.Ilosc_burakow)
                })
                .OrderByDescending(x => x.Year)
                .AsNoTracking()
                .ToListAsync();

            var stan_niewyw_plac = await _context.Plac_produktownia
                .Include(m => m.Dostawa)
                .Where(m => m.Dostawa != null)
                .GroupBy(d => d.Data_pobrania.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Stat = g.Sum(m => m.Dostawa.Ilosc_towaru) - g.Sum(m => m.Ilosc_burakow_pobrana)
                })
                .OrderByDescending(x => x.Year)
                .AsNoTracking()
                .ToListAsync();


            var model = new HomeViewModel
            {
                Ilosc_Magazyn = stan_w_magazynie
                    .Where(m => m.Operacja != null)
                    .Sum(m => m.Operacja.Ilosc_opakowan)
                    - stan_w_magazynie.Sum(m => m.Ilosc_opakowan_sprzedanych),
                Ilosc_Silos = stan_w_silosie
                    .Where(m => m.Operacja != null)
                    .Sum(m => m.Operacja.Ilosc_cukru)
                    - stan_w_silosie.Sum(m => m.Ilosc_cukru_pobrana),
                Ilosc_Plac = stan_w_plac.ToDictionary(
                    x => x.Year.ToString(),
                    x => x.Stat
                ),
                Ilosc_niewykorzystane_Plac = stan_niewyw_plac
                    .Where(x => x != null)
                    .ToDictionary(x => x.Year.ToString(), x => x.Stat)
            };



            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}