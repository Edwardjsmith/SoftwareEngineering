using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Model
{
    public class AssetData
    {

        private BitmapImage imgSource;

        private string txtContent;

        public AssetData()
        {
            
            ImgSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "../../image.png"), UriKind.Absolute));
            
            TxtContent = "TEST";
        }

        public string TxtContent { get => txtContent; set => txtContent = value; }
        public BitmapImage ImgSource { get => imgSource; set => imgSource = value; }
    }
}
