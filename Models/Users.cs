using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string Username { get; set; }
        [Display(Name = "Hasło")]
        public string PasswordHash { get; set; }
        [Display(Name = "Pracownik")]
        public int PracownikId { get; set; }
        [ForeignKey("PracownikId")]
        public virtual Pracownicy? Pracownik { get; set; }
        [Display(Name = "Rola")]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles? Role { get; set; }
    }
}
