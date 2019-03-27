using GUI_for_Software_Engineering_Project.Controller;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Model;
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

namespace GUI_for_Software_Engineering_Project.Windows
{
    /// <summary>
    /// Interaction logic for ProjectSelection.xaml
    /// </summary>
    public partial class ProjectSelection : Window, IProjectSelection
    {

        IProjectSelectionController controller;
        public ProjectSelection()
        {
            InitializeComponent();
            controller = new ProjectSelectionController(this);
        }

        ListBox IProjectSelection.lbProjects => lbProjects;

        private void btnCreateClick(object sender, RoutedEventArgs e)
        {
            controller.OnCreateProjectClicked();
        }

        private void btnOpenClick(object sender, RoutedEventArgs e)
        {
            controller.OnProjectOpenApplied((ProjectData)lbProjects.SelectedItem);
        }
    }
}
