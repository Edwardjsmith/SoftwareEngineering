using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;

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
                Connected_to_server = true;
            }
            catch (Exception e)
            {
                // if the connection falses print out error message
                Console.WriteLine("Failed to connect to SQL server ... ");
                Console.WriteLine(e.ToString());
            }
        }
        public bool Connect_SQL(string username, string password)
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
                // Console.WriteLine("User IP   = " + reader[3].ToString()); //  name
                // self.IP = (reader[3].ToString());

                // Console.WriteLine("User Port = " + reader[4].ToString()); //  name
                // self.Port = Int32.Parse(reader[4].ToString());

                }
            
            if(!reader.HasRows)
            {
                Console.WriteLine("ERROR -- CONNECT_SQL - USERNAME AND PASSWORD COMBO NOT FOUND"); //  name

                return false;
            }
           // self.ID = (int) reader[0];
            //self.Name = (string)reader[0];
           // self.IP = (string)reader[0];
           // self.Port = (int)reader[0];
           
            return true;
        }


        public List<Project> Get_My_Projects()
        {
            List<Project> projects = new List<Project>();


            return projects;
        }
        public List<Project> Get_All_Projects()
        {
            List<Project> projects = new List<Project>();

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
            for (int i = 0; i < projects.Count; i++) {
                command = string.Format("SELECT * FROM `Users` WHERE (( `ID` = '{0}' ))", projects[i].Master_user_id); 
                myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_users = myCommand.ExecuteReader();
                reader_users.Read();
                //TODO (FIX)
                Console.WriteLine("User ip" + reader_users[3].ToString());
                string ip = reader_users[3].ToString();

                Project project = projects[i];
                project.IP = ip;
                projects.RemoveAt(i);
                projects.Insert(i, project);
                
                
                projects[i].IP.Insert(0, ip);
                //projects[i].IP = reader_users[3].ToString();
                reader_users.Close();
            }
            

              

            return projects;
        }
        public Project Get_Project(int id)
        {
            Project project = new Project();

            // get project data
            string command = string.Format("SELECT * FROM `Projects` WHERE (( `ID` = '{0}' )",id);
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
                 if (!reader_projects.IsClosed) reader_projects.Close();
                // get user data
                command = string.Format("SELECT * FROM `Users` WHERE (( `ID` = '{0}' )", user_id); ;
                myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_users = myCommand.ExecuteReader();
                if (!reader_users.HasRows)
                {
                    Console.WriteLine("ERROR -- GET_PROJECT - PROJECT USER NOT FOUND");
                }
                reader_projects.Read();
                project.IP = reader_users[3].ToString();
            }
           
            return project;
        }
        public Project Get_Project(string project_name)
        {
            Project project = new Project();

            // get project data
            string command = string.Format("SELECT * FROM `Projects` WHERE (( `Name` = '{0}' )", project_name);
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
                if (!reader_projects.IsClosed) reader_projects.Close();
                // get user data
                command = string.Format("SELECT * FROM `Users` WHERE (( `ID` = '{0}' )", user_id); ;
                myCommand = new MySqlCommand(command, SQL);
                MySqlDataReader reader_users = myCommand.ExecuteReader();
                if (!reader_users.HasRows)
                {
                    Console.WriteLine("ERROR -- GET_PROJECT - PROJECT USER NOT FOUND");
                }
                reader_projects.Read();
                project.IP = reader_users[3].ToString();
            }
           
            return project;
        }
        public bool Create_User(string username, string password)
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
            if(!reader.IsClosed)reader.Close();

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
            if(Connect_SQL(username, password))
            {
                return true;
            }
            Console.WriteLine("ERROR -- CREATE_USER FAILED - LOGIN FAILED");
            return false;
        }
        public bool Create_Project(string project_name, string password,int port)
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
            command = string.Format("INSERT INTO `Projects`(`Name`, `Password`, `User`, `Port`) VALUES ({0},{1},{2},{3})",project_name, password,self.ID,port);
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


    }
}
