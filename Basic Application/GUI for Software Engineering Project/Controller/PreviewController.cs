using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Controller
{
    class PreviewController : IPreviewController
    {
        readonly Preview_Window window;
        public PreviewController(Preview_Window preview_Window, IAssetData asset, IProjectData project)
        {

            //window.imgPreview.Source = asset.ImgSource;
            window.lblPreview.Content = asset.TxtContent;

            SetAsset(asset);
        }

        void SetAsset(IAssetData asset)
        {

        }

    }
}
