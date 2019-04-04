using GUI_for_Software_Engineering_Project.Interfaces;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Model
{
    public enum ProjectState
    {
        locked,
        applied,
        accepted
    }
    class ProjectData : IProjectData
    {

        ProjectState state;
        string name;
        string description;

        public ProjectState State
        {
            get => state;
            set => state = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Description
        {
            get
            {
                if (description == null)
                    description = "This is the description for Project: " + name + ". As such, if you open it, you will have access to the files in the project. that is of course if you have acces to the project. In case you have not, please apply to a project and wait for the owner to whitelist you.";
                return description;
            }

            set => description = value;
        }

        public BitmapImage ImgSource
        {
            get
            {
                switch (state)
                {
                    case ProjectState.accepted:
                        return ResourceManager.Instance.AcceptedImage;
                        
                    case ProjectState.applied:
                        return ResourceManager.Instance.PendingImage;
                        
                    case ProjectState.locked:
                        return ResourceManager.Instance.LockedImage;
                }
                return null;
            }
        }

        

        public ProjectData(string name, ProjectState state)
        {

            this.name = name;
            this.state = state;

        }

    }
}
