using Projekt2.Models;

namespace Projekt2.ViewModels
{
    public class DostawcyStatystykiViewModel
    {
        public Dostawcy BestDostawca { get; set; }
        public Dostawcy BestPole { get; set; }
        public Dostawcy WorstDostawca { get; internal set; }
    }
}
