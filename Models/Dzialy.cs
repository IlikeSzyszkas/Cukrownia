using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{

    public class Dzialy
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_dzialu { get; set; }
        public string Nazwa { get; set; }
        public List<Pracownicy>? Pracownicy { get; set; } = new List<Pracownicy>();
        [Display(Name = "Liczba pracowników")]
        public int? LiczbaPracownikow { get; set; }
    }
}
