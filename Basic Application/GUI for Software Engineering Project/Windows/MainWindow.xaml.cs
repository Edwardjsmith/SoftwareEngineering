using System;
using System.Windows;
using System.Windows.Input;


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
