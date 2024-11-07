using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal1.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JobPortal1.Controllers
{
    public class JobSeekerCertificateController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public JobSeekerCertificateController(JobPortalTester1Context context)
        {
            _context = context;
        }

        // GET: Certificates/Create (Display the form and existing certificates)
        public async Task<IActionResult> Create(int jobSeekerId)
        {
            ViewBag.JobSeekerId = jobSeekerId;
            var certificates = await _context.JobSeekerCertificates
                .Where(c => c.JobSeekerId == jobSeekerId)
                .ToListAsync();

            ViewBag.Certificates = certificates;
            return View();
        }

        // POST: Certificates/AddCertificate (Add new certificate using AJAX)
        [HttpPost]
        public async Task<IActionResult> AddCertificate(int jobSeekerId, IFormFile certificateFile)
        {
            if (certificateFile != null && certificateFile.Length > 0)
            {
                // Save the file to a folder in wwwroot
                var filePath = Path.Combine("wwwroot/uploads/certificates", certificateFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await certificateFile.CopyToAsync(stream);
                }

                // Add the new certificate to the database
                var certificate = new JobSeekerCertificate
                {
                    JobSeekerId = jobSeekerId,
                    DocName = certificateFile.FileName,
                    DocUrl = filePath
                };

                _context.JobSeekerCertificates.Add(certificate);
                await _context.SaveChangesAsync();

                // Return the added certificate as a JSON response to be appended on the front-end
                return Json(new { docId = certificate.SupDocumentsId, docName = certificate.DocName });
            }

            return BadRequest("Error uploading certificate");
        }

        // POST: Certificates/RemoveCertificate (Remove a certificate using AJAX)
        [HttpPost]
        public async Task<IActionResult> RemoveCertificate(int docId)
        {
            var certificate = await _context.JobSeekerCertificates.FindAsync(docId);
            if (certificate != null)
            {
                // Remove the certificate entry from the database
                _context.JobSeekerCertificates.Remove(certificate);
                await _context.SaveChangesAsync();

                // Optionally: Remove the file from the server if needed
                if (System.IO.File.Exists(certificate.DocUrl))
                {
                    System.IO.File.Delete(certificate.DocUrl);
                }

                return Json(new { success = true, docId });
            }

            return BadRequest("Error removing certificate");
        }

        // POST: Certificates/CompleteCertificates (Finalize and redirect to JobSeeker Dashboard)
        [HttpPost]
        public IActionResult CompleteCertificates(int jobSeekerId)
        {
            // Redirect to JobSeeker Dashboard after completing certificates section
            return RedirectToAction("Index", "JobSeekerDashboard", new { jobSeekerId });
        }
    }
}
