using System;
using System.Collections.Generic;

namespace JobPortal1.Models;

public partial class JobSeekerWorkExperience
{
    public int ExpId { get; set; }

    public int? JobSeekerId { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyAddress { get; set; }

    public string? CompanyPhoneNumber { get; set; }

    public virtual JobSeeker? JobSeeker { get; set; }
}
