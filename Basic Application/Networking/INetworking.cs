using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Networking
{
    public interface INetworking
    {

        // USER CRADENTRALS FUNCTIONS//

        // Sign in to sql server and return true or false if the users credentrals are found
        // All the users projects servers are restarted // returns true if successful
        bool Sign_In(string name, string password);
        // Adds a user to the server and signs in automaticly 
        bool Create_User(string name, string password);
        // Signs out the current user // will halt all server ativity 
        bool Sign_Out();
        // Returns true if the intance is signed in
        bool Is_Signed_In();

        // END END END END END //



        // FILE RECOVEROY FUNCTIONS //

        // Returns a vector of stings containing file name if the user has is on the projects white_list 
        string[] Get_Files(string project_name);
        // Returns true if was able to save the file to the local storage
        bool Get_File(string project_name, string file_name);
        // Returns the file from the server as a string   
        string Get_File_raw(string project_name, string file_name);
        // Returns the files meta data as a string 
        string Get_Metadata(string project_name, string file_name);

        // END END END END END //



        // PROJECT MANAGEMENT FUNTIONS //
        
        // Creats a new project on the sql server and hosts a server to tranfer files 
        bool Create_Project(string name, string file_location,int port);
        // Returns true if the project has successful been deleted 
        bool Delete_Project(string project_name);
        // Returns the names of all projects currently on the sql server
        List<string> Get_Projects();
        // Retruns true if the user can connect to the projects server
        bool Project_Live(string project_name);

        // END END END END END //


        
        // PROJECT ASSESS MANAGEMENT FUNTIONS // 

        // Sends a Request to assess a project returns true if the message was sent (not if the request was excepted)
        bool Request_Assess(string project_name);
        // Adds a user to the projects white list returns true if the user was added to the white list
        bool Allow_Assess(string project_name, string user_name);
        // Returns true if the user is on the projects white list 
        bool Has_Assess(string project_name);
        // Returns a list of strings that are requesting assess to a project#
        List<string> Get_Assess_Requests(string project_name);

        // END END END END END //


    }
}
