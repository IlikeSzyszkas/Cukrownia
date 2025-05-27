using Projekt2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Kostrzewski_Wojciech_Gniazdowski_Maciej.Controllers
{
    public class UserController : Controller
    {
        // Tymczasowa "baza danych" użytkowników
        private static List<User> users = new()
        {
            new User { Username = "admin", PasswordHash = HashPassword("admin123"), Role = "admin" },
            new User { Username = "client", PasswordHash = HashPassword("client123"), Role = "client" },
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Login i hasło są wymagane.";
                return View();
            }

            var hashedPassword = HashPassword(password);
            var user = users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                // Użytkownik poprawnie zalogowany
                TempData["Message"] = $"Zalogowano jako {user.Username} ({user.Role})";
                // Możesz tu dodać logikę sesji / cookies
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Błędny login lub hasło.";
                return View();
            }
        }

        public static string HashPassword(string password)
        {
            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
}
