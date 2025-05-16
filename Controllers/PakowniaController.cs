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
    public class PakowniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PakowniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pakownia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pakownia.Include(m => m.Kierownik_zmiany).ToListAsync());
        }

        // GET: Pakownia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pakownia = await _context.Pakownia
                .Include(m => m.Kierownik_zmiany)
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (pakownia == null)
            {
                return NotFound();
            }

            return View(pakownia);
        }

        // GET: Pakownia/Create
        public async Task<IActionResult> Create()
        {
            var kierownicy = await _context.Pracownicy
                .Include(p => p.Stanowisko)
                .Where(p => p.Stanowisko.Nazwa == "Kierownik")
                .Where(p => p.Dzial.Nazwa == "Pakownia")
                .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                Id = k.Id,
                FullName = k.Name + " " + k.Surname
            });
            ViewBag.Id_kierownika_zmiany = new SelectList(selectList, "Id", "FullName");
            return View();
        }

        // POST: Pakownia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Pakownia pakownia)
        {
            if (ModelState.IsValid)
            {
                var kierownik = await _context.Pracownicy.FindAsync(pakownia.Id_kierownika_zmiany);
                pakownia.Kierownik_zmiany = kierownik;
                _context.Add(pakownia);
                await _context.SaveChangesAsync();
                await AddSilosPakowniaAsync(pakownia.Id_partii, pakownia.Id_partii, pakownia.Ilosc_towaru_wejscowego, pakownia.Data_zmiany);
                await AddMagazynAsync(pakownia.Id_partii, pakownia.Ilosc_towaru_wyjscowego, pakownia.Data_zmiany);
                return RedirectToAction(nameof(Index));
            }
            return View(pakownia);
        }
        private async Task AddSilosPakowniaAsync(int id_opercji, int id_partii, int ilosc, DateTime data)
        {
            var operacja = await _context.Silos
                 .OrderByDescending(m => m.Id)
                 .FirstOrDefaultAsync();

            if (operacja == null)
            {
                throw new Exception("Brak dostępnych operacji magazynowych.");
            }

            var silos = new Silos_pakownia
            {
                Id_operacji = operacja.Id,
                Id_partii = id_partii,
                Ilosc_cukru_pobrana = ilosc,
                Data_pobrania = data
            };

            _context.Silos_pakownia.Add(silos);
            await _context.SaveChangesAsync();
        }
        private async Task AddMagazynAsync(int id_operacji, int ilosc, DateTime data)
        {
            var magazyn = new Magazyn
            {
                Id_operacji = id_operacji,
                Ilosc_opakowan = ilosc,
                Data_operacji = data
            };

            _context.Magazyn.Add(magazyn);
            await _context.SaveChangesAsync();
        }

        // GET: Pakownia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pakownia = await _context.Pakownia.FindAsync(id);
            if (pakownia == null)
            {
                return NotFound();
            }
            var kierownicy = await _context.Pracownicy
                .Include(p => p.Stanowisko)
                .Where(p => p.Stanowisko.Nazwa == "Kierownik")
                .Where(p => p.Dzial.Nazwa == "Pakownia")
                .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                Id = k.Id,
                FullName = k.Name + " " + k.Surname
            });
            ViewBag.Id_kierownika_zmiany = new SelectList(selectList, "Id", "FullName");
            return View(pakownia);
        }

        // POST: Pakownia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_partii,Ilosc_towaru_wejscowego,Ilosc_towaru_wyjscowego,Id_kierownika_zmiany,Data_zmiany")] Pakownia pakownia)
        {
            if (id != pakownia.Id_partii)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var kierownik = await _context.Pracownicy.FindAsync(pakownia.Id_kierownika_zmiany);
                    pakownia.Kierownik_zmiany = kierownik;
                    _context.Update(pakownia);
                    await _context.SaveChangesAsync();
                    await UpdateSilosPakowniaAsync(pakownia.Id_partii, pakownia.Id_partii, pakownia.Ilosc_towaru_wejscowego, pakownia.Data_zmiany);
                    await UpdateMagazynAsync(pakownia.Id_partii, pakownia.Ilosc_towaru_wyjscowego, pakownia.Data_zmiany);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PakowniaExists(pakownia.Id_partii))
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
            return View(pakownia);
        }
        private async Task UpdateSilosPakowniaAsync(int id_opercji, int id_partii, int ilosc, DateTime data)
        {
            var silos = await _context.Silos_pakownia
                .FirstOrDefaultAsync(p => p.Id_operacji == id_opercji);

            if (silos != null)
            {
                silos.Id_operacji = id_opercji;
                silos.Id_partii = id_partii;
                silos.Ilosc_cukru_pobrana = ilosc;
                silos.Data_pobrania = data;
                _context.Silos_pakownia.Update(silos);
                await _context.SaveChangesAsync();
            }
        }
        private async Task UpdateMagazynAsync(int id_operacji, int ilosc, DateTime data)
        {
            var magazyn = await _context.Magazyn
                .FirstOrDefaultAsync(p => p.Id_operacji == id_operacji);

            if (magazyn != null)
            {
                magazyn.Id_operacji = id_operacji;
                magazyn.Ilosc_opakowan = ilosc;
                magazyn.Data_operacji = data;
                _context.Magazyn.Update(magazyn);
                await _context.SaveChangesAsync();
            }
        }

        // GET: Pakownia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pakownia = await _context.Pakownia
                .Include(m => m.Kierownik_zmiany)
                .FirstOrDefaultAsync(m => m.Id_partii == id);
            if (pakownia == null)
            {
                return NotFound();
            }

            return View(pakownia);
        }

        // POST: Pakownia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pakownia = await _context.Pakownia.FindAsync(id);
            if (pakownia != null)
            {
                _context.Pakownia.Remove(pakownia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PakowniaExists(int id)
        {
            return _context.Pakownia.Any(e => e.Id_partii == id);
        }
    }
}
