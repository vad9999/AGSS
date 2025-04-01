using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Spectrometer : INotifyPropertyChanged
{
    private double? _time;
    private int? _pulsecount;
    private int? _totalcount;
    private int? _windowcount;
    private int? _flight;

    public int SpectrometerId { get; set; }

    public double? MeasurementTime { get => _time; set { _time = value; OnPropertyChanged(); } }

    public int? PulseCount { get => _pulsecount; set { _pulsecount = value; OnPropertyChanged(); } }

    public int? TotalCount { get => _totalcount; set { _totalcount = value; OnPropertyChanged(); } }

    public int? EnergyWindowsCount { get => _windowcount; set { _windowcount = value; OnPropertyChanged(); } }

    public int? FlightId { get => _flight; set { _flight = value; OnPropertyChanged(); } }

    public virtual Flight? Flight { get; set; }

    public virtual ICollection<Metadata> Metadata { get; set; } = new List<Metadata>();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
