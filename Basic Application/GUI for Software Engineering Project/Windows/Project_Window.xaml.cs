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
        
        public List<IAssetData> assetData = new List<IAssetData>();

        IProjectData project;

        public List<IAssetData> AssetSource
        {
            get => assetData;
        }

        public Project_Window(IProjectData project)
        {
            InitializeComponent();
            this.project = project;
            controller = new ProjectController(this, project);
            lbAssets.ItemsSource = AssetSource;
            
        }


        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            controller.PreviewAsset((AssetData)lbAssets.SelectedItem);
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            controller.UploadFile();
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            controller.DownloadFile((AssetData)lbAssets.SelectedItem);
        }
    }
}
