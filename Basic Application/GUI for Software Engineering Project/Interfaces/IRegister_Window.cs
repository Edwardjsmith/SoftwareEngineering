using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    interface IRegister_Window
    {

        string Username { get; }
        string Email { get; }
        string Password1 { get; }
        string Password2 { get; }
        string AccountType { get; }

        void Close();
    }
}
