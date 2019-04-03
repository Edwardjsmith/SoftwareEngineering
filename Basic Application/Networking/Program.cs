using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Networking
{
    class Program
    {
        static void Clear_messages(bool server)
        {
            Console.Clear();
            Console.WriteLine("________________________________________________");
            if (server)
                Console.WriteLine("_________________PROJECT_SERVER_________________");
            else
                Console.WriteLine("______________________USER______________________");
            Console.WriteLine("________________________________________________");
        }
        static void Main(string[] args)
        {

            string mode = Console.ReadLine();
            // Client Code
            if(mode == "T")
            {
                ///////////////////////////////
                Console.WriteLine("");
                Console.WriteLine("TEST : LOGGIN");
                string name = "Test";//Console.ReadLine();
                string pass = "Pass";// Console.ReadLine();
                if (Networking.instance.Sign_In(name, pass))
                {
                    Console.WriteLine("PASS");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }

                ////////////////////////////
                Console.WriteLine("");
                Console.WriteLine("TEST : GET PROJECTS");
                List<string> projects = Networking.instance.Get_Projects(); // returns a list of all project names 
                for (int i = 0; i < projects.Count(); i++)
                {
                        Console.WriteLine("FILES FOUND " + i + " : " + projects[i]);
                }
                if(projects[0] == "test" && projects[1] == "Project super fun")
                {
                    Console.WriteLine("PASS");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }
                ///////////////////////////////
                ///
                Console.WriteLine("");
                Console.WriteLine("TEST : GET FILES");
                string[] files = Networking.instance.Get_Files(projects[0]);
                if(files[0] == "UI1.png")
                {
                    Console.WriteLine("PASS 1");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }
                if (files[0] == "UI2.png")
                {
                    Console.WriteLine("PASS 2");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }
                if (files[0] == "UI3.png")
                {
                    Console.WriteLine("PASS 3");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }
                if (files[0] == "UI4.png")
                {
                    Console.WriteLine("PASS 4");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }

                /////////////////////////////////////
                Console.WriteLine("");
                Console.WriteLine("TEST : UPLOAD FILE");
                Networking.instance.Send_File(projects[0], "test_tree.png", @"..\..\..\User_files");
                files = Networking.instance.Get_Files(projects[0]);
                if (files.Contains("test_tree.png"))
                {
                    Console.WriteLine("PASS");
                }
                else
                {
                    Console.WriteLine("FAIL");
                }
                string t = Console.ReadLine();
            }
            if (mode == "C")
            {
                Clear_messages(false);
                while (true)
                {
                    Console.WriteLine("ENTER USER NAME : ");
                    string name = "tester";//Console.ReadLine();

                    Console.WriteLine("ENTER PASSWORD  : ");
                    string pass = "tester";// Console.ReadLine();
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
                    if (user_input == "_join")
                    {
                        List<string> projects = Networking.instance.Get_Projects(); // returns a list of all project names 
                        for (int i = 0; i < projects.Count(); i++)
                        {
                            Console.WriteLine("ID " + i + " : " + projects[i]);
                        }
                        Console.WriteLine("Enter project ID");
                        string id = Console.ReadLine();
                        if (id != "Q")
                        {
                            Networking.instance.Request_Assess(projects[Convert.ToInt32(id)]);
                        }
                        else
                        {
                            Clear_messages(false);
                            Console.WriteLine("BACK");
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
                        Console.WriteLine("enter project ID");
                        string id = Console.ReadLine();
                        if (id != "Q")
                        {
                            List<string> projects = Networking.instance.Get_Projects(); // returns a list of all project names 

                            string[] files = Networking.instance.Get_Files(projects[Convert.ToInt32(id)]);
                            for (int i = 0; i < files.Count(); i++)
                            {
                                Console.WriteLine("FILE " + i + " : " + files[i]);
                            }
                            Console.WriteLine("enter file ID");
                            string file_id = Console.ReadLine();
                            if (id != "Q")
                            {

                                Networking.instance.Get_File(projects[Convert.ToInt32(id)], files[Convert.ToInt32(file_id)], @"..\..\..\User_files");

                            }
                            else
                            {
                                Clear_messages(false);
                                Console.WriteLine("BACK");
                            }
                        }
                        else
                        {
                            Clear_messages(false);
                            Console.WriteLine("BACK");

                        }


                    }
                    if (user_input == "u_file")
                    {
                        Console.WriteLine("enter project ID");
                        string id = Console.ReadLine();
                        if (id != "Q")
                        {
                            List<string> projects = Networking.instance.Get_Projects(); // returns a list of all project names 

                            string[] fileArray = Directory.GetFiles(@"..\..\..\User_files");


                            for (int i = 0; i < fileArray.Count(); i++)
                            {
                                fileArray[i] = fileArray[i].Remove(0, @"..\..\..\User_files".Count());
                            }
                            for (int i = 0; i < fileArray.Count(); i++)
                            {
                                Console.WriteLine("FILE " + i + " : " + fileArray[i]);
                            }
                            Console.WriteLine("enter file ID");
                            string file_id = Console.ReadLine();
                            if (id != "Q")
                            {

                                Networking.instance.Send_File(projects[Convert.ToInt32(id)], fileArray[Convert.ToInt32(file_id)], @"..\..\..\User_files");

                            }
                            else
                            {
                                Clear_messages(false);
                                Console.WriteLine("BACK");
                            }
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

            bool running = true;
            // SERVER CODE 
            if (mode == "S")
            {
                Network_SQL _SQL = new Network_SQL();
                List<Network_Server> _Servers = new List<Network_Server>();
                Clear_messages(true);
                while (true)
                {
                    Console.WriteLine("ENTER USER NAME : ");
                    string name = "Test";//Console.ReadLine();

                    Console.WriteLine("ENTER PASSWORD  : ");
                    string pass = "Pass"; //Console.ReadLine();


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
                    network_Server.Start(5000, @"..\..\..\Server_files", projects[i].Name);
                    Console.WriteLine(" SERVER " + i + " RUNNING ");
                    _Servers.Add(network_Server);
                }
                while (running)
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
                        if (input == "R")
                        {
                            for (int i = 0; i < _Servers.Count(); i++)
                            {
                                Console.WriteLine("Project " + i + " : " + _Servers[i].My_name());
                            }
                            Console.WriteLine("Enter project ID");
                            string id = Console.ReadLine();
                            if (id != "Q")
                            {
                                string[] requests = _Servers[Convert.ToInt32(id)].get_requests();
                                for (int i = 0; i < requests.Count(); i++)
                                {
                                    Console.WriteLine("USER  " + i + " : " + requests[i]);
                                }
                            }
                        }

                        if (input == "A")
                        {
                            for (int i = 0; i < _Servers.Count(); i++)
                            {
                                Console.WriteLine("Project " + i + " : " + _Servers[i].My_name());
                            }
                            Console.WriteLine("Enter project ID");
                            string id = Console.ReadLine();
                            if (id != "Q")
                            {
                                string[] requests = _Servers[Convert.ToInt32(id)].get_requests();
                                for (int i = 0; i < requests.Count(); i++)
                                {
                                    Console.WriteLine("USER  " + i + " : " + requests[i]);
                                }
                                Console.WriteLine("Enter User ID");
                                string user_id = Console.ReadLine();
                                if (user_id != "Q")
                                {
                                    _Servers[Convert.ToInt32(id)].Alow_asess(requests[Convert.ToInt32(user_id)]);
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
                    else {
                        running = false;

                    }
                    // SERVER END
                }

            }
        }
    }
}
