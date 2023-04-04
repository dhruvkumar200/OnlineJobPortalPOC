using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class TechnicalDetail
{
    public int Id { get; set; }

    public int LoginId { get; set; }

    public string? JobTitle { get; set; }

    public string? CompanyName { get; set; }

    public string? Experience { get; set; }

    public string? Description { get; set; }

    public string? TechSkills { get; set; }

    public virtual Login Login { get; set; } = null!;
}
