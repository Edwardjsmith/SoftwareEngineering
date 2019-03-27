using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project.Interfaces
{
    interface IProjectSelectionController
    {

        void SetProjectList();

        void OnCreateProjectClicked();

        void OnProjectOpenApplied(IProjectData data);

    }
}
