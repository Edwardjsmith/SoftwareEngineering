using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    class LoginController : ILoginController
    {
        private MainWindow mainWindow;

        public MainWindow MainWindow { get => mainWindow; set => mainWindow = value; }

        public string GetUserName()
        {
            return mainWindow.Username;
        }

        public string GetUserPassword()
        {
            return mainWindow.Password;
        }
    }
}
