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
    public class ResourceManager : IResourceManager
    {
        BitmapImage lockedImage;
        BitmapImage pendingImage;
        BitmapImage acceptedImage;

        BitmapImage imageImage;
        BitmapImage txtImage;
        BitmapImage unknownImage;
        BitmapImage noFiles;

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

        public BitmapImage ImageImage { get => imageImage; }
        public BitmapImage TxtImage { get => txtImage; }
        public BitmapImage UnknownImage { get => unknownImage; }
        public BitmapImage NoFiles { get => noFiles; }






        private ResourceManager()
        {

            lockedImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/lock.png"), UriKind.Absolute));

            pendingImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/pending.png"), UriKind.Absolute));

            acceptedImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/checked.png"), UriKind.Absolute));

            imageImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/image.png"), UriKind.Absolute));

            txtImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/text.png"), UriKind.Absolute));

            unknownImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/unknown.png"), UriKind.Absolute));

            noFiles = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../Assets/noFiles.jpg"), UriKind.Absolute));

        }



    }
}
