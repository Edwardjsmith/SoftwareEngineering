using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PingPongServer
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener listener = new TcpListener(IPAddress.Any, 2302);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();

                byte[] message = new byte[client.ReceiveBufferSize];

                int bytesRead = stream.Read(message, 0, client.ReceiveBufferSize);

                string dataReceived = Encoding.ASCII.GetString(message, 0, bytesRead);

                Console.WriteLine(dataReceived);

                client.Close();
            }
                
        }
    }
}
