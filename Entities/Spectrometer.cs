using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Spectrometer
{
    public int SpectrometerId { get; set; }

    public double? MeasurementTime { get; set; }

    public int? PulseCount { get; set; }

    public int? TotalCount { get; set; }

    public int? EnergyWindowsCount { get; set; }

    public int? FlightId { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual ICollection<Metadata> Metadata { get; set; } = new List<Metadata>();
}
