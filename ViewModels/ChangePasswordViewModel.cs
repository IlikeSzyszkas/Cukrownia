using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt2.ViewModels
{
    public class ChangePasswordViewModel
    {
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string CurrentPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła muszą być zgodne")]
        public string ConfirmPassword { get; set; }
    }
}
