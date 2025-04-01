using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Channel3 : INotifyPropertyChanged
{
    private double? _result;
    private int? _coord;
    public int Channel3Id { get; set; }

    public double? MeasurementResult { get => _result; set { _result = value; OnPropertyChanged(); } }

    public int? ProfileCoordinatesId { get => _coord; set { _coord = value; OnPropertyChanged(); } }

    public virtual ProfileCoordinate? ProfileCoordinates { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
