using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Interfaces
{
    interface IResourceManager
    {

        BitmapImage LockedImage { get; }
        BitmapImage PendingImage { get; }
        BitmapImage AcceptedImage { get; }

    }
}
