using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Pracownicy
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Adres")]
        public string Addres { get; set; }
        [Display(Name = "Numer tel.")]
        [Phone]
        public string Nr_tel { get; set; }
        [ForeignKey("stanowiska")]
        public int Id_stanowiska { get; set; }
        public Stanowiska? Stanowisko { get; set; }
        [ForeignKey("dzialy")]
        public int Id_dzialu { get; set; }
        [Display(Name = "Dział")]
        public Dzialy? Dzial { get; set; }
        public List<Produktownia>? Zmiany_prod { get; set; } = new List<Produktownia>();
        [Display(Name = "Liczba odbytych zmian")]
        public int? LiczbaZmian_prod { get; set; }
        public List<Pakownia>? Zmiany_pak { get; set; } = new List<Pakownia>();
        [Display(Name = "Liczba odbytych zmian")]
        public int? LiczbaZmian_pak { get; set; }
    }
}
