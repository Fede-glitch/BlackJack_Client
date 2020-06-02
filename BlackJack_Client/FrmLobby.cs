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
        #region Variabili di appoggio
        Player player;
        int log_id;
        int pos_tavolo;
        List<Place> posti;
        Place dealer;
        Place myPlace;
        Net interfacciaRete;
        Thread prova;
        #endregion
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
            (sender as Button).FlatAppearance.BorderSize = 1;
        }
        private void ingrassa(object sender, EventArgs e)
        {
            (sender as Button).FlatAppearance.BorderSize = 3;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Sicuro di voler Uscire?","Sicuro?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
                Application.Exit();
            
        }
        #region Metodi utilizzati in fase di debug
        private void aggiornaFiches()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(myPlace.Fiches);
            }
        }
        #endregion

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
            string[] ricevuti = message.toArray();                              //Ricavo un vettore con le informazioni del messaggio
            ObjMex msg = new ObjMex(null, null);                                
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);           //Ottengo le "istruzioni" contenute nel messaggio (Il layout del messaggio è standardizzato)
            switch (msg.Action)                                                 //Eseguo il controllo sul campo che indica l'azione da eseguire sul client
            {
                case "new-cards":                                                                                               //Caso in cui vengono generate le nuove carte del giocatore
                    dynamic appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    List<Card> carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());                    //Ricavo la lista di carte       
                    int pos = Convert.ToInt32(appoggio.Posizione);                                                              //Ricavo la posizione del giocatore corrente nella lobby (da 1 a 4)
                    Place p = new Place(carte, pos);                                                                            //Istanzio un "riferimento" da codice allo spazio di gioco del giocatore                                                       
                    if(pos == pos_tavolo)
                    {
                        (int, bool) mano = p.GetMano();                                                                         //Funzione che ritorna il valore e un booleano che mi dice se è BJ
                        BeginInvoke((MethodInvoker)delegate
                        {
                            LblMano.Text = mano.Item2 ? "BJ": mano.Item1>21 ? "Hai Sballato" : mano.Item1.ToString();           //Se è BJ lo scrivo subito, altrimenti controllo il valore effettivo
                        });
                    }
                    for (int i = 0; i < posti.Count; i++)                                                                       //Ciclo per il numero di posti attualmente presenti
                    {
                        if(posti[i].Posizione == p.Posizione)                                                                   //Quando arrivo alla posizione del Player Corrente aggiorno il suo spazio di gioco    
                        {
                            posti[i] = p;
                            UpdatePlayerGraphics(pos, posti[i]);                                                                //E aggiorno le grafiche
                            Application.DoEvents();
                            break;
                        }
                    }
                    break;
                case "new-cards-dealer":                                                                                        //Caso in cui genero le carte per il dealer
                    appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());                               //Ricavo le carte come prima
                    dealer.Carte = carte;
                    /* OUTDATED
                    BeginInvoke((MethodInvoker)delegate
                    {
                        Controls["panel5"].Controls["LblDealer"].Text = "";
                    });
                    */
                    foreach (PictureBox pcb in Controls["panel5"].Controls.OfType<PictureBox>())                                //Scorro i pcb nello spazio di gioco del banco
                        pcb.Image = (pcb.Name == "pcbBkC1") ? Image.FromFile(GetImage("BlankCard.png", false)) : null;          //e resetto ogni carta al valore null eccetto la prima
                    if ((bool)msg.Data[1])                                                                                      //Se il booleano mi conferma il dover coprire la prima carta
                    {
                        /* OUTDATED
                        foreach (Card carta in dealer.Carte)
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";
                            });
                        }*/
                        PictureBox pcbCartaCoperta = (Controls["panel5"].Controls["pcbBKC1"] as PictureBox);                    //Ricavo i primi due pcb
                        PictureBox pcbCartaScoperta = (Controls["panel5"].Controls["pcbBKC2"] as PictureBox);
                        BeginInvoke((MethodInvoker)delegate
                        {
                            pcbCartaCoperta.Image = Image.FromFile(GetImage("back.png", true));                                 //setto l'immagine della carta coperta 
                            pcbCartaScoperta.Image = Image.FromFile(GetImage(dealer.Carte[1].Seme.ToString() + "" + dealer.Carte[1].Numero.ToString() + ".png", true)); //e di quella scoperta
                            pcbCartaCoperta.Visible = true;                                                                     //Le rendo visibili 
                            pcbCartaScoperta.Visible = true;
                            //pcbCartaCoperta.BringToFront();
                            //pcbCartaScoperta.BringToFront();
                            Thread.Sleep(20);
                            Application.DoEvents();
                        });
                    }
                    else
                    {                                                                                                           //In questo caso devo scoprire tutte le carte
                        int k = 0;
                        foreach (Card carta in dealer.Carte)                                                                    //Scorro in tutte le carte del dealer
                        {
                            k++;
                            PictureBox current = panel5.Controls["pcbBkC" + k] as PictureBox;                                   //Prendo un pcb alla volta
                            BeginInvoke((MethodInvoker)delegate
                            {
                                //Controls["panel5"].Controls["LblDealer"].Text += carta.Seme.ToString() + carta.Numero + "\n";   
                                current.Image = Image.FromFile(GetImage(carta.Seme.ToString() + carta.Numero + ".png", true));  //e ne aggiorno l'immagine
                                //Console.WriteLine(GetImage(carta.Seme.ToString() + carta.Numero + ".png", true));
                                current.Visible = true;                                                                         //rendendolo poi visibile
                                current.BringToFront();
                            });
                            Application.DoEvents();
                        }
                    }
                    break;
                case "your-turn":                                                                                               //caso nel quale inizia il turno del player
                    BeginInvoke((MethodInvoker)delegate
                    {   
                        BtnCarta.Enabled = true;                                                                                //sblocco i bottoni
                        BtnEsci.Enabled = true;
                        if(myPlace.Fiches - myPlace.Puntata >0)
                            BtnDouble.Enabled = true;
                    });
                    break;
                case "blackjack":                                                                                               //caso in cui il player corrente ha totalizazto un BlackJack
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = "BJ";                                                                                    //aggiorno la label
                    });
                    
                    break;
                case "hand-twentyone":                                                                                          //caso in cui chiedendo la carta si totalizza 21 
                case "hand-bust":                                                                                               //caso in cui chiedendo la carta si sballa
                    BeginInvoke((MethodInvoker)delegate
                    {
                        BtnCarta.Enabled = false;                                                                               //entrambi eseguono lo stesso codice, disabilitando i bottoni per le giocate
                        BtnEsci.Enabled = false;
                    });
                    break;
                case "unveil-card":                                                                                             //caso in cui bisogna svelare la carta coperta del dealer
                    PictureBox pcbCorrenteElettrica = (Controls["panel5"].Controls["pcbBKC1"] as PictureBox);                   //prendo un riferimento al pcb
                    BeginInvoke((MethodInvoker)delegate
                    {
                        pcbCorrenteElettrica.Image = Image.FromFile(GetImage(dealer.Carte[0].Seme.ToString() + "" + dealer.Carte[0].Numero.ToString() + ".png", true)); //aggiorno l'immagine
                        pcbCorrenteElettrica.Visible = true;
                        Application.DoEvents();
                    });
                    break;
                case "player-wins":                                                                                             //caso in cui il giocatore vince
                    UpdatePlayerData(1, msg.Data[0]);                                                                           //updato i dati
                    break;
                case "dealer-wins":                                                                                             //caso in cui il giocatore perde
                    /* OUTDATED
                    myPlace.Fiches = Convert.ToInt32(msg.Data[0]);
                    myPlace.Puntata = 0;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Hai perso";
                        lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches.ToString();
                        lblMyPuntata.Text = "Devi ancora puntare.";
                    });*/
                    UpdatePlayerData(-1, msg.Data[0]);                                                                          //updato i dati
                    break;
                case "draw":                                                                                                    //caso in cui si pareggia
                    /*OUTDATED          
                    myPlace.Fiches = Convert.ToInt32(msg.Data[0]);
                    myPlace.Puntata = 0;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = "Pareggio";
                        lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches.ToString();
                        lblMyPuntata.Text = "Devi ancora puntare.";
                    });*/
                    UpdatePlayerData(0, msg.Data[0]);                                                                           //updato i dati
                    break;
                case "new-turn":                                                                                                //caso in cui si avvia una nuova mano
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblMano.Text = "";                                                                                      //azzero le label di feedback
                        LblRis.Text = "";
                        LblDealer.Text = "";
                        LblCarte1.Text = "";
                        LblCarte2.Text = "";
                        LblCarte3.Text = "";
                        LblCarte4.Text = "";
                        
                        BtnPuntata.Enabled = true;                                                                              //permetto la puntata
                        
                    });
                    for (int q = 1; q <= 5; q++)                                                                                //ciclo per tutti gi spazi di gioco
                    {
                        foreach (PictureBox pcb in Controls["panel" + q].Controls.OfType<PictureBox>())                         //prendo ogni picturebox nel panel corrente
                        {
                            BeginInvoke((MethodInvoker)delegate
                            {
                                pcb.Image = (int.Parse(pcb.Name.Substring(6)) == 1) ? Image.FromFile(GetImage("Blank.png", false)) : null; //Aggiorno l'immagine alla prima carta e wipo le altre
                                //pcb.BringToFront();
                            });
                            //Thread.Sleep(20);
                            //Application.DoEvents();
                        }
                    }
                    break;
                case "update-graphics":                                                                                         //caso in cui devo updatare la grafica de player
                    appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());                               //Ricavo le carte
                    dealer.Carte = carte;
                    /*BeginInvoke((MethodInvoker)delegate
                    {
                        OUTDATATO
                        LblDealer.Text = "";
                        foreach (Card carta in dealer.Carte)
                        {
                            LblDealer.Text += $"{carta.Seme}{carta.Numero}\n";
                        }
                    });*/
                    for (int i = 1; i < msg.Data.Count; i++)                                                                    //ciclo per ogni player
                    {
                        appoggio = JsonConvert.DeserializeObject(msg.Data[i].ToString());                                       
                        carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());                           //ricavo le carte
                        pos = Convert.ToInt32(appoggio.Posizione);                                                              //e la posizione
                        p = new Place(carte, pos);                                                                              //costruisco il riferimento allo spazio di gioco del player
                        for (int j = 0; j < posti.Count; j++)                                                                   //updato le grafiche
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
                        LblMano.Text = "";                                                                                      //updato le label varie
                        LblRis.Text = "";
                    });
                    break;
                case "update-graphics-dealer":                                                                                  //caso in cui devo updatare le grafiche del dealer
                    appoggio = JsonConvert.DeserializeObject(msg.Data[0].ToString());
                    carte = JsonConvert.DeserializeObject<List<Card>>(appoggio.Carte.ToString());                               //ricavo le carte
                    dealer.Carte = carte;
                    /* OUTDATED
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblDealer.Text = "";
                        foreach (Card carta in dealer.Carte)
                        {
                            LblDealer.Text += $"{carta.Seme}{carta.Numero}\n";
                        }
                    });
                    */
                    BeginInvoke((MethodInvoker)delegate                                                                         //Updato le varie label
                    {
                        LblMano.Text = "";
                        LblRis.Text = "";
                    });
                    break;
                case "update-names":                                                                                            //caso in cui checko i nickname
                    for (int i = 0; i < msg.Data.Count; i+=2)
                    {
                        string username = msg.Data[i].ToString();                                                               //recupero l'username
                        int posizione = Convert.ToInt32(msg.Data[i + 1]);                                                       //e la posizione
                        BeginInvoke((MethodInvoker)delegate
                        {
                            Point poonto = new Point();                                                                         //genero il punto per muovere la scritat relativa alla mano
                            poonto.X = 1471;
                            poonto.Y = 816;
                            Controls["panel" + posizione].Controls["LblPlayer" + posizione].Text = username;                    //Updato la label
                            switch(pos_tavolo)                                                                                  //muovo la scritta del feedback del punteggio attualemente in mano a seconda del player 
                            {
                                case 1:
                                    //Non cambio
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
                case "player-leave":                                                                                            //caso in cui il player alscia la lobby
                    pos = Convert.ToInt32(msg.Data[0]);                                                                         //ricavo la posizione
                    BeginInvoke((MethodInvoker)delegate
                    {
                        Controls["panel" + pos].Controls["LblPlayer" + pos].Text = "Giocatore " + pos;                          //resetto la label
                        Controls["panel" + pos].Controls["LblCarte" + pos].Text = "";
                    });
                    break;
                case "no-fiches":                                                                                               //caso in cui il player non ha più fiches
                    interfacciaRete.Server.datiRicevutiEvent -= Server_datiRicevutiEventLobby;                                  //dissocio l'evento
                    BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("La partita è terminata.", "Hai terminato le fiches");                                  //Kicko l'utente
                        Application.Exit();
                    });
                    break;
                case "server-shutdown":                                                                                         //caso in cui il server termina la connessione
                    Application.Exit();
                    break;
            }
        }

        private void UpdatePlayerData(int win, Object fiches)
        {
                    myPlace.Fiches = (int)fiches;                                                                               //aggiorno le fiche 
                    myPlace.Puntata = 0;                                                                                        //resetto la puntata
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblRis.Text = (win==1) ? "Hai vinto" : (win==0)? "Pareggio" : "Hai Perso";                              //in base al parametro win mando il feedback al player                                                //Aggiorno le varie label
                        lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches.ToString();                                       //aggiorno le label varie
                        lblMyPuntata.Text = "Devi ancora puntare.";
                    });
        
        }

        private void UpdatePlayerGraphics(int pos, Place place)
        {
            int k = 0;
            PictureBox current;
            BeginInvoke((MethodInvoker)delegate
            {
                //Controls["panel" + pos].Controls["LblCarte" + pos].Text = "";
                foreach (PictureBox pcb in Controls["panel" + pos].Controls.OfType<PictureBox>())                   //Ciclando sui pcb nel pannello della posizione desiderata wipo l'immagine di ognuno
                    pcb.Image = null;
            });
            Thread.Sleep(10);
            foreach (Card carta in place.Carte)                                                                     //Ciclo sulle carte presenti nello spazio di gioco passato per parametro
            {
                k++;
                current = Controls["panel" + pos].Controls["pcbG" + pos + "C" + k] as PictureBox;                   //Prendo un pcb alla volta 
                BeginInvoke((MethodInvoker)delegate
                {
                    //Controls["panel" + pos].Controls["LblCarte" + pos].Text += $"{carta.Seme}{carta.Numero}\n";     //outdated
                    current.Image = Image.FromFile(GetImage($"{carta.Seme}{carta.Numero}.png", carta: true));       //imposto l'immagine del pcb
                    current.Visible = true;                                                                         //e lo rendo visibile
                });
                Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        private string GetImage(string nf, bool carta) =>carta? Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + $"carte\\{nf}" : Application.StartupPath.Substring(0, Application.StartupPath.Length - 9) + $"AppData\\img\\{nf}";   //Metodo per ritornare la stringa del path dato il nome del file e il contesto dell' immagine (se è una carta oppure no) 

        public ClsMessaggio GeneraMessaggio(string action, List<object> data = null)                                //Converte i dati in JSON e impacchetta il messaggio
        {
            ClsMessaggio toSend = new ClsMessaggio();
            ObjMex objMex = new ObjMex(action, data);
            toSend.Messaggio = JsonConvert.SerializeObject(objMex);
            return toSend;
        }


        private void BtnPuntata_Click(object sender, EventArgs e)                                                   
        {
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);                                                                                    //Aggiungo alla lista i dati relativi alla posizione del giocatore nella lobby
            lst.Add(myPlace.Puntata);                                                                               //relativi alla puntata
            interfacciaRete.Client.Invia(GeneraMessaggio("player-bet",lst));                                        //e invio il messaggio dopo averlo impacchettato
            BtnPuntata.Enabled = false;
        }

        private void BtnDouble_Click(object sender, EventArgs e)
        {
            BtnDouble.Enabled = false;
            List<object> lst = new List<object>();
            lst.Add(pos_tavolo);                                                                                    //Aggiungo alla lista i dati della posizione
            lst.Add(log_id);                                                                                        //
            interfacciaRete.Client.Invia(GeneraMessaggio("double-bet", lst));                                       //e invio il messaggio dopo averlo impacchettato
            myPlace.Fiches -= myPlace.Puntata;                                                                      //aggiorno poi il count 
            lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches;                                                  //e le varie label
            lblMyPuntata.Text = "La tua puntata: " + myPlace.Puntata;
        }

        private void btnQuitTop_Click(object sender, EventArgs e)                                                   //Esco
        {
            Application.Exit();             
        }

        private void btnRiduci_Click(object sender, EventArgs e)                                                    //riduco a icona il form
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void punta(object sender, EventArgs e)                                                              //Metodo eseguito dal click sul pcb delle varie fiches
        {
            int daPuntare = int.Parse((sender as PictureBox).Name.Substring(3));                                    //ricavo la puntata dal nome dei pcb che contiene il valore
            if (myPlace.Fiches >= daPuntare)                                                                        //Se posso eseguire la puntata 
            {
                myPlace.Puntata += daPuntare;                                                                       //aggiorno i vari count
                myPlace.Fiches -= daPuntare;                                                                        
                lblMyFiches.Text = "Le tue Fiches: " + myPlace.Fiches;                                              //e le label
                lblMyPuntata.Text = "La tua puntata: " + myPlace.Puntata;
            }
        }

        private void puntaTutto(object sender, EventArgs e)                                                         //Metodo eseguito dal click sulla fiche per l'All in
        {
            myPlace.Puntata += myPlace.Fiches;                                                                      //Aggiorno i count
            myPlace.Fiches = 0;
            lblMyFiches.Text = "All in!";                                                                           //e le label
            lblMyPuntata.Text = "La tua puntata: " + myPlace.Puntata;
        }
    }
}
