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
using EdsStuff;
namespace GUI_for_Software_Engineering_Project.GUI
{
    /// <summary>
    /// Interaction logic for Project_Window.xaml
    /// </summary>
    public partial class Project_Window : Window, IProject_Window
    {
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
            FillUIWithDataForTesting();
            cbxProjectSelection.ItemsSource = ProjectNames;
            lbAssets.ItemsSource = AssetSource;
        }

        
        private void cbxProjectSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Server requests should be made when this event is fired

            Console.WriteLine(cbxProjectSelection.SelectedItem.ToString());
            if (cbxProjectSelection.SelectedItem.ToString() == "New Project")
            {
                Console.WriteLine("Creating new Project");
                new ProjectCreation_Window().Show();
            }

        }

        private void FillUIWithDataForTesting()
        {
            /*ProjectNames.Add("Your advertisement here!");
            ProjectNames.Add("For just £9999");
            ProjectNames.Add("Seriously the best investment you could ever consider!");
            ProjectNames.Add("New Project");*/
            ProjectNames = Networking.Networking.instance.Get_Projects();
            assetData.Add(new AssetData());

        }



        // TODO: Remove after VS Restart
        private void CbxProjectSelection_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {



        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {

            var tmp = lbAssets.SelectedItem;

            new Preview_Window((AssetData)tmp).Show();

        }
    }
}
