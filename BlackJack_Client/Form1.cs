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
        clsClientUDP client;
        clsServerUDP server;
        Timer timerConn;
        int log_id = 0;
        int port = 0;
        public Form1()
        {
            InitializeComponent();
            client = new clsClientUDP(IPAddress.Parse(NetUtilities.GetLocalIPAddress()), 7777);
            EstablishConn();
        }

        private void EstablishConn(bool istanziaServer = true)
        {
            if(istanziaServer)
            {
                port = NetUtilities.GetPort();
                server = new clsServerUDP(IPAddress.Parse(NetUtilities.GetLocalIPAddress()), port);
                server.avvia();
                server.datiRicevutiEvent += Server_datiRicevutiEvent;
            }
            ClsMessaggio msg = new ClsMessaggio(NetUtilities.GetLocalIPAddress(), port.ToString());
            ObjMex objMex = new ObjMex("new-conn", null, port);
            msg.Messaggio = JsonConvert.SerializeObject(objMex);
            client.Invia(msg);
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

        private void Server_datiRicevutiEvent(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = new ObjMex(null, null, null);
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "conn-established":
                    timerConn.Stop();
                    log_id = (int)msg.SingleData;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        LblStatoConnessione.Text = "Connesso";
                    });                    
                    break;
                case "login-success":
                    //TODO
                case "login-failed":
                    MessageBox.Show("Credenziali errate");
                    break;
            }
        }


        private void LblShowPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LblShowPwd.Text = LblShowPwd.Text == "Mostra password" ? "Nascondi password" : "Mostra password";
            TxtPassword.PasswordChar = TxtPassword.PasswordChar == '*' ? '\0' : '*';
        }

        private void BtnAccedi_Click(object sender, EventArgs e)
        {
            string errMsg;
            if ((errMsg = ValidateFields()) == "")
            {
                if (log_id != 0)
                {
                    ClsMessaggio msg = new ClsMessaggio(NetUtilities.GetLocalIPAddress(), 7777.ToString());
                    List<object> lst = new List<object>();
                    lst.Add(log_id);
                    lst.Add(new Player(TxtEmail.Text, TxtPassword.Text));
                    ObjMex objMex = new ObjMex("login-ask", lst, null);
                    msg.Messaggio = JsonConvert.SerializeObject(objMex);
                    client.Invia(msg);
                }
                else
                    MessageBox.Show("Connessione al server non ancora stabilita");
            }
            else
                MessageBox.Show(errMsg);
        }

        private string ValidateFields()
        {
            Regex regPassword = new Regex(@"(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.{8,})");
            Regex regEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            if(!regEmail.IsMatch(TxtEmail.Text))
            {
                TxtEmail.Focus();
                return "Email non valida";
            }
            if (!regPassword.IsMatch(TxtPassword.Text))
            {
                TxtPassword.Focus();
                return "Password non valida";
            }
            return "";
        }

        private void LblNewUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LblStatoConnessione.Text = "Non connesso";
        }
    }
}
