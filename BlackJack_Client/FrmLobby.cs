using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using SOCKET_UDP;
using Newtonsoft.Json;

namespace BlackJack_Client
{
    public partial class FrmLobby : Form
    {
        Player player;
        clsClientUDP client;
        clsServerUDP server;
        int log_id;

        public FrmLobby(clsClientUDP client, clsServerUDP server, Player player, int posizione_tavolo, int log_id)
        {
            InitializeComponent();
            this.player = player;
            this.client = client;
            this.server = server;
            this.log_id = log_id;
            server.datiRicevutiEvent += Server_datiRicevutiEvent;
        }

        private void Server_datiRicevutiEvent(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = new ObjMex(null, null);
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "new-cards":
                    Place p = msg.Data[0] as Place;
                    //TODO implementare parte grafica prima
                    break;
            }
        }
    }
}
