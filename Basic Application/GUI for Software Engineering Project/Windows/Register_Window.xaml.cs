using GUI_for_Software_Engineering_Project.GUI;
using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;
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

        public string AccountType => (string)cmbxAccountTypes.SelectedValue.ToString();

        public IRegisterController controller;

        public Register_Window()
        {
            InitializeComponent();
            controller = new RegisterController(this);
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {

            controller.ProcessRegistration(Username, Password1, Password2, Email);

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            controller.CloseWindow();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
