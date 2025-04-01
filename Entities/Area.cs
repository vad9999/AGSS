using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Area : INotifyPropertyChanged
{
    private string? _geologicalInfo;
    private double? _area1;
    private int? _profileCount;
    private int? _breaksCount;
    private int? _projectId;

    public int AreaId { get; set; }

    public string? GeologicalInfo 
    { 
        get => _geologicalInfo; 
        set {  _geologicalInfo = value;  OnPropertyChanged();  }
    }

    public double? Area1 
    { 
        get => _area1; 
        set {  _area1 = value; OnPropertyChanged(); }
    }

    public int? ProfileCount 
    { 
        get => _profileCount; 
        set { _profileCount = value; OnPropertyChanged(); } 
    }

    public int? BreaksCount 
    { 
        get => _breaksCount; 
        set { _breaksCount = value; OnPropertyChanged(); } 
    }

    public int? ProjectId 
    { 
        get => _projectId;
        set { _projectId = value; OnPropertyChanged(); } 
    }

    public virtual ICollection<AreaCoordinate> AreaCoordinates { get; set; } = new List<AreaCoordinate>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual Project? Project { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
