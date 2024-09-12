using System;
using System.Collections.Generic;

namespace WeFeedSA.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();

    public virtual ICollection<JobSeeker> JobSeekers { get; set; } = new List<JobSeeker>();
}
