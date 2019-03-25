using System;
using System.Collections.Generic;
using System.Linq;
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
        ThreadStart Sender_Ref;
        Thread Sender_Thread;

        public bool Start(string ip, int port)
        {
            TcpClient client = new TcpClient(ip, port);
            if (client.Connected)
            {
                client.Close();
                IP = ip;
                Port = port;
                Running = true;
                Sender_Ref = new ThreadStart(Sender);
                Sender_Thread = new Thread(Sender_Ref);
                Sender_Thread.Start();
            }
            else
            {
                return false;
            }
            return true;
        }
        public void End()
        {
            Running = false;
            Sender_Thread.Abort();
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
            Send_Message(string.Join("R:",filename));
        }
        public void Request_Acsess()
        {
            Send_Message("A/");
        }
        public void Query_Acsess()
        {
            Send_Message("U/");
        }
        public void Update_file(string filename, string filedata)
        {
            // TODO
            //Send_Message("U/");
        }
        public void Send_Message(string message)
        {
            To_send.Add(message);
        }
        public List<string> Get_Messages()
        {
            return Messages;
        }
        private static bool Listen(TcpClient Client_Data)
        {
            try
            {
               
                NetworkStream stream = Client_Data.GetStream();
                byte[] message = new byte[Client_Data.ReceiveBufferSize];
                int bytesRead = stream.Read(message, 0, Client_Data.ReceiveBufferSize);
                string dataReceived = Encoding.ASCII.GetString(message, 0, bytesRead);
                Console.Write(dataReceived);
                Messages.Add(dataReceived);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private static void Sender()
        {
            
            while (Running)
            {
                int sent_message = 0;
                for (int i = 0; i < To_send.Count; i++)
                {
                    TcpClient Client_Data = new TcpClient(IP, Port);
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
                    }
                    Client_Data.Close();
                }
                if (sent_message == To_send.Count)
                {
                    To_send.Clear();
                }
               
            }
          
        }
    }
}
