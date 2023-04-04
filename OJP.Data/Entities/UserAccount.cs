using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class UserAccount
{
    public int Id { get; set; }

    public int? UserTypeId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Mobile { get; set; }

    public string? CurrentCity { get; set; }

    public string? Qualification { get; set; }

    public string? Resume { get; set; }

    public virtual SeekerProfile? SeekerProfile { get; set; }

    public virtual Userlog? Userlog { get; set; }
}
