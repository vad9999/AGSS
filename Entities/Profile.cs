using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Profile
{
    public int ProfileId { get; set; }

    public int? BreaksCount { get; set; }

    public int? AreaId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<ProfileCoordinate> ProfileCoordinates { get; set; } = new List<ProfileCoordinate>();
}
