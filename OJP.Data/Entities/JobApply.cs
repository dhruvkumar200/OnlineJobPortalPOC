using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class JobApply
{
    public int Id { get; set; }

    public int? ApplyBy { get; set; }

    public int? JobPostId { get; set; }

    public DateTime? AppliedAt { get; set; }

    public int? Status { get; set; }

    public virtual Login? ApplyByNavigation { get; set; }

    public virtual JobPost? JobPost { get; set; }
}
