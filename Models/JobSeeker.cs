using System;
using System.Collections.Generic;

namespace WeFeedSA.Models;

public partial class JobSeeker
{
    public int JobSeekerId { get; set; }

    public int? UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Location { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? VisibilityConsent { get; set; }

    public string? ProfileStatus { get; set; }

    public virtual ICollection<Cv> Cvs { get; set; } = new List<Cv>();

    public virtual ICollection<Interest> Interests { get; set; } = new List<Interest>();

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual ICollection<JobSeekerSkill> JobSeekerSkills { get; set; } = new List<JobSeekerSkill>();

    public virtual User? User { get; set; }
}
