using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal1.Models;
using System.Threading.Tasks;
using System.Linq;

namespace JobPortal1.Controllers
{
    public class SkillsController : Controller
    {
        private readonly JobPortalTester1Context _context;

        public SkillsController(JobPortalTester1Context context)
        {
            _context = context;
        }

        // GET: Skills/Create (Display the Skills form and existing skills)
        public async Task<IActionResult> Create(int jobSeekerId)
        {
            ViewBag.JobSeekerId = jobSeekerId;
            var skills = await _context.JobSeekerSkills
                .Where(s => s.JobSeekerId == jobSeekerId)
                .ToListAsync();

            ViewBag.Skills = skills;
            return View();
        }

        // POST: Skills/AddSkill (Add a new skill using AJAX)
        [HttpPost]
        public async Task<IActionResult> AddSkill(JobSeekerSkill skill)
        {
            if (ModelState.IsValid)
            {
                // Add the new skill to the database
                _context.JobSeekerSkills.Add(skill);
                await _context.SaveChangesAsync();

                // Return the added skill as a JSON response to be appended on the front-end
                return Json(new { skillId = skill.SkillId, skillName = skill.SkillName, skillLevel = skill.SkillLevel });
            }

            return BadRequest("Error adding skill");
        }

        // POST: Skills/RemoveSkill (Remove a skill using AJAX)
        [HttpPost]
        public async Task<IActionResult> RemoveSkill(int skillId)
        {
            var skill = await _context.JobSeekerSkills.FindAsync(skillId);
            if (skill != null)
            {
                _context.JobSeekerSkills.Remove(skill);
                await _context.SaveChangesAsync();
                return Json(new { success = true, skillId });
            }

            return BadRequest("Error removing skill");
        }

        // POST: Skills/CompleteSkills (Redirect to JobSeeker Certificates page after adding skills)
        [HttpPost]
        public async Task<IActionResult> CompleteSkills(int jobSeekerId)
        {
            // Redirect to JobSeeker Certificates page after completing the Skills section
            return RedirectToAction("Create", "JobSeekerWorkExp", new { jobSeekerId });
        }
    }
}
