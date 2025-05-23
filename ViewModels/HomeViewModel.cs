namespace Projekt2.ViewModels
{
    public class HomeViewModel
    {
        public int Ilosc_Magazyn { get; set; }
        public int Ilosc_Silos { get; set; }
        public Dictionary<string,int> Ilosc_Plac { get; set; }
        public Dictionary<string,int> Ilosc_niewykorzystane_Plac { get; set; }
    }
}
