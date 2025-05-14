using Microsoft.EntityFrameworkCore;
using Projekt2.Models;

namespace Projekt2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Dostawy> Dostawy { get; set; }
        public DbSet<Dzialy> Dzialy { get; set; }
        public DbSet<Kupcy> Kupcy { get; set; }
        public DbSet<Magazyn> Magazyn { get; set; }
        public DbSet<Magazyn_sprzedarz> Magazyn_sprzedarz { get; set; }
        public DbSet<Pakownia> Pakownia { get; set; }
        public DbSet<Pracownicy> Pracownicy { get; set; }
        public DbSet<Dostawcy> Dostawcy { get; set; }
        public DbSet<Plac_buraczany> Plac_buraczany { get; set; }
        public DbSet<Plac_produktownia> Plac_produktownia { get; set; }
        public DbSet<Produktownia> Produktownia { get; set; }
        public DbSet<Silos> Silos { get; set; }
        public DbSet<Silos_pakownia> Silos_pakownia { get; set; }
        public DbSet<Sprzedarz> Sprzedarz { get; set; }
        public DbSet<Stanowiska> Stanowiska { get; set; }
    }
}
