using GUI_for_Software_Engineering_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    class LoginController : ILoginController
    {
        private IMainWindow view;

        public LoginController(MainWindow window)
        {
            view = window;
        }

        public IMainWindow MainWindow { get => view; set => view = value; }

        public string GetUserName()
        {
            return view.Username;
        }

        public string GetUserPassword()
        {
            return view.Password;
        }

        public void LoginPressed(string username, string password)
        {
            
           

            if(Networking.Networking.instance.Sign_In(username, password))
            {
                new ProjectSelection().Show();
                Console.WriteLine("Logging in!");
                ((MainWindow)view).Close();
            }
            else
            {
                 Console.WriteLine("Invalid password!");
            }
        }

        public void RegisterPressed()
        {

            Console.WriteLine("Registering!");
            new Register_Window().Show();
        }
    }
}
