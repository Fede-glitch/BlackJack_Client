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
using Newtonsoft.Json.Linq;

namespace BlackJack_Client
{
    public partial class FrmLobby : Form
    {
        Player player;
        int log_id;
        int pos_tavolo;
        List<Place> posti;
        Net interfacciaRete;
        public FrmLobby() { InitializeComponent(); }

        public FrmLobby(ref Net net, Player player, int posizione_tavolo, int log_id)
        {
            InitializeComponent();
            this.player = player;
            this.interfacciaRete = net;
            this.log_id = log_id;
            this.pos_tavolo = posizione_tavolo;
        }

        public void Assegnavariabili(ref Net net, Player player, int posizione_tavolo, int log_id)
        {
            this.player = player;
            this.interfacciaRete = net;
            this.log_id = log_id;
            this.pos_tavolo = posizione_tavolo;
            IstanziaPosti();
            interfacciaRete.Server.datiRicevutiEvent += Server_datiRicevutiEventLobby;
        }

        private void IstanziaPosti()
        {
            posti = new List<Place>(4);
            for (int i = 1; i <= 4; i++)
                posti.Add(new Place(i));
        }

        private void Server_datiRicevutiEventLobby(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = new ObjMex(null, null);
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "new-cards":
                    dynamic appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    List<Card> carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());
                    int pos = Convert.ToInt32(appoggio.Posizione);
                    Place p = new Place(carte, pos);

                    for (int i = 0; i < posti.Count; i++)
                    {
                        if(posti[0].Posizione == p.Posizione)
                        {
                            posti[0] = p;
                            break;
                        }
                    }
                    break;
                case "your-turn":
                    //TODO abilita bottoni

                    BeginInvoke((MethodInvoker)delegate
                    {
                        BtnCarta.Enabled = true;
                        BtnEsci.Enabled = true;
                    });
                    
                    break;
            }
        }

        public ClsMessaggio GeneraMessaggio(string action, List<object> data)
        {
            ClsMessaggio toSend = new ClsMessaggio();
            ObjMex objMex = new ObjMex(action, data);
            toSend.Messaggio = JsonConvert.SerializeObject(objMex);
            return toSend;
        }

        private void FrmLobby_Load(object sender, EventArgs e)
        {
            interfacciaRete.Client.Invia(GeneraMessaggio("player-ready", null));
        }
    }
}
