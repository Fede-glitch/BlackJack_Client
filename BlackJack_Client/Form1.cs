using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using SOCKET_UDP;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace BlackJack_Client
{
    public partial class Form1 : Form
    {
        Net interfacciaRete;
        public Form1()
        {
            InitializeComponent();
            interfacciaRete = new Net();
            interfacciaRete.Client = new clsClientUDP(IPAddress.Parse(Net.GetLocalIPAddress()), 7777);
            interfacciaRete.EstablishConn();
            interfacciaRete.Server.datiRicevutiEvent += Server_DatiRicevutiFormLogin;  
        }

        private void Server_DatiRicevutiFormLogin(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = new ObjMex(null, null);
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "login-success":
                    FrmLobby lobby = new FrmLobby(interfacciaRete,
                                                JsonConvert.DeserializeObject<Player>(msg.Data[0].ToString()),
                                                Convert.ToInt32(msg.Data[1]),
                                                interfacciaRete.log_id);
                    BeginInvoke((MethodInvoker)delegate
                    {
                        lobby.Show();
                    });
                    break;
                case "server-shutdown":
                    MessageBox.Show("Connessione al server persa");
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblStatoConnessione.Text = "Non connesso";
                    });
                    break;
                case "conn-established":
                    interfacciaRete.timerConn.Stop();
                    interfacciaRete.log_id = Convert.ToInt32(msg.Data[0]);
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblStatoConnessione.Text = "Connesso";
                    });
                    break;
                case "login-failed":
                    if (Convert.ToBoolean(msg.Data[0]))
                    {
                        //MessageBox.Show("Credenziali errate");
                        txtErrore.Text = "Credenziali errate.";
                        txtErrore.Visible = true;
                    }
                    else
                    {
                        //MessageBox.Show("Questo utente si trova già in partita");
                        txtErrore.Text = "Questo utente si trova già in partita";
                        txtErrore.Visible = true;
                    }
                    break;
                case "lobby-full":
                    //MessageBox.Show("Lobby al momento piena, riprova più tardi");
                    txtErrore.Text = "Lobby al momento piena, riprova più tardi";
                    txtErrore.Visible = true;
                    break;
            }
        }

        #region eventi form

        private void Form1_Load(object sender, EventArgs e)
        {
            LblStatoConnessione.Text = "Non connesso";
            TxtEmail.Text = "f.carollo.0729@vallauri.edu";
            TxtPassword.Text = "Password1";
        }


        private void BtnAccedi_Click(object sender, EventArgs e)
        {
            string errMsg;
            if ((errMsg = ValidateFields()) == "")
            {
                if (interfacciaRete.log_id != 0)
                {
                    Regex regEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
                    List<object> lst = new List<object>();
                    lst.Add(interfacciaRete.log_id);
                    if(regEmail.IsMatch(TxtEmail.Text))
                        lst.Add(new Player(TxtEmail.Text, null, TxtPassword.Text));
                    else
                        lst.Add(new Player(null, TxtEmail.Text, TxtPassword.Text));
                    interfacciaRete.Client.Invia(Net.GeneraMessaggio("login-ask",lst));
                }
                else
                {
                    //MessageBox.Show("Connessione al server non ancora stabilita");
                    txtErrore.Text = "Connessione al server non ancora stabilita";
                    txtErrore.Visible = true;
                }
                    
            }
            else
            {
                //MessageBox.Show(errMsg);
                txtErrore.Text = errMsg;
                txtErrore.Visible = true;
            }
                
        }

        private string ValidateFields()
        {
            Regex regPassword = new Regex(@"(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.{8,})");
            if (!regPassword.IsMatch(TxtPassword.Text))
            {
                TxtPassword.Focus();
                return "Password non valida";
            }
            return "";
        }

        
        private void LblNewUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TODO Creazione utenti
            //on Btnregistrati
        }
        

        #endregion
        #region UI CONTROLS CODE
        private void pcbShowHide_Click(object sender, EventArgs e)
        {
            TxtPassword.PasswordChar = TxtPassword.PasswordChar == '•' ? '\0' : '•';
            pcbShowHide.Image = (TxtPassword.PasswordChar != '•') ? Properties.Resources.HiddenPassword : Properties.Resources.VisiblePassword;
            pcbShowHide.Refresh();
        }
        #endregion

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (TxtEmail.Text == "")
                TxtEmail.Text = "Username";
        }

        private void TxtEmail_Enter(object sender, EventArgs e)
        {
            if (TxtEmail.Text == "Username")
                TxtEmail.Text = "";
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (interfacciaRete.log_id != 0)
            {
                FrmNewUser frmNew = new FrmNewUser(interfacciaRete);
                frmNew.Show();
            }
            else
            {
                //MessageBox.Show("Connessione al server non ancora stabilita");
                txtErrore.Text = "Connessione al server non ancora stabilita";
                txtErrore.Visible = true;
            }
                
            
        }

        private void tuNonPuoiScrivere(object sender, EventArgs e)
        {
            this.ActiveControl = lblPlaceHolder;
        }

        private void btnQuitta_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
