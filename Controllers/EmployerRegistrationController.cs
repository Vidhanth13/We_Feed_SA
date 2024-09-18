using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class EmployerRegistrationController : Controller
    {
        private readonly JobPortalContext _context;

        public EmployerRegistrationController(JobPortalContext context)
        {
            _context = context;
        }

        // GET: Employer registration form
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register new Employer
        [HttpPost]
        public async Task<IActionResult> Register(Employer employer, string username, string password)
        {
            if (ModelState.IsValid)
            {
                // Create new User record for Employer
                var user = new User
                {
                    Username = username,
                    PasswordHash = HashPassword(password),
                    UserType = "Employer", // Set the user type
                    Email = employer.ContactPerson // Example, can be adjusted as needed
                };
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Create Employer record
                employer.EmployerId = user.UserId;
                _context.Add(employer);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "Employer");
            }

            return View(employer);
        }

        private string HashPassword(string password)
        {
            // Implement password hashing (e.g., BCrypt or SHA-256)
            return password; // Dummy, replace with actual hashing
        }
    }
}
