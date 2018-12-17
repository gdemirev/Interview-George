using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


namespace MM_Telnet
{
    class HaasConnection
    {
        TcpClient TermSock;
        public bool IsConnected
        {
            get { return TermSock.Connected; }
        }
        public HaasConnection(string Hostname, int Port)
        {
            TermSock = new TcpClient(Hostname, Port);
        }
        public void TermWrite(string cmd)
        {
            if (!TermSock.Connected) return;
            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd);
            TermSock.GetStream().Write(buf, 0, buf.Length);
        }
        public string TermRead()
        {
            int TimeOut = 100;
            if (!TermSock.Connected) return string.Empty;

            StringBuilder sb = new StringBuilder();
            do
            {
                while (TermSock.Available > 0)
                {
                    char input = (char)TermSock.GetStream().ReadByte();
                    if ((input == '\r') || (input == '\n') || (input >= ' ' && input <= '~'))
                        // only append printable ASCII
                        sb.Append((char)input);
                }
                System.Threading.Thread.Sleep(TimeOut);
            }
            while (TermSock.Available > 0);
            return sb.ToString();
        }
}


}