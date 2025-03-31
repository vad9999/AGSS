using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Operator
{
    public int OperatorId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
