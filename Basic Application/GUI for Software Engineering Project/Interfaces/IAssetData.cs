using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Interfaces
{
    public interface IAssetData
    {
        string TxtContent { get; set; }
        string ProjectName { get; set; }
        BitmapImage ImgSource { get; set; }
    }
}
