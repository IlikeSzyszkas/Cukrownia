using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Dostawcy
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Adres")]
        public string Addres { get; set; }
        [Phone(ErrorMessage = "zły nr. telefonu")]
        [Display(Name = "Numer tel.")]
        public string Nr_tel { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa lub równa 1.")]
        [Display(Name = "Ilość ha pola")]
        public int Ilosc_ha_pola { get; set; }
        public virtual List<Dostawy>? Dostawy { get; set; } = new List<Dostawy>();
        [Display(Name = "Liczba dostaw")]
        public int? LiczbaDostaw { get; set; }

    }
}
