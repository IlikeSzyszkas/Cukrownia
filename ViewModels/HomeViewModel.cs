namespace Projekt2.ViewModels
{
    public class HomeViewModel
    {
        public int? Ilosc_Magazyn { get; set; }
        public int? Ilosc_Silos { get; set; }
        public int? Ilosc_Plac_Now { get; set; }
        public Dictionary<string, int>? Ilosc_Plac { get; set; }
        public Dictionary<string, int>? Ilosc_niewykorzystane_Plac { get; set; }
        public Dictionary<string, int>? Ilosc_wykorzystane_Plac { get; set; }
    }
}
