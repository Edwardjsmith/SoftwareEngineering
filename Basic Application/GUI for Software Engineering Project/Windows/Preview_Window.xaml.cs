using GUI_for_Software_Engineering_Project.Controller;
using GUI_for_Software_Engineering_Project.Interfaces;
using System.Windows;
namespace GUI_for_Software_Engineering_Project.Windows
{
    /// <summary>
    /// Interaction logic for Preview_Window.xaml
    /// </summary>
    public partial class Preview_Window : Window
    {
        IPreviewController controller;

        public Preview_Window(IAssetData asset, IProjectData project)
        {
            InitializeComponent();
            controller = new PreviewController(this, asset, project);

        }
    }
}
