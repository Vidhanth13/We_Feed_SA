using System;
using System.Collections.Generic;

namespace JobPortal1.Models;

public partial class JobSeeker
{
    public int JobSeekerId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsEmployed { get; set; }

    public bool? VisibilityConsent { get; set; }

    public virtual ICollection<Interest> Interests { get; set; } = new List<Interest>();

    public virtual ICollection<JobSeekerCertificate> JobSeekerCertificates { get; set; } = new List<JobSeekerCertificate>();

    public virtual ICollection<JobSeekerPersonalInfo> JobSeekerPersonalInfos { get; set; } = new List<JobSeekerPersonalInfo>();

    public virtual ICollection<JobSeekerSkill> JobSeekerSkills { get; set; } = new List<JobSeekerSkill>();

    public virtual ICollection<JobSeekerWorkExperience> JobSeekerWorkExperiences { get; set; } = new List<JobSeekerWorkExperience>();

    public virtual User? User { get; set; }
}
