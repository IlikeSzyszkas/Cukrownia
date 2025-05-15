using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Pakownia
    {
        [Display(Name = "Id partii")]
        [Key]
        public int Id_partii { get; set; }
        [Display(Name = "Ilość towaru wejściowego")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wejscowego { get; set; }
        [Display(Name = "Ilość towaru wyjściowego")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wyjscowego { get; set; }
        [ForeignKey("pracownicy")]
        public int Id_kierownika_zmiany { get; set; }
        [Display(Name = "Kierownik zmiany")]
        public Pracownicy? Kierownik_zmiany { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Data zmiany")]
        public DateTime Data_zmiany { get; set; }
    }
}
