using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal1.Models;
using System.Threading.Tasks;
using System.Linq;

namespace JobPortal1.Controllers
{
    public class JobSeekerWorkExpController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public JobSeekerWorkExpController(JobPortalTester1Context context)
        {
            _context = context;
        }

        // GET: WorkExperience/Create
        public async Task<IActionResult> Create(int jobSeekerId)
        {
            ViewBag.JobSeekerId = jobSeekerId;
            var workExperience = await _context.JobSeekerWorkExperiences
                .Where(w => w.JobSeekerId == jobSeekerId)
                .ToListAsync();

            ViewBag.WorkExperience = workExperience;
            return View();
        }

        // POST: Add new work experience via AJAX
        [HttpPost]
        public async Task<IActionResult> AddExperience(JobSeekerWorkExperience workExperience)
        {
            if (ModelState.IsValid)
            {
                _context.JobSeekerWorkExperiences.Add(workExperience);
                await _context.SaveChangesAsync();
                return Json(new { expId = workExperience.ExpId, companyName = workExperience.CompanyName, companyPhoneNumber = workExperience.CompanyPhoneNumber });
            }
            return BadRequest("Error adding work experience");
        }

        // POST: Remove work experience via AJAX
        [HttpPost]
        public async Task<IActionResult> RemoveExperience(int expId)
        {
            var experience = await _context.JobSeekerWorkExperiences.FindAsync(expId);
            if (experience != null)
            {
                _context.JobSeekerWorkExperiences.Remove(experience);
                await _context.SaveChangesAsync();
                return Json(new { success = true, expId });
            }
            return BadRequest("Error removing work experience");
        }

        // POST: Redirect to JobSeekerCertificates after work experience
        [HttpPost]
        public async Task<IActionResult> CompleteExperience(int jobSeekerId)
        {
            // Redirect to the JobSeekerCertificates page after adding work experience
            return RedirectToAction("Create", "JobSeekerCertificate", new { jobSeekerId });
        }
    }
}
