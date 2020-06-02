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
        Place myPlace;
        Net interfacciaRete;
        Thread prova;
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
            prova = new Thread(aggiornaFiches);
        }

        private void dimagrisci(object sender, EventArgs e)
        {
            //
        }
        private void ingrassa(object sender, EventArgs e)
        {
            //
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Fratm Sicuro di voler Uscire?","Sicuro?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
                this.Close();
            //TODO: Implementare roba che torna al login cuando esko
        }
        private void aggiornaFiches()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(myPlace.Fiches);
            }
        }

        private void IstanziaPosti()
        {
            posti = new List<Place>(4);
            for (int i = 1; i <= 4; i++)
                posti.Add(new Place(i));
            myPlace = posti[pos_tavolo-1];
        }

        #region Eventi form
        private void FrmLobby_Load(object sender, EventArgs e)
        {
            interfacciaRete.Client.Invia(GeneraMessaggio("player-ready", null));
            myPlace.Fiches = 1000;
            LblRis.Text = "";
            LblMano.Text = "";
            lblMyFiches.Text = "Le tue Fiches: 1000";
            lblMyPuntata.Text = "Devi ancora puntare.";
            prova.Start();
        }

        private void BtnCarta_Click(object sender, EventArgs e)
        {
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);
            lst.Add(log_id);
            interfacciaRete.Client.Invia(GeneraMessaggio("player-hit", lst));
            BeginInvoke((MethodInvoker)delegate
            {
                BtnDouble.Enabled = false;
            });
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
                BtnDouble.Enabled = false;
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
                            LblMano.Text = mano.Item2 ? "BJ":mano.Item1>21? "Hai Sballato":mano.Item1.ToString();
                        });
                    }
                    for (int i = 0; i < posti.Count; i++)
                    {
                        if(posti[i].Posizione == p.Posizione)
                        {
                            posti[i] = p;
                            UpdatePlayerGraphics(pos, posti[i]);
                            Application.DoEvents();
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
                    foreach (PictureBox pcb in Controls["panel5"].Controls.OfType<PictureBox>())
                    {
                        pcb.Image = (pcb.Name == "pcbBkC1") ? Image.FromFile(GetImage("BlankCard.png", false)) : null;

                    }
                    if ((bool)msg.Data[1])
                    {
                        /*
                        foreach (Card carta in dealer.Carte)
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";
                            });
                        }*/
                        PictureBox pcbCartaCoperta = (Controls["panel5"].Controls["pcbBKC1"] as PictureBox);
                        PictureBox pcbCartaScoperta = (Controls["panel5"].Controls["pcbBKC2"] as PictureBox);
                        BeginInvoke((MethodInvoker)delegate
                        {
                            pcbCartaCoperta.Image = Image.FromFile(GetImage("back.png", true));
                            pcbCartaScoperta.Image = Image.FromFile(GetImage(dealer.Carte[1].Seme.ToString() + "" + dealer.Carte[1].Numero.ToString() + ".png", true));
                            pcbCartaCoperta.Visible = true;
                            pcbCartaScoperta.Visible = true;
                            pcbCartaCoperta.BringToFront();
                            pcbCartaScoperta.BringToFront();
                            Thread.Sleep(20);
                            Application.DoEvents();
                        });
                    }
                    else
                    {
                        int k = 0;
                        foreach (Card carta in dealer.Carte)
                        {
                            k++;
                            PictureBox current = panel5.Controls["pcbBkC" + k] as PictureBox;
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";
                                current.Image = Image.FromFile(GetImage(carta.Seme.ToString() + carta.Numero + ".png", true));
                                Console.WriteLine(GetImage(carta.Seme.ToString() + carta.Numero + ".png", true));
                                current.Visible = true;
                                current.BringToFront();
                            });
                            Application.DoEvents();
                        }
                    }
                    break;
                case "your-turn":
                    BeginInvoke((MethodInvoker)delegate
                    {
                        BtnCarta.Enabled = true;
                        BtnEsci.Enabled = true;
                        if(myPlace.Fiches - myPlace.Puntata >0)
                            BtnDouble.Enabled = true;
                    });
                    break;
                case "blackjack":

                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = "BJ";
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
                    PictureBox pcbCorrenteElettrica = (Controls["panel5"].Controls["pcbBKC1"] as PictureBox);
                    BeginInvoke((MethodInvoker)delegate
                    {
                        pcbCorrenteElettrica.Image = Image.FromFile(GetImage(dealer.Carte[0].Seme.ToString() + "" + dealer.Carte[0].Numero.ToString() + ".png", true));
                        pcbCorrenteElettrica.Visible = true;
                        Application.DoEvents();
                    });
                    break;
                case "player-wins":                    
                    myPlace.Fiches = Convert.ToInt32(msg.Data[0]);
                    myPlace.Puntata = 0;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Hai vinto";
                        //TODO: Parametrizzare da qui a gatto :3
                        lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches.ToString();
                        lblMyPuntata.Text = "Devi ancora puntare.";
                        //gatto :3
                    });
                    break;
                case "dealer-wins":
                    myPlace.Fiches = Convert.ToInt32(msg.Data[0]);
                    myPlace.Puntata = 0;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Hai perso";
                        lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches.ToString();
                        lblMyPuntata.Text = "Devi ancora puntare.";
                    });

                    break;
                case "draw":
                    myPlace.Fiches = Convert.ToInt32(msg.Data[0]);
                    myPlace.Puntata = 0;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Pareggio";
                        lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches.ToString();
                        lblMyPuntata.Text = "Devi ancora puntare.";
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
                        
                        BtnPuntata.Enabled = true;
                        
                    });
                    /*Non funzionante, o forse si?*/
                    for (int q = 1; q <= 5; q++)
                    {
                        foreach (PictureBox pcb in Controls["panel" + q].Controls.OfType<PictureBox>())
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                pcb.Image = (int.Parse(pcb.Name.Substring(6)) == 1) ? Image.FromFile(GetImage("Blank.png", false)) : null;
                                //pcb.BringToFront();
                            });
                            //Thread.Sleep(20);
                            //Application.DoEvents();
                        }
                    }
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
                            Point poonto = new Point();
                            poonto.X = 1471;
                            poonto.Y = 816;
                            Controls["panel" + posizione].Controls["LblPlayer" + posizione].Text = username;
                            switch(pos_tavolo)
                            {
                                case 1:
                                    //yikes
                                    break;
                                case 2:
                                    poonto.X = 1471 - 361;
                                    LblMano.Location = poonto;
                                    poonto.X = 1704 - 361;
                                    label2.Location = poonto;
                                    break;
                                case 3:
                                    poonto.X = 1471 - (361 * 2);
                                    LblMano.Location = poonto;
                                    poonto.X = 1704 - (361 * 2);
                                    label2.Location = poonto;
                                    break;
                                case 4:
                                    poonto.X = 1471 - (361 * 3);
                                    LblMano.Location = poonto;
                                    poonto.X = 1704 - (361 * 3);
                                    label2.Location = poonto;
                                    break;
                            }
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
                foreach (PictureBox pcb in Controls["panel" + pos].Controls.OfType<PictureBox>())
                    pcb.Image = null;

            });
            Thread.Sleep(10);
            int k = 0;
            PictureBox current;
            foreach (Card carta in place.Carte)
            {
                k++;
                current = Controls["panel" + pos].Controls["pcbG" + pos + "C" + k] as PictureBox;
                BeginInvoke((MethodInvoker)delegate
                {
                    Controls["panel" + pos].Controls["LblCarte" + pos].Text += $"{carta.Seme}{carta.Numero}\n";
                    current.Image = Image.FromFile(GetImage($"{carta.Seme}{carta.Numero}.png", carta: true));
                    current.Visible = true;
                    
                });
                Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        private string GetImage(string nf, bool carta) =>carta? Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + $"carte\\{nf}" : Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + $"AppData\\img\\{nf}";

        public ClsMessaggio GeneraMessaggio(string action, List<object> data = null)
        {
            ClsMessaggio toSend = new ClsMessaggio();
            ObjMex objMex = new ObjMex(action, data);
            toSend.Messaggio = JsonConvert.SerializeObject(objMex);
            return toSend;
        }


        private void BtnPuntata_Click(object sender, EventArgs e)
        {
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);
            lst.Add(myPlace.Puntata);
            interfacciaRete.Client.Invia(GeneraMessaggio("player-bet",lst));
            BtnPuntata.Enabled = false;
        }

        private void BtnDouble_Click(object sender, EventArgs e)
        {
            BtnDouble.Enabled = false;
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);
            lst.Add(log_id);
            interfacciaRete.Client.Invia(GeneraMessaggio("double-bet", 
                
                lst));
            myPlace.Fiches -= myPlace.Puntata;
            lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches;
            lblMyPuntata.Text = "La tua puntata: " + myPlace.Puntata;
        }

        private void btnQuitTop_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRiduci_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void punta(object sender, EventArgs e)
        {
            int daPuntare = int.Parse((sender as PictureBox).Name.Substring(3));
            if (myPlace.Fiches >= daPuntare)
            {
                myPlace.Puntata += daPuntare;
                myPlace.Fiches -= daPuntare;
                lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches;
                lblMyPuntata.Text = "La tua puntata: " + myPlace.Puntata;
            }
        }

        private void puntaTutto(object sender, EventArgs e)
        {
            myPlace.Puntata += myPlace.Fiches;
            myPlace.Fiches = 0;
            lblMyFiches.Text = "All in!";
            lblMyPuntata.Text = "La tua puntata: " + myPlace.Puntata;
        }
    }
}
