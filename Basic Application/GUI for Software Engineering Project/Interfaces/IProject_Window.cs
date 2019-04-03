using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    interface IProject_Window
    {
        List<IAssetData> AssetSource { get; }

        void Show();

    }
}
