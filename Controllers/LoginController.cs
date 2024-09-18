using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class LoginController : Controller
    {
        private readonly JobPortalContext _context;

        public LoginController(JobPortalContext context)
        {
            _context = context;
        }

        // GET: Login page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle login (for Job Seeker, Employer, or Admin)
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                // Redirect based on user type
                if (user.UserType == "JobSeeker")
                {
                    return RedirectToAction("Dashboard", "JobSeeker");
                }
                else if (user.UserType == "Employer")
                {
                    return RedirectToAction("Dashboard", "Employer");
                }
                else if (user.UserType == "Admin")
                {
                    return RedirectToAction("AdminDashboard", "Admin"); // Assuming an Admin controller exists
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // Utility method for password verification (should be implemented)
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Implement password hash verification
            return enteredPassword == storedHash;
        }
    }
}
