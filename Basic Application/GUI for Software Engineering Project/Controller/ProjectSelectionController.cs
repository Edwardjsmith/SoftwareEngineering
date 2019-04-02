using GUI_for_Software_Engineering_Project.GUI;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Model;
using System.Collections.Generic;

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
            foreach (string name in Networking.Networking.instance.Get_Projects())
            {
                ProjectData.Add(new ProjectData(name, ProjectState.accepted));
            }
            window.lbProjects.ItemsSource = ProjectData;
        }

        public void OnCreateProjectClicked()
        {

            new ProjectCreation_Window().ShowDialog();
            
        }

        public void OnProjectOpenApplied(IProjectData selected)
        {

            //TODO: Open this project
            new Project_Window(selected.Name).Show();
        }

  
     
    }
}
