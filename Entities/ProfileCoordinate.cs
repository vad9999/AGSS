using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class ProfileCoordinate : INotifyPropertyChanged
{
    private double? _x;
    private double? _y;
    private int? _profile;

    public int ProfileCoordinatesId { get; set; }

    public double? X { get => _x; set { _x = value; OnPropertyChanged(); } }

    public double? Y { get => _y; set { _y = value; OnPropertyChanged(); } }

    public int? ProfileId { get => _profile; set { _profile = value; OnPropertyChanged(); } }

    public virtual ICollection<Channel1> Channel1s { get; set; } = new List<Channel1>();

    public virtual ICollection<Channel2> Channel2s { get; set; } = new List<Channel2>();

    public virtual ICollection<Channel3> Channel3s { get; set; } = new List<Channel3>();

    public virtual Profile? Profile { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
