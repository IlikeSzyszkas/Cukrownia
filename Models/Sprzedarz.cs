using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Sprzedarz
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_transakcji { get; set; }
        [Display(Name = "Ilość opakowań sprzedanych")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_opakowan { get; set; }
        [Display(Name = "Data odbioru transakcji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Data_odbioru { get; set; }
        [Display(Name = "Kupiec")]
        [ForeignKey("kupcy")]
        public int Id_kupca { get; set; }
        public virtual Kupcy? Kupiec { get; set; }
    }
}
