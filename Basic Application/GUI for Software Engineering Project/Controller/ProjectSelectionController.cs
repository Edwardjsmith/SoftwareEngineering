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
            ProjectData.Clear();
            foreach (string name in Networking.Networking.instance.Get_Projects())
            {
                if (Networking.Networking.instance.Has_Assess(name))
                {
                    ProjectData.Add(new ProjectData(name, ProjectState.accepted));
                }
                else
                {
                    bool re = false;
                    string[] names = Networking.Networking.instance.Get_Assess_Requests(name);
                    for (int i = 0; i < names.Length; i++)
                    {
                        if(names[i] == Networking.Networking.instance.Get_username())
                        {
                            re = true;
                        }
                    }
                    if (re)
                    {
                        ProjectData.Add(new ProjectData(name, ProjectState.applied));
                    }
                    else
                    {
                        ProjectData.Add(new ProjectData(name, ProjectState.locked));
                    }
                }
            }
            window.lbProjects.ItemsSource = ProjectData;
            
        }

        public void OnCreateProjectClicked()
        {

            new ProjectCreation_Window().ShowDialog();
            
        }

        public void OnProjectOpenApplied(IProjectData selected)
        {
            if (selected.State == ProjectState.locked)
            {
                Networking.Networking.instance.Request_Assess(selected.Name);
                selected.State = ProjectState.applied;
                Notification.Notification.instance.showNotification("Assess Requested");

                SetProjectList();
            }
            if (selected.State == ProjectState.applied)
            {
                SetProjectList();
                Networking.Networking.instance.Request_Assess(selected.Name);
                Notification.Notification.instance.showNotification("Still waiting on Acceptence");
                

            }
            if (selected.State == ProjectState.applied)
            {
                Networking.Networking.instance.Request_Assess(selected.Name);
                Notification.Notification.instance.showNotification("Still waiting on Acceptence");

            }
            if (selected.State == ProjectState.accepted) 
            {
                new Project_Window(selected.Name).Show();
            }
        }

  
     
    }
}
