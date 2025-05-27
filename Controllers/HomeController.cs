using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var stan_w_magazynie = await _context.Pakownia
                .AsNoTracking()
                .ToListAsync();
            int ilosc_sprzedanych = await _context.Sprzedarz.SumAsync(m => m.Ilosc_opakowan);

            var stan_w_silosie = await _context.Silos
                .AsNoTracking()
                .ToListAsync();
            int ilosc_spakowanych = await _context.Silos_pakownia.SumAsync(m => m.Ilosc_cukru_pobrana);

            int buraki_na_placu = await _context.Plac_buraczany
                .Where(pb => pb.Data_operacji.Year == DateTime.Now.Year)
                .Join(_context.Plac_produktownia,
                      pb => pb.Id_dostawy,
                      pp => pp.Id_dostawy,
                      (pb, pp) => pb.Ilosc_burakow - pp.Ilosc_burakow_pobrana)
                .SumAsync();

            int ilosc_przerobionych = await _context.Plac_produktownia.SumAsync(m => m.Ilosc_burakow_pobrana);


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
                    .Sum(m => m.Ilosc_towaru_wyjscowego) - ilosc_sprzedanych,
                Ilosc_Silos = stan_w_silosie
                    .Sum(m => m.Ilosc_cukru) - ilosc_spakowanych,
                Ilosc_Plac = stan_w_plac
                    .ToDictionary(
                        x => x.Year.ToString(),
                        x => x.Stat
                    ),
                Ilosc_Plac_Now = buraki_na_placu,
                Ilosc_niewykorzystane_Plac = stan_niewyw_plac
                    .Where(x => x != null)
                    .ToDictionary(x => x.Year.ToString(), x => x.Stat),
                Ilosc_wykorzystane_Plac = stan_w_plac
                    .Where(x => x != null && stan_niewyw_plac.Any(n => n.Year == x.Year))
                    .ToDictionary(
                        x => x.Year.ToString(),
                        x => x.Stat - stan_niewyw_plac.FirstOrDefault(n => n.Year == x.Year)?.Stat ?? 0
                    )
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