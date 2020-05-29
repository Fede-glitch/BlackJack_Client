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
using System.Threading;

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

        public FrmLobby(Net net, Player player, int posizione_tavolo, int log_id)
        {
            InitializeComponent();
            this.player = player;
            this.interfacciaRete = net;
            this.log_id = log_id;
            this.pos_tavolo = posizione_tavolo;
            this.dealer = new Place();
            this.Text = $"Lobby - {player.Username}";
            IstanziaPosti();
            interfacciaRete.Server.datiRicevutiEvent += Server_datiRicevutiEventLobby;
        }

        private void IstanziaPosti()
        {
            posti = new List<Place>(4);
            for (int i = 1; i <= 4; i++)
                posti.Add(new Place(i));
        }

        #region Eventi form
        private void FrmLobby_Load(object sender, EventArgs e)
        {
            interfacciaRete.Client.Invia(GeneraMessaggio("player-ready", null));
            posti[pos_tavolo].Fiches = 1000;
            LblRis.Text = "";
            LblMano.Text = "";
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
            List<object> lst = new List<object>();
            lst.Add(log_id);
            interfacciaRete.Client.Invia(GeneraMessaggio("player-stand", lst));
            BeginInvoke((MethodInvoker)delegate
            {
                BtnCarta.Enabled = false;
                BtnEsci.Enabled = false;
            });
        }
        #endregion

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
                            UpdatePlayerGraphics(pos, posti[i]);
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
                    });
                    if((bool)msg.Data[1])
                    {
                        foreach (Card carta in dealer.Carte)
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";
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
                case "blackjack":

                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = /*Convert.ToBoolean(msg.Data[0]) ? */"BlackJack";// : "21";
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
                    posti[pos_tavolo].Fiches = Convert.ToInt32(msg.Data[0]);
                    posti[pos_tavolo].Puntata = 0;
                    break;
                case "dealer-wins":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Hai perso";
                    });
                    posti[pos_tavolo].Fiches = Convert.ToInt32(msg.Data[0]);
                    posti[pos_tavolo].Puntata = 0;
                    break;
                case "draw":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Pareggio";
                    });
                    posti[pos_tavolo].Fiches = Convert.ToInt32(msg.Data[0]);
                    posti[pos_tavolo].Puntata = 0;
                    break;
                case "new-turn":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = "";
                        LblRis.Text = "";
                        LblDealer.Text = "";
                        LblCarte1.Text = "";
                        LblCarte2.Text = "";
                        LblCarte3.Text = "";
                        LblCarte4.Text = "";
                        TBPuntata.Enabled = true;
                        NumPuntata.Enabled = true;
                        BtnPuntata.Enabled = true;
                        TBPuntata.Maximum = posti[pos_tavolo].Fiches;
                        NumPuntata.Maximum = posti[pos_tavolo].Fiches;
                    });
                    break;
                case "update-graphics":
                    appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());
                    dealer.Carte = carte;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblDealer.Text = "";
                        foreach (Card carta in dealer.Carte)
                        {
                            LblDealer.Text += $"{carta.Seme}{carta.Numero}\n";
                        }
                    });
                    for (int i = 1; i < msg.Data.Count; i++)
                    {
                        appoggio = JsonConvert.DeserializeObject(msg.Data[i].ToString());
                        carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());
                        pos = Convert.ToInt32(appoggio.Posizione);
                        p = new Place(carte, pos);
                        for (int j = 0; j < posti.Count; j++)
                        {
                            if (posti[j].Posizione == p.Posizione)
                            {
                                posti[j] = p;

                                UpdatePlayerGraphics(pos, posti[j]);
                            }
                        }
                    }
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = "";
                        LblRis.Text = "";
                    });
                    break;
                case "update-graphics-dealer":
                    appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());
                    dealer.Carte = carte;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblDealer.Text = "";
                        foreach (Card carta in dealer.Carte)
                        {
                            LblDealer.Text += $"{carta.Seme}{carta.Numero}\n";
                        }
                    });
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = "";
                        LblRis.Text = "";
                    });
                    break;
                case "update-names":
                    for (int i = 0; i < msg.Data.Count; i+=2)
                    {
                        string username = msg.Data[i].ToString();
                        int posizione = Convert.ToInt32(msg.Data[i + 1]);
                        BeginInvoke((MethodInvoker)delegate
                        {
                            Controls["panel" + posizione].Controls["LblPlayer" + posizione].Text = username;
                        });
                    }
                    
                    break;
                case "player-leave":
                    pos = Convert.ToInt32(msg.Data[0]);

                    BeginInvoke((MethodInvoker)delegate
                    {
                        Controls["panel" + pos].Controls["LblPlayer" + pos].Text = "Giocatore " + pos;
                        Controls["panel" + pos].Controls["LblCarte" + pos].Text = "";
                    });
                    break;
                case "no-fiches":
                    interfacciaRete.Server.datiRicevutiEvent -= Server_datiRicevutiEventLobby;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("La partita è terminata", "Hai terminato le fiches");
                        Application.Exit();
                    });
                    break;
                    
            }
        }

        private void UpdatePlayerGraphics(int pos, Place place)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                Controls["panel" + pos].Controls["LblCarte" + pos].Text = "";
            });
            foreach (Card carta in place.Carte)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Controls["panel" + pos].Controls["LblCarte" + pos].Text += $"{carta.Seme}{carta.Numero}\n";
                });
            }
        }


        public ClsMessaggio GeneraMessaggio(string action, List<object> data)
        {
            ClsMessaggio toSend = new ClsMessaggio();
            ObjMex objMex = new ObjMex(action, data);
            toSend.Messaggio = JsonConvert.SerializeObject(objMex);
            return toSend;
        }

        private void TBPuntata_Scroll(object sender, EventArgs e)
        {
            NumPuntata.Value = TBPuntata.Value;
        }

        private void NumPuntata_ValueChanged(object sender, EventArgs e)
        {
            TBPuntata.Value = Convert.ToInt32(NumPuntata.Value);
        }

        private void BtnPuntata_Click(object sender, EventArgs e)
        {
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);
            lst.Add(TBPuntata.Value);
            interfacciaRete.Client.Invia(GeneraMessaggio("player-bet",lst));
            posti[pos_tavolo].Fiches -= TBPuntata.Value;
            posti[pos_tavolo].Puntata = TBPuntata.Value;
            TBPuntata.Enabled = false;
            NumPuntata.Enabled = false;
            BtnPuntata.Enabled = false;
        }
    }
}
