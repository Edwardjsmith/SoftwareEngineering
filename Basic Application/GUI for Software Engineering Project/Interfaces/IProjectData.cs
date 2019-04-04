using GUI_for_Software_Engineering_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Interfaces
{
    public interface IProjectData
    {
        ProjectState State { get; set; }
        string Name { get; set; }
        BitmapImage ImgSource { get; }



    }
}
