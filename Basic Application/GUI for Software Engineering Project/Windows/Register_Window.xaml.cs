using GUI_for_Software_Engineering_Project.GUI;
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

namespace GUI_for_Software_Engineering_Project
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register_Window : Window, IRegister_Window
    {
        public string Username { get => txtbxUsername.Text; }

        public string Email { get => txtbxEMail.Text; }

        public string Password1 { get => txtbxEMail.Text; }

        public string Password2 { get => txtbxEMail.Text; }

        public Register_Window()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Registered");
            IProject_Window window = new Project_Window();
            window.Show();
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Not Registered");
            this.Close();
        }
    }
}
