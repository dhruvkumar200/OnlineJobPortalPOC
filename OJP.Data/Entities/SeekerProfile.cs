using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class SeekerProfile
{
    public int UserAccountId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? CurrentSalary { get; set; }

    public virtual SeekerRegistration UserAccount { get; set; } = null!;
}
