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

        BitmapImage imageImage;
        BitmapImage txtImage;
        BitmapImage unknownImage;

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







        private ResourceManager()
        {

            // Taken from https://www.flaticon.com/free-icon/lock_483408#term=lock&page=1&position=4
            lockedImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../lock.png"), UriKind.Absolute));

            // Taken from https://www.flaticon.com/free-icon/pending_1701869#term=pending&page=1&position=1
            pendingImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../email.png"), UriKind.Absolute));

            // Taken from https://www.flaticon.com/free-icon/checked_128384#term=accept&page=1&position=1
            acceptedImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../checked.png"), UriKind.Absolute));

            // Taken from https://www.flaticon.com/free-icon/picture_149092#term=image&page=1&position=2
            imageImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../image.png"), UriKind.Absolute));

            // Taken from https://www.flaticon.com/free-icon/document_1086563#term=text&page=1&position=5
            txtImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../text.png"), UriKind.Absolute));

            // Taken from https://www.flaticon.com/free-icon/unknown_1179247#term=unknown&page=1&position=20
            unknownImage = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../../unknown.png"), UriKind.Absolute));

        }



    }
}
