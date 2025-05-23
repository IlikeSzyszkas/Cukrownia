using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Plac_buraczany
    {
        public int Id { get; set; }
        [Display(Name = "Id dostawy")]
        [ForeignKey(nameof(Dostawa))]
        public int Id_dostawy { get; set; }
        public virtual Dostawy? Dostawa { get; set; }
        [Display(Name = "Ilość buraków")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_burakow {  get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Data operacji")]
        public DateTime Data_operacji { get; set; }
    }
}
