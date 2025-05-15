using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Pakownia
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_partii { get; set; }
        [Display(Name = "Ilość towaru wejściowego [kg cukru]")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wejscowego { get; set; }
        [Display(Name = "Ilość towaru wyjściowego [opakowania po 1kg]")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wyjscowego { get; set; }
        [Display(Name = "Kierownik zmiany")]
        [ForeignKey("pracownicy")]
        public int Id_kierownika_zmiany { get; set; }
        [Display(Name = "Kierownik zmiany")]
        public Pracownicy? Kierownik_zmiany { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Data zmiany")]
        public DateTime Data_zmiany { get; set; }
    }
}
