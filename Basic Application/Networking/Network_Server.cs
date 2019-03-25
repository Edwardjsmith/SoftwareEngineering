﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
        public string my_name = "";
        ThreadStart Lister_Ref;
        Thread Lister_Thread;

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
            TcpListener listener = new TcpListener(IPAddress.Any, Server_Port);
            listener.Start();
            List<TcpClient> Clients = new List<TcpClient>();
            while (Running)
            {
                if (listener.Pending())
                {
                    TcpClient Client_Data = listener.AcceptTcpClient();
                   
                  
                    NetworkStream stream = Client_Data.GetStream();
                    
                    byte[] message = new byte[Client_Data.ReceiveBufferSize];

                    int bytesRead = stream.Read(message, 0, Client_Data.ReceiveBufferSize);

                    string dataReceived = Encoding.ASCII.GetString(message, 0, bytesRead);

                    Console.WriteLine(dataReceived);

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
                                //message = ASCIIEncoding.ASCII.GetBytes("Test message 1");//Get_File_raw(string.Join("", dataReceived, 2, dataReceived.Length)));
                                message = ASCIIEncoding.ASCII.GetBytes(Get_File_raw(string.Join("", dataReceived, 2, dataReceived.Length)));

                                Client_Data.GetStream().Write(message, 0, message.Length);
                            }
                            if (dataReceived[1] == ':')
                            {
                               // message = ASCIIEncoding.ASCII.GetBytes("test message 2");
                                message = ASCIIEncoding.ASCII.GetBytes(Get_File_name());
                                Client_Data.GetStream().Write(message, 0, message.Length);
                            }
                        }
                        

                       
                    }
                    Client_Data.Close();
                }
                //
            }
        }
        private static string Get_File_raw(string file_name)
        {
            //byte[] data = File.ReadAllBytes(File_Location + file_name);
            //return Convert.ToBase64String(data);
            return "";
        }
        private static string Get_File_name()
        {
            string[] fileArray = Directory.GetFiles(File_Location);
         
           
            for (int i = 0; i < fileArray.Count(); i++)
            {
                fileArray[i] = fileArray[i].Remove(0, File_Location.Count());
            }
            string files = string.Join("|",fileArray);
            return files;
        }
    }
}
