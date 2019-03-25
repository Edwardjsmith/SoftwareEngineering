using GUI_for_Software_Engineering_Project.GUI;
using GUI_for_Software_Engineering_Project.Model;
using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project.Controller
{
    class ProjectSelectionController
    {

        public List<ProjectData> ProjectData = new List<ProjectData>();

        private readonly ProjectSelection window;

        public ProjectSelectionController(ProjectSelection window)
        {
            this.window = window;
            SetProjectList();
        }

        public void SetProjectList()
        {
            ProjectData.Add(new ProjectData("Test 1", ProjectState.accepted));
            ProjectData.Add(new ProjectData("Test 2", ProjectState.applied));
            ProjectData.Add(new ProjectData("Test 3", ProjectState.locked));
            window.lbProjects.ItemsSource = ProjectData;
            
        }

        public void OnCreateProjectClicked()
        {

            new ProjectCreation_Window().ShowDialog();
            
        }

        public void OnProjectOpenApplied(ProjectData selected)
        {

            //TODO: Open this project
            new Project_Window().Show();
        }

    }
}
