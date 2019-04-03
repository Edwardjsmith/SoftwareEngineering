using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
namespace Networking
{
    public class Networking : INetworking
    {
        private static Networking Instance = null;
        private static readonly object padlock = new object();


        private Networking()
        {

        }

        public static Networking instance
        {
            get
            {
                lock (padlock)
                {
                    if (Instance == null)
                    {
                        Instance = new Networking();
                    }

                    return Instance;
                }
            }
        }


        Network_SQL _SQL = new Network_SQL();
        ///List<Network_Server> Servers = new List<Network_Server>();
        Network_Client _Client = new Network_Client();
        // USER CRADENTRALS FUNCTIONS//
        public bool Sign_In(string name, string password)
        { 
            return _SQL.Connect_SQL(name,password);
        }
        public bool Create_User(string name, string password)
        {
            return _SQL.Create_User(name,password);
        }
        public bool Is_Signed_In()
        {
            return _SQL.Is_Signed();
        }
        public bool Sign_Out()
        {
            _SQL = new Network_SQL();
            return true;
        }
        public string Get_username()
        {
            return _SQL.Get_User().Name;
        }


        // FILE RECOVEROY FUNCTIONS //
        public bool Get_File(string project_name, string file_name, string save_location)
        {
            if (Has_Assess(project_name))
            {
                Project _temp = _SQL.Get_Project(project_name);
                _Client.Start(_temp.IP, _temp.Port,Get_username());
                _Client.Request_file(file_name);
                List<string> temp_return = _Client.Get_Messages();
                while (temp_return.Count() == 0)
                {
                    temp_return = _Client.Get_Messages();
                }
                _Client.End();
                byte[] data = _Client.Get_Messages(0);
                File.WriteAllBytes(save_location + file_name, data);
                return true;
            }
            return false;
        }
        public bool Send_File(string project_name, string file_name, string save_location)
        {
            if (Has_Assess(project_name))
            {
                Project _temp = _SQL.Get_Project(project_name);
                _Client.Start(_temp.IP, _temp.Port, Get_username());
                byte[] data = File.ReadAllBytes(save_location + file_name);
                _Client.Update_file(file_name, data);
                _Client.End();
                return true;
            }
            return false;
        }

        public string[] Get_Files(string project_name)
        {
            if (Has_Assess(project_name))
            {
                Project temp = _SQL.Get_Project(project_name);
                _Client.Start(temp.IP, temp.Port, Get_username());
                _Client.Request_filenames();
                List<string> temp_return = _Client.Get_Messages();
                while (temp_return.Count() == 0)
                {
                    temp_return = _Client.Get_Messages();
                }
                _Client.End();
                string[] split = temp_return[0].Split('|');
                return split;
            }
            string[] bate = { };
            return bate;
        }

        public byte[] Get_File_raw(string project_name, string file_name)
        {
            if (Has_Assess(project_name))
            {
                Project _temp = _SQL.Get_Project(project_name);
                _Client.Start(_temp.IP, _temp.Port, Get_username());
                _Client.Request_file(file_name);
                List<string> temp_return = _Client.Get_Messages();
                while (temp_return.Count() <= 1)
                {
                    temp_return = _Client.Get_Messages();
                }
                _Client.End();
                byte[] data = _Client.Get_Messages(1);
                return data;
            }
            byte[] bate = { };
            return bate;
        }

        public string Get_Metadata(string project_name, string file_name)
        {
            return "lalala";
        }


        // PROJECT MANAGEMENT FUNTIONS //

         public bool Create_Project(string name, string password,int port)
        {
            // add project to sql data base
            _SQL.Create_Project(name, password, port);
            // start server

           
            return true;
        }

       

        public List<string> Get_Projects()
        {
            List<string> names = new List<string>();
            List<Project> projects = _SQL.Get_All_Projects();
            for(int i = 0; i < projects.Count(); i++)
            {
                names.Add(projects[i].Name);
            }
            return names;
        }

        public bool Allow_Assess(string project_name, string user_name)
        {
            Project temp = _SQL.Get_Project(project_name);
            _Client.Start(temp.IP, temp.Port, Get_username());
            _Client.Alow_Acsess(user_name);

            List<string> temp_return = _Client.Get_Messages();
            while (temp_return.Count() == 0)
            {
                temp_return = _Client.Get_Messages();
            }
            _Client.End();
            return false;
        }
        public bool Project_Live(string project_name)
        {
            return true;
        }



        // PROJECT ASSESS MANAGEMENT FUNTIONS // 
        public string[] Get_Assess_Requests(string project_name)
        {
         
            string name = _SQL.Get_User().Name;
            List<string> temp_return = new List<string>();
            Project temp = _SQL.Get_Project(project_name);

            try
            {
                _Client.Start(temp.IP, temp.Port, Get_username());
                _Client.Request_Requests();
                while (temp_return.Count() == 0)
                {
                    temp_return = _Client.Get_Messages();
                }
                _Client.End();

               
            }
            catch { }
         
          if(temp_return.Count() == 0)
            {
                temp_return.Add("NONE");
            }

            return temp_return[0].Split('|');
        }
        public bool Has_Assess(string project_name)
        {
            try
            {
                string name = _SQL.Get_User().Name;
                Project temp = _SQL.Get_Project(project_name);
                if (_SQL.Get_User().ID == temp.Master_user_id)
                {
                    return true;
                }

                else
                {
                    _Client.Start(temp.IP, temp.Port, Get_username());
                    _Client.Query_Acsess(name);
                    List<string> temp_return = _Client.Get_Messages();
                    while (temp_return.Count() == 0)
                    {
                        temp_return = _Client.Get_Messages();
                    }
                    _Client.End();
                    if (temp_return[0] == "TRUE")
                    {
                        return true;

                    }
                }
            }
            catch { }
            return false;
        }
        public bool Request_Assess(string project_name)
        {
            string name = _SQL.Get_User().Name;
            Project temp = _SQL.Get_Project(project_name);
            _Client.Start(temp.IP, temp.Port, Get_username());
            _Client.Request_Acsess(name);

            List<string> temp_return = _Client.Get_Messages();

            while (temp_return.Count() == 0)
            {
                temp_return = _Client.Get_Messages();
            }
            _Client.End();

            return true;
            
        }

        public bool Delete_Project(string project_name)
        {
            throw new NotImplementedException();
        }
    }
}
