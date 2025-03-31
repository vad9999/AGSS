using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Channel3
{
    public int Channel3Id { get; set; }

    public double? MeasurementResult { get; set; }

    public int? ProfileCoordinatesId { get; set; }

    public virtual ProfileCoordinate? ProfileCoordinates { get; set; }
}
