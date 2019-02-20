using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsStuff
{
    class Projects
    {
        string projectname;

        Projects(string name)
        {
            projectname = name;
        }

        public string getName()
        {
            return projectname;
        }
    }
}
