using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    class RegisterController : IRegisterController
    {
        RegisterController(IRegister_Window window)
        {
            this.window = window;
        }


        IRegister_Window window ;

        public string EMail => window.Email;

        public string Username => window.Username;

        public string Password1 => window.Password1;

        public string Password2 => window.Password2;

        public bool CheckForEmail()
        {
            return (EMail.Split('@').Length > 1 && EMail.Split('@').Length < 3);
        }

        public bool CheckForPasswordMatch()
        {
            return Password2 == Password1;
        }
    }
}
