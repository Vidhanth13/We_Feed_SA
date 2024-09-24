using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home Page (Landing Page)
        public IActionResult Index()
        {
            return View();
        }

        // GET: About Us Page
        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // GET: Contact Us Page
        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // GET: Privacy Policy Page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error Handling (Used for displaying custom error pages)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
