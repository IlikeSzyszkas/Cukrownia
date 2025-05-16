using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public List<Pracownicy>? Kierownicy { get; set; } = new List<Pracownicy>();
    }
}
