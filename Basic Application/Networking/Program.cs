using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Networking
{
    class Program
    {
       static void Clear_messages(bool server)
        {
            Console.Clear();
            Console.WriteLine("________________________________________________");
            if(server)
                Console.WriteLine("_________________PROJECT_SERVER_________________");
            else
                Console.WriteLine("______________________USER______________________");
            Console.WriteLine("________________________________________________");
        }
        static void Main(string[] args)
        {
            // Client Code
            if (Console.ReadLine() == "C")
            {
                Clear_messages(false);
                while (true)
                {
                    Console.WriteLine("ENTER USER NAME : ");
                    string name = Console.ReadLine();

                    Console.WriteLine("ENTER PASSWORD  : ");
                    string pass = Console.ReadLine();
                    Clear_messages(false);
                    if (Networking.instance.Sign_In(name, pass))
                    {
                        break;
                    }
                }
                while (true)
                {
                    string user_input = Console.ReadLine();
                    if (user_input == "Quit")
                    {
                        break;
                    }
                    // gets a list of all the projects 
                    if (user_input == "_projects")
                    {
                        List<string> projects = Networking.instance.Get_Projects(); // returns a list of all project names 
                        for (int i = 0; i < projects.Count(); i++)
                        {
                            Console.WriteLine("ID " + i + " : " + projects[i]);
                        }
                    }
                    // gets a list of all the files 
                    if (user_input == "_files")
                    {
                        Console.WriteLine("enter ID"); // enter the id of the project that you want the file names from 
                        string id = Console.ReadLine();
                        if (id != "Q")
                        {
                            List<string> projects = Networking.instance.Get_Projects(); // returns a list of all project names 
                            string[] files = Networking.instance.Get_Files(projects[Convert.ToInt32(id)]);
                            for (int i = 0; i < files.Count(); i++)
                            {
                                Console.WriteLine("FILE " + i + " : " + files[i]);
                            }
                        }
                        else
                        {
                            Clear_messages(false);
                            Console.WriteLine("BACK");
                        }

                    }
                    // gets the data for a file
                    if (user_input == "_file")
                    {
                        Console.WriteLine("enter ID");
                        string id = Console.ReadLine();
                        if (id != "Q")
                        {
                            List<string> projects = Networking.instance.Get_Projects();
                           
                        }
                        else
                        {
                            Clear_messages(false);
                            Console.WriteLine("BACK");
                        }

                    }
                }
            }
            // Client END


                // SERVER CODE 
            if (Console.ReadLine() == "S")
            {
                Network_SQL _SQL = new Network_SQL();
                List<Network_Server> _Servers = new List<Network_Server>();
                Clear_messages(true);
                while (true)
                {
                    Console.WriteLine("ENTER USER NAME : ");
                    string name = Console.ReadLine();

                    Console.WriteLine("ENTER PASSWORD  : ");
                    string pass = Console.ReadLine();


                    if (_SQL.Connect_SQL(name, pass))
                    {
                        Clear_messages(true);
                        break;

                    }
                    Console.WriteLine("WRONG USERNAME OR PASSWORD");

                }
                Console.WriteLine(".....SIGN IN SUCCESSFUL....");

                List<Project> projects = new List<Project>();

                projects = _SQL.Get_My_Projects();
                for (int i = 0; i < projects.Count(); i++)
                {
                    Console.WriteLine("PROJECT FOUND : ");
                    Console.WriteLine(projects[i].Name);
                    Network_Server network_Server = new Network_Server();
                    network_Server.Start(25565, @"N:\GitKrack\software\SoftwareEngineering\Basic Application\Repos", projects[i].Name);
                    Console.WriteLine(" SERVER " + i + " RUNNING ");
                    _Servers.Add(network_Server);
                }
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input != "Quit")
                    {
                        if (input == "U")
                        {
                            Console.WriteLine("UPDATEING IP ADDRESS FOR ALL SERVERS ");
                            for (int i = 0; i < projects.Count(); i++)
                            {
                                _SQL.Update_Project_IP(projects[i].Name);
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < projects.Count(); i++)
                        {
                            Console.WriteLine("KILLING SERVER" + projects[i].Name);
                            _Servers[i].End();
                            Console.WriteLine(projects[i].Name + " SERVER DEAD ");
                        }
                        break;
                    }


                }
            }
            // SERVER END
        }

    /*             string externalip = new WebClient().DownloadString("http://icanhazip.com");
            Console.WriteLine("My Public IP Address is :" + externalip);
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            Console.WriteLine("My Host Name is :" + hostName);
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            Console.WriteLine("My Local IP Address is :" + myIP);
            if (Console.ReadLine() == "S")
            {
                Console.WriteLine("STARTING SERVER");
                Network_Server network_Server = new Network_Server();
                network_Server.Start(25565, @"N:\GitKrack\software\SoftwareEngineering\Basic Application\Repos");
                // Network_SQL _SQL = new Network_SQL();
                // _SQL.Connect_SQL("Test", "Pass");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input != "Quit")
                    {

                    }
                    else
                    {
                        network_Server.End();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("STARTING USER");
                Network_Client network_Client = new Network_Client();
                network_Client.Start("localhost", 25565);
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input != "Quit")
                    {
                        network_Client.Send_Message(input);
                    }
                    else
                    {
                        network_Client.End();
                        break;
                    }
                    Console.Clear();
                }

            }
        }*/
    }
}
