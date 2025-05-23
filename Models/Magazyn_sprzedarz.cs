using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Magazyn_sprzedarz
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id operacji magazynowej")]
        [ForeignKey(nameof(Operacja))]
        public int Id_operacji { get; set; }

        [Display(Name = "Operacja magazynowa")]
        public virtual Magazyn? Operacja { get; set; }

        [Display(Name = "Id transakcji sprzedaży")]
        [ForeignKey(nameof(Transakcja))]
        public int Id_transakcji { get; set; }

        [Display(Name = "Transakcja sprzedaży")]
        public virtual Sprzedarz? Transakcja { get; set; }

        [Display(Name = "Ilość sprzedanych opakowań")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_opakowan_sprzedanych { get; set; }
    }

}
