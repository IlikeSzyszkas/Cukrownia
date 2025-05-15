using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Dostawy
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_dostawy { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        [Display(Name = "Ilość towaru")]
        public int Ilosc_towaru { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Data dostawy")]
        public DateTime Data_dostawy { get; set; }
        [ForeignKey("dostawcy")]
        public int Id_dostawcy { get; set; }
        [Display(Name = "Dostawca")]
        public Dostawcy? Dostawca { get; set; }
    }
}
