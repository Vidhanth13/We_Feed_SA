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

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login"); // Map to the login view
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == HashPassword(model.Password));

                if (user != null)
                {
                    if (user.UserType == "JobSeeker")
                        return RedirectToAction("JobSeekerDashboard", "JobSeekerDashboard");
                    else if (user.UserType == "Employer")
                        return RedirectToAction("EmployerDashboard", "EmployerDashboard");
                    else if (user.UserType == "Admin")
                        return RedirectToAction("AdminDashboard", "AdminDashboard");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View("Login", model); // Map to the login view
        }
    }
}
