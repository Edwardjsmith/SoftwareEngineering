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
using GUI_for_Software_Engineering_Project.Model;

namespace GUI_for_Software_Engineering_Project
{
    class ProjectController : IProjectController
    {
        public IProject_Window view { get; }
        
        IProjectData projectData;

        public ProjectController(IProject_Window view, IProjectData projectData)
        {
            this.view = view;
            this.projectData = projectData;
            LoadProject(projectData.Name);
        }

        public void PreviewAsset(IAssetData assetData)
        {
            new Preview_Window(assetData, projectData).Show();

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

        private void LoadProject(string name)
        {
            List<string> fileNames = Networking.Networking.instance.Get_Files(name).ToList();
            for (int i = 0; i < fileNames.Count(); i++)
            {
                string thumbnail;
                switch (fileNames[i].Split('.')[1])
                {
                    case ("png"):
                        thumbnail = "picture.png";
                        break;
                    case ("txt"):
                        thumbnail = "text.png";
                        break;
                    default:
                        thumbnail = "unknown.png";
                        break;
                }

                view.AssetSource.Add(new AssetData(@"..\..\" + thumbnail, fileNames[i]));
            }
        }

        public void DownloadFile(IAssetData data)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
          
            dialog.ShowDialog();
        
            if(Networking.Networking.instance.Get_File(projectData.Name, data.TxtContent, dialog.SelectedPath))
            {
                Notification.Notification.instance.showNotification(projectData.Name+ " successfully downloaded", data.TxtContent, 1000000);
            }
            else
            {
                Notification.Notification.instance.showNotification(projectData.Name+ " successfully downloaded", " ", 1000000);
            } 

            Networking.Networking.instance.Get_File(projectData.Name, data.TxtContent, dialog.SelectedPath);
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
