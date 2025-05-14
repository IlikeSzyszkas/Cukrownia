using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Plac_produktownia
    {
        public int Id { get; set; }
        [ForeignKey("plac_buraczany")]
        public int Id_dostawy { get; set; }
        [ForeignKey("produktownia")]
        public int Id_partii { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_burakow_pobrana { get; set; }
        public DateTime Data_pobrania { get; set; }
    }
}
