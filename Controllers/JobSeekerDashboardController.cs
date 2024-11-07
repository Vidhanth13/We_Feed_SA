using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JobPortal1.Models;
using System.Linq;

namespace JobPortal1.Controllers
{
    public class JobSeekerDashboardController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public JobSeekerDashboardController(JobPortalTester1Context context)
        {
            _context = context;
        }

        // GET: Dashboard/Index (Displays the JobSeeker Dashboard)
        [HttpGet]
        public async Task<IActionResult> Index(int jobSeekerId)
        {
            // Get the JobSeeker info
            var jobSeeker = await _context.JobSeekers
                .Include(js => js.JobSeekerPersonalInfos) // Ensure PersonalInfo is loaded
                .FirstOrDefaultAsync(js => js.JobSeekerId == jobSeekerId);

            if (jobSeeker == null)
            {
                return NotFound();
            }
            // Fetch unread notifications for this jobseeker
            // Get relevant notifications for the jobseeker
            var notifications = await _context.Notifications
                .Where(n => n.JobSeekerId == jobSeekerId)
                .ToListAsync();

            // Pass JobSeeker and Notifications to the view
            ViewBag.Notifications = notifications;

            return View(jobSeeker);
        }
        [HttpPost]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == notificationId);
            if (notification == null)
            {
                return NotFound();
            }

            notification.IsRead = true; // Mark the notification as read
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { jobSeekerId = notification.JobSeekerId });
        }

        // POST: Dashboard/MarkAllNotificationsAsRead
        [HttpPost]
        public async Task<IActionResult> MarkAllNotificationsAsRead(int jobSeekerId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.JobSeekerId == jobSeekerId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            _context.Notifications.UpdateRange(notifications);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { jobSeekerId });
        }
    }
}
    

