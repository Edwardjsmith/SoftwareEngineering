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
<<<<<<< HEAD
        {
            new Preview_Window(data).Show();

        }

        public void UploadFile()
=======
>>>>>>> EdsBranch
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            FileStream stream = File.Create(dialog.FileName);

            StreamReader reader = new StreamReader(stream);
        }

        public void DownloadFile(IAssetData data)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.ShowDialog();
            if (dialog.CheckPathExists)
            {
                
                BitmapEncoder encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(data.ImgSource));

                bool fileextension = (dialog.FileName.Split(".png".ToCharArray())).Length > 1;

                if (File.Exists( dialog.FileName + ((fileextension)? ".png" : "")))
                    File.Delete(dialog.FileName + ((fileextension) ? ".png" : ""));


                using( FileStream fileStream = new FileStream(dialog.FileName + ((fileextension) ? ".png" : ""), FileMode.CreateNew))
                {
                    encoder.Save(fileStream);
                }
            }
        }
    }
}
