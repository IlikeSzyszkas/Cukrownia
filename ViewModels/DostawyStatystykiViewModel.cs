using Projekt2.Models;

namespace Projekt2.ViewModels
{
    public class DostawyStatystykiViewModel
    {
        public int LiczbaDostaw { get; set; }
        public int SumaTowaru { get; set; }
        public double SredniaIloscTowaru { get; set; }
        public Dostawy? NajwiekszaDostawa { get; set; }
        public Dostawy? NajmniejszaDostawa { get; set; }
        public Dostawy? PierwszaDostawa { get; set; }
        public Dostawy? OstatniaDostawa { get; set; }

        public List<DostawcaSumaTowaru>? TopDostawcy { get; set; } = new();
    }

    public class DostawcaSumaTowaru
    {
        public Dostawcy? Dostawca { get; set; }
        public int SumaTowaru { get; set; }
    }
}
