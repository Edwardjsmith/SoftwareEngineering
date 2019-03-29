using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;

namespace GUI_for_Software_Engineering_Project
{
    class ProjectController : IProjectController
    {
        public IProject_Window view { get; }

        public List<IAssetData> AssetSource => throw new NotImplementedException();

        public ProjectController(IProject_Window view)
        {
            this.view = view;
        }

        public void PreviewAsset(IAssetData data)
        {
            new Preview_Window(data).Show();

        }
    }
}
