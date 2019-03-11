using GUI_for_Software_Engineering_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_for_Software_Engineering_Project.Windows
{
    /// <summary>
    /// Interaction logic for Preview_Window.xaml
    /// </summary>
    public partial class Preview_Window : Window
    {
        public Preview_Window(AssetData asset)
        {
            InitializeComponent();

            imgPreview.Source = asset.ImgSource;

            lblPreview.Content = asset.TxtContent;

        }
    }
}
