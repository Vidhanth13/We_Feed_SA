using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly JobPortalContext _context;

        public AdminDashboardController(JobPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View("AdminDashboard", users); // Map to AdminDashboard view
        }

        public IActionResult ViewPayments()
        {
            var payments = _context.Payments.ToList();
            return View("ViewPaymentInfo", payments); // Map to ViewPaymentInfo view
        }
    }

}
