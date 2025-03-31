using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Channel2
{
    public int Channel2Id { get; set; }

    public double? MeasurementResult { get; set; }

    public int? ProfileCoordinatesId { get; set; }

    public virtual ProfileCoordinate? ProfileCoordinates { get; set; }
}
