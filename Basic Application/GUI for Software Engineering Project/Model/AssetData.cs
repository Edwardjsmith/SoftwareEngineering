using GUI_for_Software_Engineering_Project.Interfaces;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Model
{
    public enum AssetTypeEnum
    {
        image,
        text,
        unknown
    }

    public class AssetData : IAssetData
    {

        private BitmapImage imgSource;

        private string txtContent;
        private string projectName;
        AssetTypeEnum assetType;
        public AssetData(string display_url,string file_name,string project_name)
        {
            
            ImgSource = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, display_url), UriKind.Absolute));
            
            TxtContent = file_name;
            projectName = project_name;
        }

        public AssetData(string file_name, IProjectData project)
        {
            switch (file_name.Split('.')[1])
            {
                case ("png"):
                    AssetType = AssetTypeEnum.image;
                    ImgSource = ResourceManager.Instance.ImageImage;
                    break;
                case ("txt"):
                    AssetType = AssetTypeEnum.text;
                    ImgSource = ResourceManager.Instance.TxtImage;
                    break;
                default:
                    AssetType = AssetTypeEnum.unknown;
                    ImgSource = ResourceManager.Instance.UnknownImage;
                    break;

            }

            TxtContent = file_name;
            projectName = project.Name;
        }

        public string TxtContent { get => txtContent; set => txtContent = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public BitmapImage ImgSource { get => imgSource; set => imgSource = value; }

        public AssetTypeEnum AssetType { get => assetType; set => assetType = value; }

    }
}
