﻿using System.Diagnostics;
using System.Runtime.Serialization;

namespace PrimalEditor.GameProject;

[DataContract]
public class Scene : ViewModelBase
{
    private string _name;
    [DataMember]
    public string Name
    {
        get => _name;
        set
        {
            if (_name == value) return;
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    
    [DataMember]
    public Project Project { get; private set; }

    public Scene(Project project, string name)
    {
        Debug.Assert(project != null);
        Project = project;
        Name = name;
    }
}