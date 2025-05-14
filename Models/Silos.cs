using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Silos
    {
        public int Id { get; set; }
        [ForeignKey("produktownia")]
        public int Id_operacji { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        public int Ilosc_cukru { get; set; }
        public DateTime Data_skladowania { get; set; }
    }
}
