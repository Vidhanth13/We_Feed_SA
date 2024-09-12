using System;
using System.Collections.Generic;

namespace WeFeedSA.Models;

public partial class Employer
{
    public int EmployerId { get; set; }

    public int? UserId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? Industry { get; set; }

    public string? Location { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public virtual ICollection<Interest> Interests { get; set; } = new List<Interest>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual User? User { get; set; }
}
