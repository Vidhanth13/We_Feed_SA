using System;
using System.Collections.Generic;

namespace JobPortal1.Models;

public partial class JobSeekerPersonalInfo
{
    public int PersonalInfoId { get; set; }

    public int? JobSeekerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public string? Suburb { get; set; }

    public string LevelOfEducation { get; set; } = null!;

    public string? AboutYou { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual JobSeeker? JobSeeker { get; set; }
}
