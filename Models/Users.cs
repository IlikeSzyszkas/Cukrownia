using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt2.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int PracownikId { get; set; }
        [ForeignKey("PracownikId")]
        public virtual Pracownicy? Pracownik { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles? Role { get; set; }
    }
}
