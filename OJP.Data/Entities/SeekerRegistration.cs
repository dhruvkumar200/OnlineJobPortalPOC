using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class SeekerRegistration
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Mobile { get; set; }

    public string? CurrentCity { get; set; }

    public string? Qualification { get; set; }

    public string? Resume { get; set; }

    public virtual SeekerProfile? SeekerProfile { get; set; }

    public virtual Userlog? Userlog { get; set; }
}
