using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class ExperienceDetail
{
    public int UserAccountId { get; set; }

    public bool? IsCurrentJob { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? JobTitle { get; set; }

    public string? CompanyName { get; set; }

    public string? JobLocationCity { get; set; }

    public string? JobLocaionState { get; set; }

    public string? JobLocationCountry { get; set; }

    public string? Description { get; set; }

    public virtual SeekerProfile UserAccount { get; set; } = null!;
}
