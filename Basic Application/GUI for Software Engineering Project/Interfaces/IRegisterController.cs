using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    interface IRegisterController
    {


        string EMail { get; }
        string Username { get; }
        string Password1 { get; }
        string Password2 { get; }
        bool CheckForPasswordMatch();
        bool CheckForEmail();
        


    }
}
