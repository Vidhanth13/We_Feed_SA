using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class JobSeekerRegistrationController : Controller
    {
        private readonly JobPortalContext _context;

        public JobSeekerRegistrationController(JobPortalContext context)
        {
            _context = context;
        }

        // GET: Job Seeker registration form
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register new Job Seeker
        [HttpPost]
        public async Task<IActionResult> Register(JobSeeker jobSeeker, string username, string password)
        {
            if (ModelState.IsValid)
            {
                // Create new User record for Job Seeker
                var user = new User
                {
                    Username = username,
                    PasswordHash = HashPassword(password),
                    UserType = "JobSeeker", // Set the user type
                    Email = jobSeeker.FullName // Example, adjust as needed
                };
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Create Job Seeker record
                jobSeeker.JobSeekerId = user.UserId;
                _context.Add(jobSeeker);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "JobSeeker");
            }

            return View(jobSeeker);
        }

        private string HashPassword(string password)
        {
            // Implement password hashing (e.g., BCrypt or SHA-256)
            return password; // Dummy, replace with actual hashing
        }
    }
}
