using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Basic_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onButtonClick(object sender, RoutedEventArgs e)
        {

            tblMessage.Text = tbMessage.Text;
            tbMessage.Clear();

            TcpClient client = new TcpClient("localhost", 2302);

            byte[] message = ASCIIEncoding.ASCII.GetBytes(tblMessage.Text);

            client.GetStream().Write(message, 0, message.Length);

            client.Close();
        }
    }
}
