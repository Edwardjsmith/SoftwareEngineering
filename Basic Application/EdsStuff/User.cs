using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsStuff
{
    class User : IUser
    {
        private string username;
        private string password;
        private string[] userDetails;

        User(string name, string password, string[] userDetails)
        {
            setUserName(name);
            setPassword(password);
            setUserDetails(userDetails);

            if(Networking.Networking.instance.Create_User(name, password))
            {
                Console.WriteLine("User creation success");
            }
            else
            {
                Console.WriteLine("User creation failed");
            }
        }

        public string getPassword()
        {
            return password;
        }

        public string[] getUserDetails()
        {
            return userDetails;
        }

        public string getUserName()
        {
            return username;
        }

        public void setPassword(string pass)
        {
            password = pass;
        }

        public void setUserDetails(string[] details)
        {
            userDetails = details;
        }

        public void setUserName(string name)
        {
            username = name;
        }
    }
}
