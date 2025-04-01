using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class AreaCoordinate : INotifyPropertyChanged
{
    private double? _x;
    private double? _y;
    private int? _areaId;

    public int AreaCoordinatesId { get; set; }

    public double? X { get => _x; set { _x = value; OnPropertyChanged(); } }

    public double? Y { get => _y; set { _y = value; OnPropertyChanged(); } }

    public int? AreaId { get => _areaId; set { _areaId = value; OnPropertyChanged(); } }

    public virtual Area? Area { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
