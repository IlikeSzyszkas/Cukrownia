using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Stanowiska
    {
        [Key]
        public int Id_stanowiska { get; set; }
        public string Nazwa {  get; set; }
        public List<Pracownicy>? Pracownicy { get; set; } = new List<Pracownicy>();
    }
}
