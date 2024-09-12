using System;
using System.Collections.Generic;

namespace WeFeedSA.Models;

public partial class Cv
{
    public int Cvid { get; set; }

    public int? JobSeekerId { get; set; }

    public string? Summary { get; set; }

    public string? Education { get; set; }

    public string? Experience { get; set; }

    public string? Certifications { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual JobSeeker? JobSeeker { get; set; }
}
