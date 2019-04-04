using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Model;
using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Controller
{
    class PreviewController : IPreviewController
    {

        IPreviewWindow previewWindow;
        string name;
        public PreviewController(IPreviewWindow preview_Window, IAssetData data, IProjectData project)
        {
            this.previewWindow = preview_Window;

            if (Networking.Networking.instance.Get_File(project.Name, data.TxtContent, @"..\..\..\Temp"))
            {
                if (data.AssetType == AssetTypeEnum.image)
                {
                    string[] tmp = data.TxtContent.Split('/');
                    name = tmp[tmp.Length - 1];

                    BitmapImage temp = new BitmapImage();
                    Stream stream = File.OpenRead(@"..\..\..\Temp" + name);
                    temp.BeginInit();
                    temp.CacheOption = BitmapCacheOption.OnLoad;
                    temp.StreamSource = stream;
                    temp.EndInit();
                    stream.Close();
                    stream.Dispose();

                    ((Preview_Window)previewWindow).imgPreview.Source = temp;
                    ((Preview_Window)previewWindow).lblPreview.Content = data.TxtContent;

                }
                else if (data.AssetType == AssetTypeEnum.text) 
                {
                    string[] tmp = data.TxtContent.Split('/');
                    name = tmp[tmp.Length - 1];

                    string temp;
                    Stream stream = File.OpenRead(@"..\..\..\Temp" + name);
                    StreamReader reader = new StreamReader(stream);
                    temp = reader.ReadToEnd();
                    stream.Close();
                    stream.Dispose();

                    previewWindow.Description = temp;
                    ((Preview_Window)previewWindow).txblPreview.Text = temp;
                    ((Preview_Window)previewWindow).imgPreview.Visibility = System.Windows.Visibility.Hidden;
                    ((Preview_Window)previewWindow).lblPreview.Content = data.TxtContent;

                }
                else if (data.AssetType == AssetTypeEnum.unknown)
                {
                    string[] tmp = data.TxtContent.Split('/');
                    name = tmp[tmp.Length - 1];

                    ((Preview_Window)previewWindow).imgPreview.Source = ResourceManager.Instance.UnknownImage;
                    ((Preview_Window)previewWindow).lblPreview.Content = data.TxtContent;
                }

            }

            


        }


        public void DeleteTempFile()
        {
            string path = @"..\..\..\Temp" + name; //((BitmapImage)((Preview_Window)previewWindow).imgPreview.Source).UriSource.LocalPath;
            ((Preview_Window)previewWindow).imgPreview.Source = null;

            File.Delete(path);

        }

    }
}
