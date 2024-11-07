using JobPortal1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal1.Controllers
{
    public class ManageUsersController : Controller
    {
        private readonly JobPortalTester1Context _context;
        public ManageUsersController(JobPortalTester1Context context)
        {
            _context = context;
        }


        // GET: ManageUsers/Index
        public IActionResult Index()
        {
            var users = _context.Users
                .Select(u => new User
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    UserType = u.UserType,
                    IsActive = u.IsActive
                }).ToList();

            return View(users);
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


      
        [HttpPost]
        public async Task<IActionResult> EditUser(int userId, string username, string email, bool isActive)
        {
       
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return RedirectToAction("Index", "ManageUsers", new { message = "User not found." });
            }

         
            user.Username = username;
            user.Email = email;
            user.IsActive = isActive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "ManageUsers", new { message = "Changes saved successfully."});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Toggle the user's active status
            user.IsActive = !user.IsActive;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View(); // This will return the CreateAdmin.cshtml view
        }

        [HttpPost]
        public IActionResult CreateAdmin(User model)
        {
            if (ModelState.IsValid)
            {
                // Set the user type to Admin
                model.UserType = "Admin";
                // Hash the password before saving
                model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);

                // Save the user to the database (assumes you have a repository or dbContext)
                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirect back to the list of users
            }
            return View(model);
        }
      
    }


}

