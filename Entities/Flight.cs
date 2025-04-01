using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Flight : INotifyPropertyChanged
{
    private DateTime? _start;
    private DateTime? _end;
    private double? _sea;
    private double? _ground;
    private double? _speed;
    private int? _projectId;
    private int? _operatorId;

    public int FlightId { get; set; }

    public DateTime? StartDateTime { get => _start; set { _start = value; OnPropertyChanged(); } }

    public DateTime? EndDateTime { get => _end; set { _end = value; OnPropertyChanged(); } }

    public double? AltitudeAboveSea { get => _sea; set { _sea = value; OnPropertyChanged(); } }

    public double? AltitudeAboveGround { get => _ground; set { _ground = value; OnPropertyChanged(); } }

    public double? Speed { get => _speed; set { _speed = value; OnPropertyChanged(); } }

    public int? ProjectId { get => _projectId; set { _projectId = value; OnPropertyChanged(); } }

    public int? OperatorId { get => _operatorId; set { _operatorId = value; OnPropertyChanged(); } }

    public virtual Operator? Operator { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Spectrometer> Spectrometers { get; set; } = new List<Spectrometer>();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
