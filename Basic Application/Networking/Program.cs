using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Networking
{
    class Program
    {
        static void Main(string[] args)
        {
            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            Console.WriteLine("My Public IP Address is :" + externalip);
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            Console.WriteLine("My Host Name is :" + hostName);
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            Console.WriteLine("My Local IP Address is :" + myIP);
            if (Console.ReadLine() == "S")
            {
                Console.WriteLine("STARTING SERVER");
                Network_Server network_Server = new Network_Server();
                network_Server.Start(2302, @"C:\Users\Public\Pictures\Sample Pictures");
                // Network_SQL _SQL = new Network_SQL();
                // _SQL.Connect_SQL("Test", "Pass");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input != "Quit")
                    {

                    }
                    else
                    {
                        network_Server.End();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("STARTING USER");
                Network_Client network_Client = new Network_Client();
                network_Client.Start("localhost", 2302);
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input != "Quit")
                    {
                        network_Client.Send_Message(input);
                    }
                    else
                    {
                        network_Client.End();
                        break;
                    }
                }

            }
        }
    }
}
