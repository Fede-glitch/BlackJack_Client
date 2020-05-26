﻿using System;
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
                        
                    foreach(PictureBox pcb in Controls["panel"+pos].Controls.OfType<PictureBox>())
                    {
                        pcb.Image = (pcb.Name == "pcbG" + pos + "C1") ? Image.FromFile(Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + @"\AppData\img\BlankCard.png") : null;
                            
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
                            int k = 0;
                            foreach (Card carta in posti[i].Carte)
                            {
                                k++;
                                PictureBox pcbCorrente = (Controls["panel" + pos].Controls["pcbG" + pos + "C" + k] as PictureBox);
                                BeginInvoke((MethodInvoker)delegate
                                {
                                    // Controls["panel" + pos].Controls["LblCarte" + pos].Text += carta.Seme.ToString() + carta.Numero + "\n";
                                    pcbCorrente.Image = Image.FromFile(Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + $@"\carte\{carta.Seme}{carta.Numero}.png");
                                    pcbCorrente.Visible = true;
                                    pcbCorrente.BringToFront();
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
                        //Controls["panel5"].Controls["LblDealer"].BackgroundImage = Image.FromFile(Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + @"\AppData\img\blu.png");
                    });
                    if((bool)msg.Data[1])   //TODO: nascondi prima carta
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
                case "draw":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Pareggio";
                    });
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

                                BeginInvoke((MethodInvoker)delegate
                                {
                                    Controls["panel" + pos].Controls["LblCarte" + pos].Text = "";
                                }); 
                                foreach (Card carta in posti[j].Carte)
                                {
                                    BeginInvoke((MethodInvoker)delegate
                                    {
                                        Controls["panel" + pos].Controls["LblCarte" + pos].Text += $"{carta.Seme}{carta.Numero}\n";
                                    });
                                }
                                break;
                            }
                        }
                    }
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
            //BtnCarta.Enabled = true;
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

        private void ingrassa(object sender, EventArgs e)
        {
            (sender as Button).FlatAppearance.BorderSize = 3;
        }

        private void dimagrisci(object sender, EventArgs e)
        {
            (sender as Button).FlatAppearance.BorderSize = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
