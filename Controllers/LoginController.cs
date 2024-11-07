using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using JobPortal1.Models;
using BCrypt.Net;

namespace JobPortal1.Controllers
{
    public class LoginController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public LoginController(JobPortalTester1Context context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View(); // Returns the login view
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(User model)
        {
            if (!string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                // Check if the user is found and the password is correct
                if (user != null && BCrypt.Net.BCrypt.Verify(model.PasswordHash, user.PasswordHash))
                {
                    // Redirect based on user type
                    if (user.UserType == "JobSeeker")
                    {
                        return RedirectToAction("Index", "JobSeekerDashboard", new { jobSeekerId = user.UserId });
                    }
                    else if (user.UserType == "Employer")
                    {
                        return RedirectToAction("Index", "EmployerDashboard", new { employerId = user.UserId });
                    }
                    else if (user.UserType == "Admin")
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                }
                else
                {
                    // If user not found or password is incorrect
                    TempData["LoginError"] = "Invalid email or password.";
                    return View(model);
                }
            }
            else
            {
                TempData["LoginError"] = "Email and Password are required.";
            }
            return View(model);
        }

        // GET: Logout
        public IActionResult Logout()
        {
            // Clear session or cookies if necessary, then redirect to login
            return RedirectToAction("Index");
        }
    }
}
