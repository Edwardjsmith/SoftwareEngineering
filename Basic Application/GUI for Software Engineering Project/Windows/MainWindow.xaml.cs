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
using System.Windows.Navigation;
using System.Windows.Shapes;

using GUI_for_Software_Engineering_Project.GUI;

namespace GUI_for_Software_Engineering_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {

        public string Username
        {
            get => txtbxUsername.Text;
        }

        public string Password
        {
            get => txtbxPassword.Text;
        }

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Project_Window();
            window.Show();
            Console.WriteLine("Logging in!");
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Registering!");
            new Register_Window().Show();

        }

        private void txtbxPassword_Enter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Console.WriteLine("Logging in!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
