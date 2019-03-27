using GUI_for_Software_Engineering_Project.GUI;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Model;
using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project.Controller
{
    class ProjectSelectionController: IProjectSelectionController
    {

        public List<ProjectData> ProjectData = new List<ProjectData>();

        private readonly IProjectSelection window;

        public ProjectSelectionController(IProjectSelection window)
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

        public void OnProjectOpenApplied(IProjectData selected)
        {

            //TODO: Open this project
            new Project_Window().Show();
        }

  
     
    }
}
