using System;
using System.Collections.Generic;

namespace JobPortal1.Models;

public partial class Employer
{
    public int EmployerId { get; set; }

    public int? UserId { get; set; }

    public string? CompanyName { get; set; }

    public string? ContactPersonFirstName { get; set; }

    public string? ContactPersonLastName { get; set; }

    public string? Industry { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public string? Suburb { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public string EmployerType { get; set; } = null!;

    public virtual ICollection<Interest> Interests { get; set; } = new List<Interest>();

    public virtual User? User { get; set; }
}
