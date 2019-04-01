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

        public AssetData(string image_url,string name)
        {
            
            ImgSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, image_url), UriKind.Absolute));
            
            TxtContent = name;
        }

        public string TxtContent { get => txtContent; set => txtContent = value; }
        public BitmapImage ImgSource { get => imgSource; set => imgSource = value; }
    }
}
