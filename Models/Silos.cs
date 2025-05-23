using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Silos
    {
        public int Id { get; set; }
        [Display(Name = "Id operacji")]
        [ForeignKey(nameof(Produktownia))]
        public int Id_operacji { get; set; }
        public virtual Produktownia Produktownia { get; set; }
        [Display(Name = "Ilość cukru")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_cukru { get; set; }
        [Display(Name = "Data składowania")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Data_skladowania { get; set; }
    }
}
