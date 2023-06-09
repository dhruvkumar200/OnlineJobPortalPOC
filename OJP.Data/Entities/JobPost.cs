﻿using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class JobPost
{
    public int Id { get; set; }

    public string? JobTitle { get; set; }

    public int? JobType { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? Salary { get; set; }

    public string? JobDescription { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int PostedBy { get; set; }

    public virtual ICollection<JobApply> JobApplies { get; } = new List<JobApply>();

    public virtual Login PostedByNavigation { get; set; } = null!;
}
