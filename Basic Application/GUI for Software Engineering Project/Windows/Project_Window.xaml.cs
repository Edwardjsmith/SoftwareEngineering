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
        public List<AssetData> assetData = new List<AssetData>();

        public List<AssetData> AssetSource
        {
            get => assetData;
        }

        public static List<string> ProjectNames
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



        public Project_Window()
        {
            InitializeComponent();
            controller = new ProjectController(this);
            FillUIWithDataForTesting();
            lbAssets.ItemsSource = AssetSource;
        }

        private void FillUIWithDataForTesting()
        {
            ProjectNames = Networking.Networking.instance.Get_Projects();
            assetData.Add(new AssetData());
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
