using System;
using System.Collections.Generic;
using System.Linq;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;
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

            Notification.Notification.instance.showNotification(project_name + " successfully uploaded", " ");
            Networking.Networking.instance.Send_File(project_name, "\\" + tmpsplit[tmpsplit.Count()-1], path);

           
        }

        public void DownloadFile(IAssetData data)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.ShowDialog();

            if (Networking.Networking.instance.Get_File(data.ProjectName, data.TxtContent, dialog.SelectedPath))
            {
                Notification.Notification.instance.showNotification(data.ProjectName + " successfully downloaded", data.TxtContent);
            }
            else
            {
                Notification.Notification.instance.showNotification(data.ProjectName + " failed to download");
            } 
        }
    }
}
