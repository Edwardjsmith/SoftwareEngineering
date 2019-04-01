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

namespace GUI_for_Software_Engineering_Project
{
    /// <summary>
    /// Interaction logic for ProjectCreation_Window.xaml
    /// </summary>
    public partial class ProjectCreation_Window : Window, IProjectCreation_Window
    {
        IProjectCreationController controller;

        public ProjectCreation_Window()
        {
            InitializeComponent();

            controller = new ProjectCreationController();
        }
        

        public string Projectname { get => txtbxProjectname.Text; }

        public string Tags { get => txtbxTags.Text; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (controller.CheckForExistingProject(Projectname))
            {
                controller.CreateNewProject(Projectname);
                this.Close();
            }
                
            else
                lblErrorMessage.Content = "The project could not be created";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Project Creation Canceled");
            this.Close();
        }
    }
}
