using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_for_Software_Engineering_Project
{
    public interface ILoginController
    {
        string GetUserName();
        string GetUserPassword();
        void LoginPressed(string username, string password);
        void RegisterPressed();
    }
}
