using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Silos_pakownia
    {
        public int Id { get; set; }
        [ForeignKey("silos")]
        public int Id_operacji { get; set; }
        [ForeignKey("pakownia")]
        public int Id_partii { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_cukru_pobrana { get; set; }
        public DateTime Data_pobrania { get; set; }
    }
}
