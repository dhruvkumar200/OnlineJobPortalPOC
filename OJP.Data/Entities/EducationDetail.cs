using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class EducationDetail
{
    public int Id { get; set; }

    public int LoginId { get; set; }

    public string? Institute { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? Percentage { get; set; }

    public int? Cgpa { get; set; }

    public virtual Login Login { get; set; } = null!;
}
