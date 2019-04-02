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

        public void UploadFile(IAssetData data)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.ShowDialog();

            Networking.Networking.instance.Send_File(data.ProjectName, data.TxtContent, dialog.SelectedPath);

           
        }

        public void DownloadFile(IAssetData data)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
          
            dialog.ShowDialog();
           
            if(Networking.Networking.instance.Get_File(data.ProjectName, data.TxtContent, dialog.SelectedPath))
            {
                Notification.Notification.instance.showNotification(data.ProjectName + " successfully downloaded", data.TxtContent, 1000000);
            }
            else
            {
                Notification.Notification.instance.showNotification(data.ProjectName + " successfully downloaded", " ", 1000000);
            } 
        }
    }
}
