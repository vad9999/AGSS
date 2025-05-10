using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Project : INotifyPropertyChanged
{
    private string _projectName = null!;
    private string _notes = null!;
    private int? _customerId;
    private int? _chiefEngineerId;
    private int? _leadSpecialistId;

    public int ProjectId { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set { _projectName = value; OnPropertyChanged(); }
    }

    public string Notes
    {
        get => _notes;
        set { _notes = value; OnPropertyChanged(); }
    }

    public int? CustomerId
    {
        get => _customerId;
        set { _customerId = value; OnPropertyChanged(); }
    }

    public int? ChiefEnginnerId
    {
        get => _chiefEngineerId;
        set { _chiefEngineerId = value; OnPropertyChanged(); }
    }

    public int? LeadSpecialistId
    {
        get => _leadSpecialistId;
        set { _leadSpecialistId = value; OnPropertyChanged(); }
    }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
    public virtual ChiefEnginner? ChiefEnginner { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
    public virtual LeadSpecialist? LeadSpecialist { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
