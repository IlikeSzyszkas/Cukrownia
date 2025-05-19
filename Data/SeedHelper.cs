using Microsoft.EntityFrameworkCore;
using Projekt2.Models;
using System;
using System.Linq;
using Projekt2.Data;

public static class SeedHelper
{
    public static void UzupelnijReferencje(ApplicationDbContext context)
    {
        // Produktownia – Kierownik_zmiany
        var produktownie = context.Produktownia
            .Where(p => p.Kierownik_zmiany == null)
            .ToList();

        foreach (var p in produktownie)
        {
            p.Kierownik_zmiany = context.Pracownicy.FirstOrDefault(pr => pr.Id == p.Id_kierownika_zmiany);
        }

        // Pakownia – Kierownik_zmiany
        var pakownie = context.Pakownia
            .Where(p => p.Kierownik_zmiany == null)
            .ToList();

        foreach (var p in pakownie)
        {
            p.Kierownik_zmiany = context.Pracownicy.FirstOrDefault(pr => pr.Id == p.Id_kierownika_zmiany);
        }

        // Sprzedarz – Kupiec
        var sprzedarze = context.Sprzedarz
            .Where(s => s.Kupiec == null)
            .ToList();

        foreach (var s in sprzedarze)
        {
            s.Kupiec = context.Kupcy.FirstOrDefault(k => k.Id_kupca == s.Id_kupca);
        }

        // Magazyn_sprzedarz – Operacja, Transakcja
        var magSprzed = context.Magazyn_sprzedarz
            .Where(m => m.Operacja == null || m.Transakcja == null)
            .ToList();

        foreach (var m in magSprzed)
        {
            m.Operacja = context.Magazyn.FirstOrDefault(o => o.Id == m.Id_operacji);
            m.Transakcja = context.Sprzedarz.FirstOrDefault(t => t.Id_transakcji == m.Id_transakcji);
        }

        // Pracownicy – Dzial, Stanowisko
        var pracownicy = context.Pracownicy
            .Where(p => p.Dzial == null || p.Stanowisko == null)
            .ToList();

        foreach (var p in pracownicy)
        {
            p.Dzial = context.Dzialy.FirstOrDefault(d => d.Id_dzialu == p.Id_dzialu);
            p.Stanowisko = context.Stanowiska.FirstOrDefault(s => s.Id_stanowiska == p.Id_stanowiska);
        }

        // Dostawy – Dostawca
        var dostawy = context.Dostawy
            .Where(d => d.Dostawca == null)
            .ToList();

        foreach (var d in dostawy)
        {
            d.Dostawca = context.Dostawcy.FirstOrDefault(ds => ds.Id == d.Id_dostawcy);
        }

        context.SaveChanges();
        //Console.WriteLine("✅ Wszystkie referencje zostały uzupełnione.");
    }
}
