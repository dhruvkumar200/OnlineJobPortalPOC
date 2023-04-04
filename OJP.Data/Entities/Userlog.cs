using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class Userlog
{
    public int UserAccountId { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastJobApplyDate { get; set; }

    public virtual SeekerRegistration UserAccount { get; set; } = null!;
}
