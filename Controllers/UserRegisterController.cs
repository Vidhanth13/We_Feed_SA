using Microsoft.AspNetCore.Mvc;
using JobPortal1.Models;
using System.Threading.Tasks;

namespace JobPortal1.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public UserRegisterController(JobPortalTester1Context context)
        {
            _context = context;
        }

        // GET: UserRegister/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRegister/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,PasswordHash,Email,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                user.IsActive = true; // Mark user as active
                user.DateCreated = DateTime.Now;

                // Save the new user to the database
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Redirect based on the UserType
                if (user.UserType == "JobSeeker")
                {

                    // Redirect to JobSeekerRegister with the new user's ID
                    return RedirectToAction("Create", "JobSeekerRegister", new { userId = user.UserId });
                }
                else if (user.UserType == "Employer")
                {
                    // Redirect to EmployerRegister with the new user's ID
                    return RedirectToAction("Create", "EmployerRegister", new { userId = user.UserId });
                }
            }

            // If model state is not valid, return the view with the user model to show errors
            return View(user);
        }
    }
}
