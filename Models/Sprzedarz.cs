using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Sprzedarz
    {
        [Key]
        public int Id_transakcji { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_opakowan { get; set; }
        public DateTime Data_odbioru { get; set; }
        [ForeignKey("kupcy")]
        public int Id_kupca { get; set; }
        public Kupcy? Kupiec { get; set; }
    }
}
