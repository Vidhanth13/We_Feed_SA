using JobPortal1.Models;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;

namespace JobPortal1.Controllers
{
   
    public class ManageNotificationsController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public ManageNotificationsController(JobPortalTester1Context context)
        {
            _context = context;
        }
        // GET: Manage Notifications page
        [HttpGet]
        [Route("ManageNotifications")]
        public async Task<IActionResult> Index()
        {
            var notifications = await _context.Notifications
                .Where(n => !n.IsRead) // Only unread notifications
                .ToListAsync();

            ViewBag.JobSeekers = await _context.JobSeekers.ToListAsync();
            return View(notifications);
        }


        // POST: Send Notification to Job Seeker
        // POST: Notify JobSeeker
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SendNotification(int jobSeekerId, string message)
        {
            var notification = new Notification
            {
                JobSeekerId = jobSeekerId,    // Targeting the specific jobseeker
                Message = message,
                NotificationType = "JobseekerNotification",
                IsRead = false,
                CreatedAt = DateTime.Now,
                IsActionRequired = true       // Marking as action-required if relevant
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Redirect to Manage Notifications after sending
            return RedirectToAction("Index");
        }


    }

    }
