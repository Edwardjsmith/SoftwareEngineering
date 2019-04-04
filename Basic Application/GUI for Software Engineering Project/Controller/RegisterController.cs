using GUI_for_Software_Engineering_Project.Interfaces;
using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    class RegisterController : IRegisterController
    {
        public RegisterController(IRegister_Window window)
        {
            this.window = window;
        }


        IRegister_Window window ;

        public string EMail => window.Email;

        public string Username => window.Username;

        public string Password1 => window.Password1;

        public string Password2 => window.Password2;

        public string AccountType => window.AccountType;

        public bool CheckForEmail()
        {
            return (EMail.Split('@').Length > 1 && EMail.Split('@').Length < 3);
        }

        public bool CheckForPasswordMatch()
        {
            return Password2 == Password1;
        }

        public void ProcessRegistration(string name, string pw1, string pw2, string email)
        {

            if (Networking.Networking.instance.Create_User(AccountType[0] + Username, Password2))
            {
                Console.WriteLine("Registered");
                IProjectSelection window = new ProjectSelection();
                window.Show();
                this.window.Close();
                Notification.Notification.instance.showNotification("User " + Username + "has been registered");
            }
            else
            {
                Notification.Notification.instance.showNotification("User could not be registered");
                Console.WriteLine("User could not be created");
            }
        }

        public void CloseWindow()
        {
            Console.WriteLine("Not Registered");
            Notification.Notification.instance.showNotification("User not created");
            window.Close();
        }
    }
}
