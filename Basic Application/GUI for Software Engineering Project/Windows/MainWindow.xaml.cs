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
using GUI_for_Software_Engineering_Project.Windows;

namespace GUI_for_Software_Engineering_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {

        LoginController controller;

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
            controller = new LoginController(this);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            controller.LoginPressed(Username, Password);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            controller.RegisterPressed();
        }

        private void txtbxPassword_Enter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Console.WriteLine("Logging in!");
        }

        
    }
}
