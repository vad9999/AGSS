using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Flight
{
    public int FlightId { get; set; }

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public double? AltitudeAboveSea { get; set; }

    public double? AltitudeAboveGround { get; set; }

    public double? Speed { get; set; }

    public int? ProjectId { get; set; }

    public int? OperatorId { get; set; }

    public virtual Operator? Operator { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Spectrometer> Spectrometers { get; set; } = new List<Spectrometer>();
}
