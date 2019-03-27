using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Model
{
    class ResourceManager : IResourceManager
    {
        BitmapImage lockedImage;
        BitmapImage pendingImage;
        BitmapImage acceptedImage;

        static ResourceManager instance;

        public static ResourceManager Instance { get
            {
                if (instance == null)
                    instance = new ResourceManager();
                return instance;
            }
        }

        public BitmapImage LockedImage { get => lockedImage; }
        public BitmapImage PendingImage { get => pendingImage; }
        public BitmapImage AcceptedImage { get => acceptedImage; }

        private ResourceManager()
        {
            lockedImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../lock.png"), UriKind.Absolute));

            pendingImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../email.png"), UriKind.Absolute));

            acceptedImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../checked.png"), UriKind.Absolute));
        }



    }
}
