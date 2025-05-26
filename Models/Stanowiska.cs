using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Stanowiska
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_stanowiska { get; set; }
        public string Nazwa { get; set; }
        public virtual List<Pracownicy>? Pracownicy { get; set; } = new List<Pracownicy>();
        [Display(Name = "Liczba pracowników")]
        public int? LiczbaPracownikow { get; set; }
    }
}
