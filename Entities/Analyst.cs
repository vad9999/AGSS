using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Analyst : INotifyPropertyChanged
{
    private string? _fullname;
    private string? _phone;
    private string? _adress;

    public int AnalystId { get; set; }

    public string? FullName
    {
        get => _fullname;
        set { _fullname = value; OnPropertyChanged(); }
    }

    public string? Phone
    {
        get => _phone;
        set { _phone = value; OnPropertyChanged(); }
    }

    public string? Address
    {
        get => _adress;
        set { _adress = value; OnPropertyChanged(); }
    }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
