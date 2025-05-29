using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt2.Data;
using Projekt2.Models;

namespace Projekt2.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public static string HashPassword(string password)
        {
            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(hashBytes).ToLower();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Login i hasło są wymagane.";
                return View();
            }

            var hashedPassword = HashPassword(password);
            var user = await _context.Users
                   .Include(u => u.Role)
                   .Include(p => p.Pracownik)
                   .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                // Użytkownik poprawnie zalogowany
                //TempData["Message"] = $"Zalogowano jako {user.Username} ({user.Role})";
                // Możesz tu dodać logikę sesji / cookies

                var claims = new List<Claim>
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim("Name", user.Pracownik.Name),
                        new Claim("Surname", user.Pracownik.Surname),
                        new Claim(ClaimTypes.Role, user.Role.Name)
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Błędny login lub hasło.";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users.Include(u => u.Pracownik).Include(u => u.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Pracownik)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: User/Create
        public async Task<IActionResult> CreateAsync()
        {

            var kierownicy = await _context.Pracownicy
                .Include(p => p.Stanowisko)
                .Where(p => p.Stanowisko.Nazwa == "Kierownik")
                .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                k.Id,
                FullName = k.Name + " " + k.Surname
            });


            ViewData["PracownikId"] = new SelectList(selectList, "Id", "FullName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,PasswordHash,PracownikId,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.PasswordHash = HashPassword(users.PasswordHash);
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PracownikId"] = new SelectList(_context.Pracownicy, "Id", "Id", users.PracownikId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", users.RoleId);
            return View(users);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            var kierownicy = await _context.Pracownicy
               .Include(p => p.Stanowisko)
               .Where(p => p.Stanowisko.Nazwa == "Kierownik")
               .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                k.Id,
                FullName = k.Name + " " + k.Surname
            });

            ViewData["PracownikId"] = new SelectList(selectList, "Id", "FullName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View(users);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,PasswordHash,PracownikId,RoleId")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var kierownicy = await _context.Pracownicy
                           .Include(p => p.Stanowisko)
                           .Where(p => p.Stanowisko.Nazwa == "Kierownik")
                           .ToListAsync();

            var selectList = kierownicy.Select(k => new
            {
                Id = k.Id,
                FullName = k.Name + " " + k.Surname
            });

            ViewData["PracownikId"] = new SelectList(selectList, "Id", "FullName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View(users);
        }

        //Get: User/ChangePassword/5
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        //Post: User/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, string newPassword, string repeatedPassword)
        {
            if (id != int.Parse(User.FindFirst("Id").Value))
            {
                return NotFound();
            }
            if (newPassword != repeatedPassword)
            {
                ModelState.AddModelError("", "Hasła nie są zgodne.");
                return View();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.PasswordHash = HashPassword(newPassword);
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Pracownik)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
