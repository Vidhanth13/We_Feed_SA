using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class NotificationController : Controller
    {
        private readonly JobPortalContext _context;

        public NotificationController(JobPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index(int userId)
        {
            var notifications = _context.Notifications
                .Where(n => n.UserId == userId)
                .ToList();

            return View("JobSeekerNotifications", notifications); // Map to JobSeekerNotifications view
        }
    }

}
