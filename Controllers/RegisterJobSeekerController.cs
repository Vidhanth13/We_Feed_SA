using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class RegisterJobSeekerController : Controller
    {
        private readonly JobPortalContext _context;

        public RegisterJobSeekerController(JobPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("RegisterJobSeeker"); // Ensures it returns the correct view
        }

        [HttpPost]
        public IActionResult Register(RegisterJobSeekerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = HashPassword(model.Password),
                    Email = model.Email,
                    UserType = "JobSeeker",
                    DateCreated = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                var jobSeeker = new JobSeeker
                {
                    FullName = model.FullName,
                    Location = model.Location,
                    PhoneNumber = model.PhoneNumber,
                    VisibilityConsent = model.VisibilityConsent,
                    UserId = user.UserId
                };

                _context.JobSeekers.Add(jobSeeker);
                _context.SaveChanges();

                return RedirectToAction("JobSeekerDashboard", "JobSeekerDashboard");
            }
            return View("RegisterJobSeeker", model); // Correct mapping of the view
        }
    }

}
