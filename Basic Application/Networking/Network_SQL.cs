using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    struct Project
    {
        public int ID;
        public string Name;
        public int Master_user_id;
        public int Port;
        public string IP;
        public string Password;
    };
    struct User
    {
        public int ID;
        public string Name;
        public string IP;
        public int Port;
    };
    class Network_SQL
    {
        private User self = new User();
        
        private MySqlConnection SQL;
        private bool Connected_to_server = false;
        /*init code */
        public Network_SQL()
        {
            self.ID = -1; // set self id to -1 to repsent a unsigned in user 

            // define the sql server address and password 
            SQL = new MySqlConnection("Server= sql2.freemysqlhosting.net;" +
                                       "Database=sql2276785;" + "User=sql2276785;" +
                                       "Password=pC7*yS3*;");
            try
            {
                // attempt to login to the server 
                Console.WriteLine("Connecting to SQL server ... ");
                SQL.Open();
                Console.WriteLine("Connected to SQL server ... ");
                if (SQL.Ping())
                {
                    Connected_to_server = true;
                }
            }
            catch (Exception e)
            {
                // if the connection falses print out error message
                Console.WriteLine("Failed to connect to SQL server ... ");
                Console.WriteLine(e.ToString());
            }
            if (Connected_to_server)
            {
                string commanda = string.Format("SELECT * FROM `Users`");
                MySqlCommand myCommand = new MySqlCommand(commanda, SQL);
                MySqlDataReader reader = myCommand.ExecuteReader();
                List<string> user_data = new List<string>();
                
                
                while (reader.Read())
                {

                    user_data.Add(reader[0].ToString());
                    user_data.Add(reader[1].ToString());
                    user_data.Add(reader[2].ToString());
                    user_data.Add(reader[3].ToString());
                   

                }
                System.IO.File.WriteAllLines(@".\Users.txt", user_data);
                if (!reader.IsClosed) reader.Close();


                 commanda = string.Format("SELECT * FROM `Projects`");
                 myCommand = new MySqlCommand(commanda, SQL);
                 reader = myCommand.ExecuteReader();
                List<string> project_data = new List<string>();


                while (reader.Read())
                {

                    project_data.Add(reader[0].ToString());
                    project_data.Add(reader[1].ToString());
                    project_data.Add(reader[2].ToString());
                    project_data.Add(reader[3].ToString());


                }
                System.IO.File.WriteAllLines(@".\Projects.txt", project_data);
                if (!reader.IsClosed) reader.Close();


            }
        }
        public bool Connect_SQL(string username, string password)
        {
            if (Connected_to_server)
            {
                string commanda = string.Format("SELECT * FROM `Users` WHERE (( `Name` = '{0}' )AND( `Password` = '{1}'))", username, password);
                MySqlCommand myCommand = new MySqlCommand(commanda, SQL);
                MySqlDataReader reader = myCommand.ExecuteReader();
                string public_ip = new WebClient().DownloadString("http://icanhazip.com");

                while (reader.Read())
                {

                    Console.WriteLine("User id   = " + reader[0].ToString()); //  id
                    self.ID = Int32.Parse(reader[0].ToString());

                    Console.WriteLine("User Name = " + reader[1].ToString()); //  name
                    self.Name = (reader[1].ToString());
                    if (!reader.IsClosed) reader.Close();
                    commanda = string.Format("UPDATE `Users` SET `IP` = \"{0}\" WHERE `Users`.`ID` = '{1}'", public_ip, self.ID);
                    myCommand = new MySqlCommand(commanda, SQL);
                    myCommand.ExecuteNonQuery();
                    return true;

                }

                if (!reader.HasRows)
                {
                    Console.WriteLine("ERROR -- CONNECT_SQL - USERNAME AND PASSWORD COMBO NOT FOUND"); //  name
                    if (!reader.IsClosed) reader.Close();
                    return false;
                }
            }
            else
            {
                string[] user_data = System.IO.File.ReadAllLines(@".\Users.txt");
                int number_of_users = user_data.Count() / 4;
                for (int i = 0; i < number_of_users; i++)
                {
                    if (user_data[i*5 + 1] == username && user_data[i * 5 + 2] == password)
                    {
                        self.ID = Int32.Parse(user_data[0]);
                        self.Name = username;
                    }
                }


            }
          
           
            return true;
        }
        public void Update_Project_IP(string project_name)
        {
            if (Connected_to_server)
            {
                string public_ip = new WebClient().DownloadString("http://icanhazip.com");
                string commanda = string.Format("UPDATE `Projects` SET `IP` = \"{0}\" WHERE `Projects`.`Name` = '{1}'", public_ip.Remove(public_ip.Count()-1,1), project_name);
                MySqlCommand myCommand = new MySqlCommand(commanda, SQL);
                myCommand.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("ERROR -- CAN NOT UPDATE SQL SERVER -- FAILED TO CONNECT");
            }
        }

        public List<Project> Get_My_Projects()
        {
            List<Project> projects = new List<Project>();
            if (Connected_to_server)
            {
                string command = string.Format("SELECT * FROM `Projects` WHERE (( `User` = '{0}' ))", self.ID);
                MySqlCommand myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_projects = myCommand.ExecuteReader();
                if (!reader_projects.HasRows)
                {
                    Console.WriteLine("ERROR -- NO Projects found for this user");
                }
                else
                {
                    while (reader_projects.Read())
                    {
                        Project project = new Project();
                        int user_id = (int)reader_projects[3];
                        project.Port = (int)reader_projects[4];
                        project.Master_user_id = user_id;
                        project.Name = reader_projects[1].ToString();
                        project.Password = reader_projects[2].ToString();
                        project.IP = "localhost";//reader_projects[5].ToString();
                        projects.Add(project);
                    }

                }
                if (!reader_projects.IsClosed) reader_projects.Close();
            }
            else
            {
                string[] project_data = System.IO.File.ReadAllLines(@".\Projects.txt");
                int number_of_projects = project_data.Count() / 4;
                for (int i = 0; i < number_of_projects; i++)
                {
                    if (Int32.Parse(project_data[i *4 + 3]) == self.ID)
                    {
                        Project project = new Project();
                        int user_id = self.ID;
                        project.Port = 25525;
                        project.Master_user_id = user_id;
                        project.Name = project_data[i * 4 + 1];
                        project.Password = project_data[i * 4 + 2];
                        project.IP = "localhost";
                        projects.Add(project);
                    }
                }
            }
          
            return projects;
        }
        public List<Project> Get_All_Projects()
        {
            List<Project> projects = new List<Project>();
            if (Connected_to_server)
            {
                // get project data
                string command = "SELECT * FROM `Projects`";
                MySqlCommand myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_projects = myCommand.ExecuteReader();


                while (reader_projects.Read())
                {


                    Project project = new Project();
                    int user_id = (int)reader_projects[3];
                    project.Port = (int)reader_projects[4];
                    project.Master_user_id = user_id;
                    project.Name = reader_projects[1].ToString();
                    project.Password = reader_projects[2].ToString();
                    project.IP = "";
                    // get user data
                    projects.Add(project);


                }
                reader_projects.Close();
                for (int i = 0; i < projects.Count; i++)
                {
                    command = string.Format("SELECT * FROM `Users` WHERE (( `ID` = '{0}' ))", projects[i].Master_user_id);
                    myCommand = new MySqlCommand(command, SQL);
                    MySqlDataReader reader_users = myCommand.ExecuteReader();
                    reader_users.Read();
                    Console.WriteLine("User ip" + reader_users[3].ToString());
                    string ip = reader_users[3].ToString();

                    Project project = projects[i];
                    project.IP = ip;
                    projects.RemoveAt(i);
                    projects.Insert(i, project);


                    projects[i].IP.Insert(0, ip);

                    reader_users.Close();
                }
            }
            else
            {
                

                string[] project_data = System.IO.File.ReadAllLines(@".\Projects.txt");
                int number_of_projects = project_data.Count() / 4;
                for (int i = 0; i < number_of_projects; i++)
                {

                    Project project = new Project();
                    int user_id = self.ID;
                    project.Port = 25525;
                    project.Master_user_id = Int32.Parse(project_data[i * 4 + 3]);
                    project.Name = project_data[i * 4 + 1];
                    project.Password = project_data[i * 4 + 2];
                    string localIP;
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        localIP = endPoint.Address.ToString();
                    }
                    project.IP = localIP;
                   /* string[] user_data = System.IO.File.ReadAllLines(@".\Users.txt");
                    int number_of_users = user_data.Count() / 4;
                    for (int b = 0; b < number_of_users; b++)
                    {
                        if (Int32.Parse(user_data[b * 5 ]) == project.Master_user_id )
                        {
                            project.IP = "localhost";//user_data[b * 5 + 3];
                        }
                    }
                  
                    projects.Add(project);*/

                }
            }



            return projects;
        }
        public Project Get_Project(int id)
        {
            Project project = new Project();
            if (Connected_to_server)
            {
                // get project data
                string command = string.Format("SELECT * FROM `Projects` WHERE (( `ID` = '{0}' )", id);
                MySqlCommand myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_projects = myCommand.ExecuteReader();
                reader_projects.Read();
                if (!reader_projects.HasRows)
                {
                    Console.WriteLine("ERROR -- GET_PROJECT - ID NOT FOUND ON SQL SERVER - ID OUT OF RANGE OR PROJECT HAS BEEN REMOVED");
                }
                else
                {
                    int user_id = (int)reader_projects[3];
                    project.Port = (int)reader_projects[4];
                    project.Master_user_id = user_id;
                    project.Name = reader_projects[1].ToString();
                    project.Password = reader_projects[2].ToString();
                    project.IP = reader_projects[5].ToString();
                    if (!reader_projects.IsClosed) reader_projects.Close();
                }
            }
            else
            {
                List<Project> projects = Get_All_Projects();
                for (int i = 0; i < projects.Count(); i++)
                {
                    if (projects[i].ID == id)
                    {
                        ;
                        return projects[i];
                    }
                }
            }
           
            return project;
        }
        public Project Get_Project(string project_name)
        {
            Project project = new Project();
            if (Connected_to_server)
            {
                // get project data
                string command = string.Format("SELECT * FROM `Projects` WHERE (( `Name` = '{0}' ))", project_name);
                MySqlCommand myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_projects = myCommand.ExecuteReader();
                reader_projects.Read();
                if (!reader_projects.HasRows)
                {
                    Console.WriteLine("ERROR -- GET_PROJECT - NAME NOT FOUND ON SQL SERVER");
                }
                else
                {
                    int user_id = (int)reader_projects[3];
                    project.Port = (int)reader_projects[4];
                    project.Master_user_id = user_id;
                    project.Name = reader_projects[1].ToString();
                    project.Password = reader_projects[2].ToString();
                    project.IP = reader_projects[5].ToString();
                    if (!reader_projects.IsClosed) reader_projects.Close();
                }
            }
            else
            {
                List<Project> projects = Get_All_Projects();
                for (int i = 0; i < projects.Count(); i++)
                {
                    if (projects[i].Name == project_name)
                    {
                        
                        return projects[i];
                    }
                }
            }

            return project;
        }
        public bool Create_User(string username, string password)
        {
            if (Connected_to_server)
            {
                string command = string.Format("SELECT * FROM `Users` WHERE (( `Name` = '{0}' ))", username);
                MySqlCommand myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    // user name in use
                    Console.WriteLine("ERROR -- CREATE_USER FAILED - USERNAME IN USE");
                    Console.WriteLine("POSSIBLE SOLUTION -- CREATE_USER FAILED - SOLUTION :: LOGIN WITH USERNAME AND PASSWORD ...");
                    reader.Close();
                    if (Connect_SQL(username, password))
                    {
                        Console.WriteLine("SUCCESS -- CREATE_USER - USER LOGGED IN ...");
                        return true;
                    }
                    Console.WriteLine("ERROR -- CREATE_USER FAILED - POSSIBLE SOLUTION FAILED");
                    return false;

                }
                if (!reader.IsClosed) reader.Close();

                string public_ip = new WebClient().DownloadString("http://icanhazip.com");

                command = string.Format("INSERT INTO `Users`(`Name`, `Password`, `IP`) VALUES (\"{0}\",\"{1}\",\"{2}\")", username, password, public_ip);
                myCommand = new MySqlCommand(command, SQL);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    Console.WriteLine("ERROR -- CREATE_USER FAILED - COMMAND FAILED");
                    return false;
                }
                if (Connect_SQL(username, password))
                {
                    return true;
                }

                Console.WriteLine("ERROR -- CREATE_USER FAILED - LOGIN FAILED");
                return false;
            }
            Console.WriteLine("ERROR -- NO CONNECTION TO SQL SERVER --- Please use offline mode");
            return false;
        }
        public bool Create_Project(string project_name, string password,int port)
        {
            if (Connected_to_server)
            {
                string command = string.Format("SELECT * FROM `Projects` WHERE (( `Name` = '{0}' ))", project_name);
                MySqlCommand myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    // project name in use
                    Console.WriteLine("ERROR -- CREATE_PROJECT FAILED - PROJECT NAME IN USE");
                    return false;

                }
                command = string.Format("INSERT INTO `Projects`(`Name`, `Password`, `User`, `Port`) VALUES ({0},{1},{2},{3})", project_name, password, self.ID, port);
                myCommand = new MySqlCommand(command, SQL);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    Console.WriteLine("ERROR -- CREATE_PROJECT FAILED - COMMAND FAILED");
                    return false;
                }
                return true;
            }
            Console.WriteLine("ERROR -- NO CONNECTION TO SQL SERVER --- Please use offline mode");
            return false;
        }
        public bool Is_Signed()
        {
            if(self.ID != -1)
            {
                return true;
            }
            return false;
        }
        public bool Is_Connected()
        {
            return Connected_to_server;
        }
        public User Get_User()
        {
            return self;
        }

    }
}
