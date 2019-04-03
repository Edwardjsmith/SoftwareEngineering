using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Model;
using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_for_Software_Engineering_Project.GUI
{
    /// <summary>
    /// Interaction logic for Project_Window.xaml
    /// </summary>
    public partial class Project_Window : Window, IProject_Window
    {
        IProjectController controller;

        private static List<string> _projectNames = new List<string>();
        public List<IAssetData> assetData = new List<IAssetData>();
        string project_name = "";
        public List<IAssetData> AssetSource
        {
            get => assetData;
        }

        public static List<string> FileNames
        {
            get
            {
                return _projectNames;
            }
            set
            {
                _projectNames = value;
            }
        }



        public Project_Window(IProjectData projectData)
        {
            InitializeComponent();
            
            controller = new ProjectController(this, projectData);
            
        }

        

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            controller.PreviewAsset((AssetData)lbAssets.SelectedItem);
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            controller.UploadFile(project_name);
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            controller.DownloadFile((AssetData)lbAssets.SelectedItem);
        }
    }
}
