using System.Windows;

namespace PrimalEditor.GameProject;

public partial class NewProjectView
{
    public NewProjectView()
    {
        InitializeComponent();
    }

    private void OnCreate_Button_Click(object sender, RoutedEventArgs e)
    {
        var vm = DataContext as NewProject;
        var projectPath = vm!.CreateProject(TemplateListBox.SelectedItem as ProjectTemplate);
        var dialogResult = false;
        var win = Window.GetWindow(this);
        if (!string.IsNullOrEmpty(projectPath))
        {
            dialogResult = true;
        }
        win.DialogResult = dialogResult;
        win.Close();    
    }
}