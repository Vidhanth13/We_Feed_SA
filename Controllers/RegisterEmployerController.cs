using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class RegisterEmployerController : Controller
    {
        private readonly JobPortalContext _context;

        public RegisterEmployerController(JobPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("RegisterEmployer"); // Ensures correct view mapping
        }

        [HttpPost]
        public IActionResult Register(RegisterEmployerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = HashPassword(model.Password),
                    Email = model.Email,
                    UserType = "Employer",
                    DateCreated = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                var employer = new Employer
                {
                    CompanyName = model.CompanyName,
                    ContactPerson = model.ContactPerson,
                    Industry = model.Industry,
                    Location = model.Location,
                    PhoneNumber = model.PhoneNumber,
                    UserId = user.UserId
                };

                _context.Employers.Add(employer);
                _context.SaveChanges();

                return RedirectToAction("EmployerDashboard", "EmployerDashboard");
            }
            return View("RegisterEmployer", model); // Ensure correct view mapping
        }
    }

}
