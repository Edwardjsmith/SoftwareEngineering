using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

using System.Net;


namespace Networking
{
    class Network_Server
    {
        public struct Client_Message
        {
            public string Message;
            public TcpClient Client;
        }

        public static List<Client_Message> Message_Data = new List<Client_Message>();
        static bool Running = false;
        static int Server_Port;
        static string File_Location;
        static string my_name = "";
        static List<string> White_list = new List<string>();
        static List<string> Request_list = new List<string>();
        ThreadStart Lister_Ref;
        Thread Lister_Thread;
        public string My_name()
        {
            return my_name;
        } 
        public List<Client_Message> Get_messages()
        {
            return Message_Data;
        }
        public void Clear_messages()
        {
            Message_Data.Clear();
        }
        public bool Start(int port,string file_location,string name)
        {
            try
            {
                White_list = System.IO.File.ReadAllLines(file_location + @"..\..\Server_private/White_list_" + name + ".txt").ToList();
                Request_list = System.IO.File.ReadAllLines(file_location + @"..\..\Server_private/Request_list_" + name + ".txt").ToList();
            }
            catch 
            {
                System.IO.File.WriteAllLines(File_Location + @"..\..\Server_private/White_list_" + my_name + ".txt", White_list.ToArray());
                System.IO.File.WriteAllLines(File_Location + @"..\..\Server_private/Request_list_" + my_name + ".txt", Request_list.ToArray());
            }
            my_name = name;
            File_Location = file_location;
            Server_Port = port;
            Running = true;
            try
            {
                Lister_Ref = new ThreadStart(Listener);
                Lister_Thread = new Thread(Lister_Ref);
                Lister_Thread.Start();
            }catch(Exception e)
            {
                Console.Write(e);
                Running = false;
                return false;
            }
            return true;
        }
        public bool End()
        {
            System.IO.File.WriteAllLines(File_Location + @"..\..\Server_private/White_list_" + my_name + ".txt", White_list.ToArray());
            System.IO.File.WriteAllLines(File_Location + @"..\..\Server_private/Request_list_" + my_name + ".txt", Request_list.ToArray());
            try
            {
                Running = false;
                Lister_Thread.Abort();
                for (int i = 0; i < Message_Data.Count(); i++)
                {
                    Message_Data[i].Client.Close();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            }
            return true;
        }
        public static void Listener()
        {
            TcpListener listener = new TcpListener(IPAddress.Any,Server_Port);
            listener.Start();
            while (Running)
            {
                if (listener.Pending())
                {
                    TcpClient Client_Data = listener.AcceptTcpClient();
                    Client_Data.ReceiveBufferSize = Int32.MaxValue/10;
                    Client_Data.SendBufferSize = Int32.MaxValue/10;

                    NetworkStream stream = Client_Data.GetStream();
                    
                    byte[] message = new byte[Client_Data.ReceiveBufferSize];
                    byte[] message_in = new byte[Client_Data.ReceiveBufferSize];

                    int bytesRead = stream.Read(message_in, 0, Client_Data.ReceiveBufferSize);

                    string dataReceived = Encoding.ASCII.GetString(message_in, 0, bytesRead);
                    

                    // PREIX:                         Meaning:
                    // R:                           - filenames 
                    // R/[filename]                 - filedata 
                    // A/[username]                 - request acsess to 
                    // U/[username]                 - on whitelist? 
                    // S/[filename]/D/[filedata]    - sending a file to server for updating
                    if (dataReceived.Length > 1)
                    {
                        if (dataReceived[0] == 'R')
                        {
                            if (dataReceived[1] == '/')
                            {

                                // request file data
                                Console.WriteLine("File requested");
                                message = Get_File_raw(dataReceived[2], dataReceived.Remove(0, 3));
                                Console.WriteLine("File sending");
                                Client_Data.GetStream().Write(message, 0, message.Length);
                                Console.WriteLine("File sent");
                            }
                            if (dataReceived[1] == ':')
                            {
                               // request file names
                                message = Encoding.UTF8.GetBytes(Get_File_name(dataReceived[2]));
                                Client_Data.GetStream().Write(message, 0, message.Length);
                            }
                        }
                        if (dataReceived[0] == 'A')
                        {
                            if (dataReceived[1] == '/')
                            {
                                // requset acsess to project 
                                Request_list.Add(dataReceived.Remove(0, 2));
                                Console.WriteLine("user Request : " + dataReceived.Remove(0, 2));
                            }
                          
                        }
                        if (dataReceived[0] == 'F')
                        {
                            if (dataReceived[1] == '/')
                            {
                                // requset acsess to project 
                                string name = dataReceived.Remove(0, 2);
                                White_list.Add(name);
                                Request_list.Remove(name);
                                message = Encoding.UTF8.GetBytes("OK");
                                Client_Data.GetStream().Write(message, 0, message.Length);
                            }

                        }
                        if (dataReceived[0] == 'Z')
                        {
                            if (dataReceived[1] == '/')
                            {
                                // requset acsess to project 
                                string requests = string.Join("|",Request_list.ToArray());
                                message = Encoding.UTF8.GetBytes(requests);
                                Client_Data.GetStream().Write(message, 0, message.Length);

                            }
                        }
                        if (dataReceived[0] == 'U')
                        {
                            if (dataReceived[1] == '/')
                            {
                                bool on_white = false;
                                
                                // true false on white list 
                               for (int i = 0; i < White_list.Count(); i++)
                                {
                                    if(White_list[i] == dataReceived.Remove(0, 2))
                                    {
                                        on_white = true;
                                        break;
                                    }
                                }
                                if (!on_white)
                                {
                                    message = Encoding.UTF8.GetBytes("FALSE");
                                    Client_Data.GetStream().Write(message, 0, message.Length);
                                }
                                else
                                {
                                    message = Encoding.UTF8.GetBytes("TRUE");
                                    Client_Data.GetStream().Write(message, 0, message.Length);
                                }
                            }

                        }
                        if (dataReceived[0] == 'S' && dataReceived[1] == '/')
                        {
                            char user_type;
                            int name_end = 0;
                            for (int i = 0; i < dataReceived.Count(); i++)
                            {
                                if (dataReceived[i] == '/' && dataReceived[i + 1] == 'D' && dataReceived[i + 2] == '/')
                                {
                                    user_type = dataReceived[i + 3];
                                    name_end = i;
                                }
                            }
                            Console.WriteLine("file inbound");

                            string filename = dataReceived.Remove(name_end, dataReceived.Count() - name_end).Remove(0, 3);
                            List<byte> data = new List<byte>();
                            for(int i = 0; i < message_in.Count() - (name_end+4); i++)
                            {
                                data.Add(message_in[i + name_end+4]);
                            }
                            Console.WriteLine("data uploaded");

                           File.WriteAllBytes(File_Location + "/" + my_name + filename, data.ToArray());
                            Console.WriteLine("File saved : " + filename);
                        }
                        
                    }
                    
                    Client_Data.Close();
                }
                
            }
        }
        private static byte[] Get_File_raw(char user_type, string file_name)
        {
            byte[] data = File.ReadAllBytes(File_Location +"/" + my_name +"/"+ user_type  + file_name);
            return data;
        }
        private static string Get_File_name(char user_type)
        {
            string[] fileArray = Directory.GetFiles(File_Location + "/" + my_name + "/" + user_type);
         
           
            for (int i = 0; i < fileArray.Count(); i++)
            {
                fileArray[i] = fileArray[i].Remove(0, (File_Location + "/" + my_name + "/" + user_type ).Count());
            }
            string files = string.Join("|",fileArray);
            return files;
        }
        public string[] get_requests()
        {
            return Request_list.ToArray();
        }
        public void Alow_asess(string name)
        {
            White_list.Add(name);
            Request_list.Remove(name);
        }
    }
}
