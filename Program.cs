using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Net.Sockets;

namespace MM_Telnet
{
    class Program
    {
        static void Main(string[] args) 
        {
            string IPAddr = "127.0.0.1";
            int Port = 4000;
            
            //List of commands to Haas mill/lathe
            List<string> Commands = new List<string> {"Q100", "Q102"};

            HaasConnection hc = new HaasConnection(IPAddr, Port);

            for (int i = 0; i < Commands.Count; i++)
            {
                if (hc.IsConnected)
                {
                    // send command to instrument
                    hc.TermWrite(Commands[i] + "\r\n");

                    // display instrument reply
                    string srvmsg = hc.TermRead();
                    //Remove the interactive prompt
                    if (srvmsg.EndsWith(">")) srvmsg = srvmsg.TrimEnd('>'); 
                    Console.Write(srvmsg);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press a key to exit.");

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
