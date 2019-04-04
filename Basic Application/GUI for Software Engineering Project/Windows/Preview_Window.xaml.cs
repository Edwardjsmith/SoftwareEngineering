using GUI_for_Software_Engineering_Project.Controller;
using GUI_for_Software_Engineering_Project.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GUI_for_Software_Engineering_Project.Windows
{
    /// <summary>
    /// Interaction logic for Preview_Window.xaml
    /// </summary>
    public partial class Preview_Window : Window, IPreviewWindow
    {
        IPreviewController controller;
        string description;
        public Preview_Window(IAssetData asset, IProjectData project)
        {
            InitializeComponent();
            controller = new PreviewController(this, asset, project);
            
        }

        private static  BitmapImage imgPreview1;

        public BitmapImage ImgPreview
        {
            get => imgPreview1;
            set
            {
                imgPreview1 = value;
                
            }
        }

        public string Description { get => description; set => description = value; }

        private void PreviewClosed(object sender, System.EventArgs e)
        {
            controller.DeleteTempFile();
        }
    }
}
