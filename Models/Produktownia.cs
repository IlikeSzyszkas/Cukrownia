using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Produktownia
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_partii { get; set; }
        [Display(Name = "Ilość towaru wejściowego [kg buraków]")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wejscowego { get; set; }
        [Display(Name = "Ilość towaru wyjściowego [kg cukru]")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wyjscowego { get; set; }
        [Display(Name = "Kierownik zmiany")]
        [ForeignKey("pracownicy")]
        public int Id_kierownika_zmiany { get; set; }
        [Display(Name = "Kierownik zmiany")]
        public Pracownicy? Kierownik_zmiany { get; set; }
        [Display(Name = "Data zmiany")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Data_zmiany { get; set; }

    }
}
