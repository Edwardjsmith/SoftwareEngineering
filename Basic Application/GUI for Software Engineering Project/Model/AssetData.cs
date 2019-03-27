using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Model
{
    public class AssetData : IAssetData
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
