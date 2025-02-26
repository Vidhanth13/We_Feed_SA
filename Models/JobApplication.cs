﻿using System;
using System.Collections.Generic;

namespace WeFeedSA.Models;

public partial class JobApplication
{
    public int ApplicationId { get; set; }

    public int? JobId { get; set; }

    public int? JobSeekerId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string? ApplicationStatus { get; set; }

    public string? CoverLetter { get; set; }

    public virtual Job? Job { get; set; }

    public virtual JobSeeker? JobSeeker { get; set; }
}
