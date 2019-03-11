using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsStuff
{
    interface IUser
    {
        string getUserName();
        string getPassword();
        string[] getUserDetails();

        void setUserName(string name);
        void setPassword(string password);
        void setUserDetails(string[] details);
    }
}
