using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class ProfileCoordinate
{
    public int ProfileCoordinatesId { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public int? ProfileId { get; set; }

    public virtual ICollection<Channel1> Channel1s { get; set; } = new List<Channel1>();

    public virtual ICollection<Channel2> Channel2s { get; set; } = new List<Channel2>();

    public virtual ICollection<Channel3> Channel3s { get; set; } = new List<Channel3>();

    public virtual Profile? Profile { get; set; }
}
