using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AGSS.Entities;

public partial class Customer : INotifyPropertyChanged
{
    private string? _name;
    private string? _person;
    private string? _address;
    private string? _phone;
    private string? _email;

    public int CustomerId { get; set; }

    public string? OrganizationName { get => _name; set { _name = value; OnPropertyChanged(); } }

    public string? ContactPerson { get => _person; set { _person = value; OnPropertyChanged(); } }

    public string? Address { get => _address; set { _address = value; OnPropertyChanged(); } }

    public string? Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

    public string? Email { get => _email; set { _email = value; OnPropertyChanged(); } }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
