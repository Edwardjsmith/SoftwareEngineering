using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;
using Microsoft.Win32;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace GUI_for_Software_Engineering_Project
{
    class ProjectController : IProjectController
    {
        public IProject_Window view { get; }

        public List<IAssetData> AssetSource => throw new NotImplementedException();

        public ProjectController(IProject_Window view)
        {
            this.view = view;
        }

        public void PreviewAsset(IAssetData data)

        {
            new Preview_Window(data).Show();

        }

        public void UploadFile(string project_name)
        {

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();
            Console.WriteLine(dialog.FileName);
            string[] tmpsplit = dialog.FileName.Split('\\');
            string path = "";
            for (int i = 0; i < tmpsplit.Length - 1; i++)
                path += tmpsplit[i] + "\\";
            Networking.Networking.instance.Send_File(project_name, "\\" + tmpsplit[tmpsplit.Count()-1], path);

           
        }

        public void DownloadFile(IAssetData data)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
          
            dialog.ShowDialog();
            Networking.Networking.instance.Get_File(data.ProjectName, data.TxtContent, dialog.SelectedPath);
                /*BitmapEncoder encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(data.ImgSource));

                bool fileextension = (dialog.FileName.Split(".png".ToCharArray())).Length > 1;

                if (File.Exists( dialog.FileName + ((fileextension)? ".png" : "")))
                    File.Delete(dialog.FileName + ((fileextension) ? ".png" : ""));


                using( FileStream fileStream = new FileStream(dialog.FileName + ((fileextension) ? ".png" : ""), FileMode.CreateNew))
                {
                    encoder.Save(fileStream);
                }*/
            
        }
    }
}
