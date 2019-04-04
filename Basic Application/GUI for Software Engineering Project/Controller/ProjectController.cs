using System;
using System.Collections.Generic;
using System.Linq;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using GUI_for_Software_Engineering_Project.Model;

namespace GUI_for_Software_Engineering_Project
{
    class ProjectController : IProjectController
    {
        public IProject_Window view { get; }

        IProjectData projectData;

        public ProjectController(IProject_Window view, IProjectData project)
        {
            this.view = view;
            projectData = project;
            LoadProject(project);
        }

        public void LoadProject(IProjectData projectData)
        {
            List<string> FileNames = Networking.Networking.instance.Get_Files(projectData.Name).ToList();
            if (FileNames[0] != "") 
            {
                for (int i = 0; i < FileNames.Count(); i++)
                {
                    string thumbnail;
                    switch (FileNames[i].Split('.')[1])
                    {
                        case ("png"):
                            thumbnail = "image.png";
                            break;
                        case ("txt"):
                            thumbnail = "text.png";
                            break;
                        default:
                            thumbnail = "unknown.png";
                            break;

                    }
                    view.AssetSource.Add(new AssetData(@"..\..\..\Assets\" + thumbnail, FileNames[i], projectData.Name));
                }
            }
            else
            {
                view.AssetSource.Add(new AssetData(@"..\..\..\Assets\noFiles.jpg", " NO FILES ", projectData.Name));
            }
        }


        public void PreviewAsset(IAssetData data)

        {
            Networking.Networking.instance.Get_File(data.ProjectName, data.TxtContent, @"..\..\..\Temp");
            new Preview_Window(data, projectData).Show();

        }

        public void UploadFile()
        {

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();
            Console.WriteLine(dialog.FileName);
            string[] tmpsplit = dialog.FileName.Split('\\');
            string path = "";
            for (int i = 0; i < tmpsplit.Length - 1; i++)
                path += tmpsplit[i] + "\\";


            Notification.Notification.instance.showNotification(projectData.Name + " successfully uploaded", " ");
            Networking.Networking.instance.Send_File(projectData.Name, "\\" + tmpsplit[tmpsplit.Count()-1], path);


           
        }

        public void DownloadFile(IAssetData data)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.ShowDialog();
            if (data.TxtContent != "NO FILES")
            {
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
}
