using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? OrganizationName { get; set; }

    public string? ContactPerson { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
