using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class AreaCoordinate
{
    public int AreaCoordinatesId { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public int? AreaId { get; set; }

    public virtual Area? Area { get; set; }
}
