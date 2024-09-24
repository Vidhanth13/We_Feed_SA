using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class JobSeekerDashboardController : Controller
    {
        private readonly JobPortalContext _context;

        public JobSeekerDashboardController(JobPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index(int jobSeekerId)
        {
            var jobSeeker = _context.JobSeekers
                .Include(js => js.JobApplications)
                .Include(js => js.Cvs)
                .Include(js => js.Notifications)
                .FirstOrDefault(js => js.JobSeekerId == jobSeekerId);

            return View("JobSeekerDashboard", jobSeeker); // Map to JobSeekerDashboard view
        }
    }

}
