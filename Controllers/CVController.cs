using Microsoft.AspNetCore.Mvc;
using WeFeedSA.Models;

namespace WeFeedSA.Controllers
{
    public class CVController : Controller
    {
        private readonly JobPortalContext _context;

        public CVController(JobPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateCV(int jobSeekerId)
        {
            return View("CreateCV", new CvViewModel { JobSeekerId = jobSeekerId });
        }

        [HttpPost]
        public IActionResult CreateCV(CvViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cv = new Cv
                {
                    JobSeekerId = model.JobSeekerId,
                    Summary = model.Summary,
                    Education = model.Education,
                    Experience = model.Experience,
                    Certifications = model.Certifications,
                    LastUpdated = DateTime.Now
                };

                _context.Cvs.Add(cv);
                _context.SaveChanges();

                return RedirectToAction("JobSeekerDashboard", "JobSeekerDashboard");
            }
            return View("CreateCV", model);
        }

        [HttpGet]
        public IActionResult UpdateCV(int cvId)
        {
            var cv = _context.Cvs.FirstOrDefault(c => c.Cvid == cvId);
            return View("UpdateCV", cv); // Map to UpdateCV view
        }

        [HttpPost]
        public IActionResult UpdateCV(CvViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cv = _context.Cvs.FirstOrDefault(c => c.Cvid == model.Cvid);
                if (cv != null)
                {
                    cv.Summary = model.Summary;
                    cv.Education = model.Education;
                    cv.Experience = model.Experience;
                    cv.Certifications = model.Certifications;
                    cv.LastUpdated = DateTime.Now;

                    _context.SaveChanges();
                    return RedirectToAction("JobSeekerDashboard", "JobSeekerDashboard");
                }
            }
            return View("UpdateCV", model); // Map to UpdateCV view
        }
    }

}
