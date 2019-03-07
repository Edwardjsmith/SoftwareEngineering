using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsStuff
{
    class User
    {
        string loginName;
        string password;
        string name;

        List<Projects> userProjects;

        public User(string login, string pass, string n)
        {
            loginName = login;
            password = pass;
            name = n;
        }

        public string getLogin()
        {
            return loginName;
        }

        public string getPassword()
        {
            return password;
        }
        public string getName()
        {
            return name;
        }
     
    }
            
    
}
