using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly JobPortalContext _context;

        public UserRegisterController(JobPortalContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register (handles both Job Seeker and Employer registration)
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Hash password and save user
                user.PasswordHash = HashPassword(user.PasswordHash); // Implement password hashing

                _context.Add(user);
                await _context.SaveChangesAsync();

                // Redirect based on user type
                if (user.UserType == "JobSeeker")
                {
                    return RedirectToAction("CreateJobSeekerProfile", "JobSeeker");
                }
                else if (user.UserType == "Employer")
                {
                    return RedirectToAction("CreateEmployerProfile", "Employer");
                }
            }
            return View(user);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.PasswordHash)) // Implement password verification
            {
                // Logic for login, set session, etc.
                return RedirectToAction("Dashboard");
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View();
        }

        // Utility functions for password hashing/verification
        private string HashPassword(string password)
        {
            // Implement password hashing (e.g., BCrypt or SHA-256)
            return password;
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Implement password verification
            return enteredPassword == storedHash;
        }
    }
}
