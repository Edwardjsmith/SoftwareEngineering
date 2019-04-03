using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    interface IProjectController
    {

        IProject_Window view { get; }

        void UploadFile();
        void LoadProject(IProjectData projectData);
        void DownloadFile(IAssetData data);
        void PreviewAsset(IAssetData data);

    }
}
