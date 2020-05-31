using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SOCKET_UDP;

namespace BlackJack_Client
{
    public class Net
    {
        private clsClientUDP _client;
        private clsServerUDP _server;
        private static int port;
        public int log_id;
        public Timer timerConn;

        public clsClientUDP Client { get => _client; set => _client = value; }
        public clsServerUDP Server { get => _server; set => _server = value; }

        public void Server_datiRicevutiEvent(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = new ObjMex(null, null);
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "login-failed":
                    if (Convert.ToBoolean(msg.Data[0]))
                        MessageBox.Show("Credenziali errate");
                    else
                        MessageBox.Show("Questo utente si trova già in partita");
                    break;
                case "lobby-full":
                    MessageBox.Show("Lobby al momento piena, riprova più tardi");
                    break;
                case "ping":
#if DEBUG 
                    Console.WriteLine("ping arrivato");
#endif
                    List<object> lst = new List<object>();
                    lst.Add(log_id);
                    _client.Invia(GeneraMessaggio("ping-response",lst));
                    break;
            }

            
        }

        public void EstablishConn(bool istanziaServer = true)
        {
            if (istanziaServer)
            {
                port = Net.GetPort();
                this.Server = new clsServerUDP(IPAddress.Parse(Net.GetLocalIPAddress()), port);
                this.Server.avvia();
                this.Server.datiRicevutiEvent += this.Server_datiRicevutiEvent;
            }
            List<object> lst = new List<object>();
            lst.Add(port);
            Client.Invia(GeneraMessaggio("new-conn", lst));
            timerConn = new Timer();
            timerConn.Interval = 5000;
            timerConn.Tick += TimerConn_Tick;
            timerConn.Start();
        }

        private void TimerConn_Tick(object sender, EventArgs e)
        {
            timerConn.Stop();
            EstablishConn(istanziaServer: false);
        }

        public static ClsMessaggio GeneraMessaggio(string action, List<object> data = null)
        {
            ClsMessaggio toSend = new ClsMessaggio();
            ObjMex objMex = new ObjMex(action, data);
            toSend.Messaggio = JsonConvert.SerializeObject(objMex);
            return toSend;
        }


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
