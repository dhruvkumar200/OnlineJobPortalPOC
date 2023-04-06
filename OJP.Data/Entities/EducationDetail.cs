using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class EducationDetail
{
    public int Id { get; set; }

    public string? Institute { get; set; }

    public decimal? Percentage { get; set; }

    public decimal? Cgpa { get; set; }

    public string? Specilization { get; set; }

    public string? Resume { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Login? CreatedByNavigation { get; set; }
}
