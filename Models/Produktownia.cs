using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Produktownia
    {
        [Key]
        public int Id_partii { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wejscowego { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_towaru_wyjscowego { get; set; }
        [ForeignKey("pracownicy")]
        public int Id_kierownika_zmiany { get; set; }
        public Pracownicy? Kierownik_zmiany { get; set; }
        public DateTime Data_zmiany { get; set; }

    }
}
