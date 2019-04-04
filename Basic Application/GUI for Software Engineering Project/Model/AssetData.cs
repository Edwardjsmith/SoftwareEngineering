using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Model
{
    public enum AssetType
    {
        image,
        text,
        unknown
    }

    public class AssetData : IAssetData
    {

        private BitmapImage imgSource;
        readonly AssetType assetType;

        private string txtContent;
        private string projectName;
        public AssetData(string display_url,string file_name,string project_name)
        {
            
            ImgSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, display_url), UriKind.Absolute));
            
            TxtContent = file_name;
            projectName = project_name;
        }

        public string TxtContent { get => txtContent; set => txtContent = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public BitmapImage ImgSource { get => imgSource; set => imgSource = value; }

        public AssetType AssetType => assetType;
    }
}
