using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUI_for_Software_Engineering_Project.Interfaces
{
    interface IProjectSelection
    {
        ListBox lbProjects { get; }

        void Show();

        void Close();
    }
}
