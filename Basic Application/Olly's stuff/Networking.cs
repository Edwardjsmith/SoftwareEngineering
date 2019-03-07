using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Networking
{
    class Networking : INetworking
    {
        Network_SQL _SQL = new Network_SQL();
        List<Network_Server> Servers = new List<Network_Server>();
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


        // FILE RECOVEROY FUNCTIONS //
        public bool Get_File(string project_name, string file_name)
        {
            return true;
        }

        public bool Get_Files(string project_name)
        {
            return true;
        }

        public string Get_File_raw(string project_name, string file_name)
        {
            return " lalala ";
        }

        public string Get_Metadata(string project_name, string file_name)
        {
            return "lalala";
        }


        // PROJECT MANAGEMENT FUNTIONS //

         public bool Create_Project(string name, string file_location,int port)
        {
            // add project to sql data base
            _SQL.Create_Project(name, "password", port);
            // start server

           
            return true;
        }

        public bool Delete_Project(string project_name)
        {
            return true;
        }

        public List<string> Get_Projects()
        {
            throw new NotImplementedException();
        }

        public bool Allow_Assess(string project_name, string user_name)
        {
            return true;
        }
        public bool Project_Live(string project_name)
        {
            return true;
        }



        // PROJECT ASSESS MANAGEMENT FUNTIONS // 
        public List<string> Get_Assess_Requests(string project_name)
        {
            throw new NotImplementedException();
        }
        public bool Has_Assess(string project_name)
        {
            return true;
        }
        public bool Request_Assess(string project_name)
        {
            return true;
        }
    }
}
