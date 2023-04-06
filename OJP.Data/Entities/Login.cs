using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class Login
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? RoleId { get; set; }

    public string? Profile { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<EducationDetail> EducationDetails { get; } = new List<EducationDetail>();

    public virtual ICollection<JobApply> JobApplies { get; } = new List<JobApply>();

    public virtual ICollection<JobPost> JobPosts { get; } = new List<JobPost>();

    public virtual ICollection<TechnicalDetail> TechnicalDetails { get; } = new List<TechnicalDetail>();
}
