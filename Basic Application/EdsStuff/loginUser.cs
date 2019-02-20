using System;
using System.Collections.Generic;

namespace EdsStuff
{
    class loginUser
    {

        string[] fileData;

        loginUser()
        {
            users = new List<User>();
            fileData = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteText.txt");

            foreach(string line in fileData)
            {
                string[] data = line.Split(' ');

                users.Add(new User(data[0], data[1], data[2]));
            }
        }
        List<User> users;
        User currentUser;
        public void addUser(string login, string password, string name)
        {
            if(getUser(login, password) == null)
            {
                Console.WriteLine("User added!");
                users.Add(new User(login, password, name));
                System.IO.File.AppendAllText(@"c:\path\file.txt", login + ' ' + password + ' ' + name + Environment.NewLine);
            }
            else
            {
                Console.WriteLine("User exists!");
            }

        }



        public User getUser(string login, string password)
        {
            foreach(User user in users)
            {
                if(login == user.getLogin() && password == user.getPassword())
                {
                    return user;
                }
            }
            return null;
        }
    }
}
