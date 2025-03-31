using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Area
{
    public int AreaId { get; set; }

    public string? GeologicalInfo { get; set; }

    public double? Area1 { get; set; }

    public int? ProfileCount { get; set; }

    public int? BreaksCount { get; set; }

    public int? ProjectId { get; set; }

    public virtual ICollection<AreaCoordinate> AreaCoordinates { get; set; } = new List<AreaCoordinate>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual Project? Project { get; set; }
}
