using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class EmployerDashboardController : Controller
    {
        private readonly JobPortalContext _context;

        public EmployerDashboardController(JobPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index(int employerId)
        {
            var employer = _context.Employers
                .Include(e => e.Jobs)
                .Include(e => e.Interests)
                .Include(e => e.Interests.Select(i => i.Payments))
                .FirstOrDefault(e => e.EmployerId == employerId);

            return View("EmployerDashboard", employer); // Map to EmployerDashboard view
        }
    }

}
