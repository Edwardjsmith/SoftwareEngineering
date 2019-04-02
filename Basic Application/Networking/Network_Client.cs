using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking
{
    class Network_Client
    {
        static List<string> To_send = new List<string>();
        static string IP;
        static int Port;
        static bool Running;
        static List<string> Messages = new List<string>();
        static List<byte[]> raw_bytes = new List<byte[]>();
        ThreadStart Sender_Ref;
        Thread Sender_Thread;

        public void Start(string ip, int port)
        {

                IP = ip;
                //Port = port;
                Port = 5000;
                Running = true;
                Sender_Ref = new ThreadStart(Sender);
                Sender_Thread = new Thread(Sender_Ref);
                Sender_Thread.Start();
         
        }
        public void End()
        {
            Running = false;
            Sender_Thread.Join();
        }
        // PREIX:                         Meaning:
        // R:                           - filenames 
        // R/[filename]                 - filedata 
        // A/[username]                 - request acsess to 
        // U/[username]                 - on whitelist? 
        // S/[filename]/D/[filedata]    - sending a file to server for updating
        public void Request_filenames()
        {
            Send_Message("R:");
        }
        public void Request_file(string filename)
        {
            Send_Message("R/"+filename);
        }
        public void Request_Acsess(string username)
        {
            Send_Message("A/" + username);
        }
        public void Request_Requests()
        {
            Send_Message("Z/");
        }
        public void Alow_Acsess(string username)
        {
            Send_Message("F/" + username);
        }
        public void Query_Acsess(string username)
        {
            Send_Message("U/" + username);
        }
        public void Update_file(string filename, byte[] filedata)
        {
            TcpClient Client_Data = new TcpClient(IP, Port);
            Client_Data.ReceiveBufferSize = Int16.MaxValue * 10;
            Client_Data.SendBufferSize = Int16.MaxValue * 10;
            string start = "S/" + filename + "/D/";
            List<byte> message = ASCIIEncoding.ASCII.GetBytes(start).ToList();
            message.AddRange(filedata);
            Client_Data.GetStream().Write(message.ToArray(), 0, message.ToArray().Length);
            Client_Data.Close();
        }
        public void Send_Message(string message)
        {
            To_send.Add(message);
        }
        public List<string> Get_Messages()
        {
            return Messages;
        }
        public byte[] Get_Messages(int id)
        {
            return raw_bytes[id];
        }
        private static bool Listen(TcpClient Client_Data)
        {
            try
            {
               
                NetworkStream stream = Client_Data.GetStream();
                byte[] message = new byte[Client_Data.ReceiveBufferSize];
                int bytesRead = stream.Read(message, 0, Client_Data.ReceiveBufferSize);
                string dataReceived = Encoding.ASCII.GetString(message, 0, bytesRead);
                ///Console.Write(dataReceived);
                Messages.Add(dataReceived);
                raw_bytes.Add(message);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private static void Sender()
        {
           
            TcpClient Client_Data = new TcpClient(IP, Port);
            Client_Data.ReceiveBufferSize = Int16.MaxValue * 10;
            Client_Data.SendBufferSize = Int16.MaxValue * 10;
            
            while (Running)
            {
                int sent_message = 0;
                for (int i = 0; i < To_send.Count; i++)
                {
                    
                    byte[] message = ASCIIEncoding.ASCII.GetBytes(To_send[i]);
                    Client_Data.GetStream().Write(message, 0, message.Length);

                    sent_message ++;

                    if (To_send[i][0] == 'R')
                    {
                        if (To_send[i][1] == ':')
                        {
                            // requesting file names
                            if (!Listen(Client_Data)) { i--; }

                        }
                        else
                        {
                            if (To_send[i][1] == '/')
                            {
                                // requesting file 
                                if (!Listen(Client_Data)) { i--; }
                            }
                        }
                    }
                    else
                    {
                        if (To_send[i][0] == 'U')
                        {
                            if (To_send[i][1] == '/')
                            {
                                // Asking if on white list
                                if (!Listen(Client_Data)) { i--; }
                            }
                        }
                        if (To_send[i][0] == 'Z')
                        {
                            if (To_send[i][1] == '/')
                            {
                                // asking for a list of acess requests 
                                if (!Listen(Client_Data)) { i--; }
                            }
                        }
                    }
                   
                }
                if (sent_message == To_send.Count)
                {
                    To_send.Clear();
                }
               
            }
            Client_Data.Close();
        }
    }
}
