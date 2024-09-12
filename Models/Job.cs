using System;
using System.Collections.Generic;

namespace WeFeedSA.Models;

public partial class Job
{
    public int JobId { get; set; }

    public int? EmployerId { get; set; }

    public string JobTitle { get; set; } = null!;

    public string JobDescription { get; set; } = null!;

    public string JobType { get; set; } = null!;

    public string? Location { get; set; }

    public string? RequiredSkills { get; set; }

    public string? ExperienceLevel { get; set; }

    public string? SalaryRange { get; set; }

    public DateTime? PostedDate { get; set; }

    public DateTime? ApplicationDeadline { get; set; }

    public bool? IsActive { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
