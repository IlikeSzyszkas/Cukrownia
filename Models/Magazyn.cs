using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Magazyn
    {
        public int Id { get; set; }
        [ForeignKey("pakownia")]
        public int Id_operacji { get; set; }
        [Display(Name = "Ilość opakowań")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_opakowan { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Data operacji")]
        public DateTime Data_operacji { get; set; }
    }
}
