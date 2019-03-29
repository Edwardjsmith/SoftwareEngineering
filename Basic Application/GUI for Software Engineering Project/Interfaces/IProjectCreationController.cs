using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    interface IProjectCreationController
    {

        void CreateNewProject(string projectName);
        bool CheckForExistingProject(string projectName);

    }
}
