using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Silos_pakownia
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id operacji w silosie")]
        [ForeignKey("silos")]
        public int Id_operacji { get; set; }

        [Display(Name = "Id partii z pakowni")]
        [ForeignKey("pakownia")]
        public int Id_partii { get; set; }

        [Display(Name = "Ilość pobranego cukru [kg]")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_cukru_pobrana { get; set; }

        [Display(Name = "Data pobrania")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Data_pobrania { get; set; }
    }

}
