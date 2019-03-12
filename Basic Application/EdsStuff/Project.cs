using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EdsStuff
{
    class Project : IProject
    {
        string fileAddress;
        string projectName;
        string password = null;

        Project(string name, string password, string fileLocation, int port)
        {
            setName(name);
            setPassword(password);

            Networking.Networking.instance.Create_Project(name, fileLocation, port);
        }

        public string getFileAddress()
        {
            return fileAddress;
        }
        
        public string getName()
        {
            return projectName;
        }

        public string getProjectPassword()
        {
            return password;
        }

        public void setName(string name)
        {
            projectName = name;
        }

        public void setPassword(string pass)
        {
            password = pass;
        }
    }
}
