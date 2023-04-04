using System;
using System.Collections.Generic;

namespace OJP.Data.Entities;

public partial class RecruiterRegistration
{
    public long? Id { get; set; }

    public string? CompanyName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? ConfirmPassword { get; set; }

    public string? Mobile { get; set; }

    public string? Designation { get; set; }

    public string? City { get; set; }

    public int? EmployeeNumbers { get; set; }

    public string? ClientRequirement { get; set; }
}
