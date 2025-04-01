using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Profile : INotifyPropertyChanged
{
    private int? _count;
    private int? _areaId;

    public int ProfileId { get; set; }

    public int? BreaksCount { get => _count; set { _count = value; OnPropertyChanged(); } }

    public int? AreaId { get => _areaId; set { _areaId = value; OnPropertyChanged(); } }

    public virtual Area? Area { get; set; }

    public virtual ICollection<ProfileCoordinate> ProfileCoordinates { get; set; } = new List<ProfileCoordinate>();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
