using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Metadata : INotifyPropertyChanged
{
    private string? _desc;
    private string? _notes;
    private int? _spectrId;

    public int MetadataId { get; set; }

    public string? EquipmentDescription { get => _desc; set { _desc = value; OnPropertyChanged(); } }

    public string? Notes { get => _notes; set { _notes = value; OnPropertyChanged(); } }

    public int? SpectrometerId { get => _spectrId; set { _spectrId = value; OnPropertyChanged(); } }

    public virtual Spectrometer? Spectrometer { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
