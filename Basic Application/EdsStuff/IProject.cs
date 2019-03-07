using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsStuff
{
    interface IProject
    {
        string getName();
        string getFileAddress();
        string getProjectPassword();

        void setName(string name);
        void setPassword(string pass);
    }
}
