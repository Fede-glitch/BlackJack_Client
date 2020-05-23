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
        Place dealer;
        Net interfacciaRete;
        public FrmLobby() { InitializeComponent(); }

        public FrmLobby(ref Net net, Player player, int posizione_tavolo, int log_id)
        {
            InitializeComponent();
            this.player = player;
            this.interfacciaRete = net;
            this.log_id = log_id;
            this.pos_tavolo = posizione_tavolo;
            this.dealer = new Place();
            IstanziaPosti();
            interfacciaRete.Server.datiRicevutiEvent += Server_datiRicevutiEventLobby;
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
                    if(pos == pos_tavolo)
                    {
                        (int, bool) mano = p.GetMano();
                        BeginInvoke((MethodInvoker)delegate
                        {
                            LblMano.Text = mano.Item2 ? "Blackjack":mano.Item1>21? "Hai Sballato":mano.Item1.ToString();
                        });
                    }
                        
                    for (int i = 0; i < posti.Count; i++)
                    {
                        if(posti[i].Posizione == p.Posizione)
                        {
                            posti[i] = p;
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel" + pos].Controls["LblCarte" + pos].Text = "";
                            });
                            foreach (Card carta in posti[i].Carte)
                            {
                                BeginInvoke((MethodInvoker)delegate
                                {
                                    Controls["panel" + pos].Controls["LblCarte" + pos].Text += carta.Seme.ToString() + carta.Numero + "\n";
                                    Application.DoEvents();
                                });
                            }
                            break;
                        }
                    }
                    
                    break;
                case "new-cards-dealer":
                    appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());
                    dealer.Carte = carte;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        Controls["panel5"].Controls["LblDealer"].Text = "";
                        Application.DoEvents();
                    });
                    if((bool)msg.Data[1])   //TODO: nascondi prima carta
                    {
                        foreach (Card carta in dealer.Carte)
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";
                                Application.DoEvents();
                            });
                        }
                    }
                    else
                    {
                        foreach (Card carta in dealer.Carte)
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";
                                Application.DoEvents();
                            });
                        }
                    }

                    break;
                case "your-turn":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        BtnCarta.Enabled = true;
                        BtnEsci.Enabled = true;
                    });
                    break;
                case "hand-twentyone-first":

                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = Convert.ToBoolean(msg.Data[0]) ? "BlackJack" : "21";
                    });
                    
                    break;
                case "hand-twentyone":
                case "hand-bust":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        BtnCarta.Enabled = false;
                        BtnEsci.Enabled = false;
                    });
                    break;
                case "unveil-card":
                    //TODO: mostrare carta coperta dealer
                    break;
                case "player-wins":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Hai vinto";
                    });
                    break;
                case "dealer-wins":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Hai perso";
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

        private void BtnCarta_Click(object sender, EventArgs e)
        {
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);
            lst.Add(log_id);
            interfacciaRete.Client.Invia(GeneraMessaggio("player-hit", lst));
        }

        private void BtnEsci_Click(object sender, EventArgs e)
        {
            interfacciaRete.Client.Invia(GeneraMessaggio("player-stand", null));
            BeginInvoke((MethodInvoker)delegate
            {
                BtnCarta.Enabled = false;
                BtnEsci.Enabled = false;
            });
        }
    }
}
