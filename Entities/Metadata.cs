using System;
using System.Collections.Generic;

namespace AGSS.Entities;

public partial class Metadata
{
    public int MetadataId { get; set; }

    public string? EquipmentDescription { get; set; }

    public string? Notes { get; set; }

    public int? SpectrometerId { get; set; }

    public virtual Spectrometer? Spectrometer { get; set; }
}
