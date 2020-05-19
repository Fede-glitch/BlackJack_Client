using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_Client
{
    public static class NetUtilities
    {
        public static string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Nessuna interfaccia di rete disponibile su questo computer");
        }

        public static int GetPort()
        {
            int port = 7778;
            while (isPortOpen(port))
                port++;
            return port;
        }

        private static bool isPortOpen(int port) => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners().Any(p => p.Port == port);
    }
}
