using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Plac_produktownia
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id dostawy z placu buraczanego")]
        [ForeignKey(nameof(Dostawa))]
        public int Id_dostawy { get; set; }
        public virtual Dostawy? Dostawa { get; set; }

        [Display(Name = "Id partii w produktowni")]
        [ForeignKey(nameof(Partia))]
        public int Id_partii { get; set; }
        public virtual Produktownia? Partia { get; set; }
        [Display(Name = "Ilość pobranych buraków [kg]")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_burakow_pobrana { get; set; }

        [Display(Name = "Data pobrania")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Data_pobrania { get; set; }
    }

}
