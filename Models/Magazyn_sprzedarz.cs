using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Magazyn_sprzedarz
    {
        public int Id { get; set; }
        [ForeignKey("magazyn")]
        public int Id_operacji { get; set; }
        public virtual Magazyn? Operacja { get; set; }
        [ForeignKey("sprzedarz")]
        public int Id_transakcji { get; set; }
        public virtual Sprzedarz? Transakcja { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_opakowan_sprzedanych { get; set; }
    }
}
