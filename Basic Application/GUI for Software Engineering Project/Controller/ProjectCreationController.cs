
namespace GUI_for_Software_Engineering_Project
{
    class ProjectCreationController : IProjectCreationController
    {
        public bool CheckForExistingProject(string projectName)
        {
            foreach(string name in Networking.Networking.instance.Get_Projects())
            {
                if(name == projectName)
                {
                    return false;
                }
            }

            return true;
        }

        public void CreateNewProject(string projectName)
        {
            //Networking.Networking.instance.Create_Project(projectName, la la la, lalala);
        }
    }
}
