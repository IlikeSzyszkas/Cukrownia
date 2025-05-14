using System.ComponentModel.DataAnnotations;

namespace Projekt2.Models
{
    public class Kupcy
    {
        [Display(Name = "Id")]
        [Key]
        public int Id_kupca { get; set; }
        public string Nazwa {  get; set; }
        [Display(Name = "Numer NIP")]
        [RegularExpression(@"^\d{10}$|^\d{3}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Wprowadź poprawny numer NIP.")]
        public string Nip {  get; set; }
        public string Adres {  get; set; }
        [Display(Name = "Numer tel.")]
        [Phone]
        public string Nr_tel {  get; set; }
        public List<Sprzedarz>? Transakcje { get; set; } = new List<Sprzedarz>();

    }
}
