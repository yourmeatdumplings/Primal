using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using PrimalEditor.Utilities;

namespace PrimalEditor.GameProject;

[DataContract]
public class ProjectTemplate
{
    [DataMember]
    public string? ProjectType { get; set; }
    [DataMember]
    public string? ProjectFile { get; set; }
    [DataMember]
    public List<string>?  Folders { get; set; }

    public byte[]? Icon { get; set; }
    public byte[]? Screenshot { get; set; }
    public string? IconFilePath { get; set; }
    public string? ScreenshotFilePath { get; set; }
    public string? ProjectFilePath { get; set; }
}

public class NewProject : ViewModelBase
{
    //TODO: Path
    private readonly string _templatePath = @"..\..\PrimalEditor\ProjectTemplates";
    private string _projectProjectName = "NewProject";
    public string ProjectName
    {
        get => _projectProjectName;
        set
        {
            if (_projectProjectName == value) return;
            _projectProjectName = value;
            OnPropertyChanged(nameof(ProjectName));
        }
    }
    
    private string _projectProjectPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\PrimalProjects\";
    public string ProjectPath
    {
        get => _projectProjectPath;
        set
        {
            if (_projectProjectPath == value) return;
            _projectProjectPath = value;
            OnPropertyChanged(nameof(ProjectPath));
        }
    }

    private readonly ObservableCollection<ProjectTemplate> _projectTemplates = [];
    public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates 
    { get; }

    public NewProject()
    {
        ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemplate>(_projectTemplates);
        try
        {
            var templatesFiles = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
            Debug.Assert(templatesFiles.Length != 0);
            foreach (var file in templatesFiles)
            {
               var template = Serializer.FromFile<ProjectTemplate>(file);
               template.IconFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file)!, "Icon.png"));
               template.Icon = File.ReadAllBytes(template.IconFilePath);
               template.ScreenshotFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file)!, "Screenshot.png"));
               template.Screenshot = File.ReadAllBytes(template.ScreenshotFilePath);
               template.ProjectFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file)!, template.ProjectFile!));
               
               _projectTemplates.Add(template);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            //TODO: log error
        }
    }
}