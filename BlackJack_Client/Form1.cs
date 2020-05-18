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
        public Form1()
        {
            InitializeComponent();
            client = new clsClientUDP(IPAddress.Parse(GetLocalIPAddress()), 7777);
            EstablishConn();
        }

        private void EstablishConn()
        {
            int port = GetPort();
            ClsMessaggio msg = new ClsMessaggio(GetLocalIPAddress(), port.ToString());
            ObjMex objMex = new ObjMex("new-conn", port);
            msg.Messaggio = JsonConvert.SerializeObject(objMex);
            client.Invia(msg);
            server = new clsServerUDP(IPAddress.Parse(GetLocalIPAddress()), port);
            server.avvia();
            timerConn = new Timer();
            timerConn.Interval = 5000;
            timerConn.Tick += TimerConn_Tick;
            timerConn.Start();
            server.datiRicevutiEvent += Server_datiRicevutiEvent;
        }

        private void TimerConn_Tick(object sender, EventArgs e)
        {
            timerConn.Stop();
            MessageBox.Show("Connessione fallita al server");
            EstablishConn();
        }

        private void Server_datiRicevutiEvent(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "conn-established":
                    timerConn.Stop();
                    MessageBox.Show("Connessione stabilita");
                    break;
            }
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

        private int GetPort()
        {
            int port = 7778;
            while(isPortOpen(port))
                port++;
            return port;
        }

        private bool isPortOpen(int port)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect("127.0.0.1", port);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
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
                ClsMessaggio msg = new ClsMessaggio();
                msg.Ip = "127.0.0.1";
                msg.Port = "7777";

                ObjMex objMex = new ObjMex("Prova", new Player(TxtEmail.Text, TxtPassword.Text));
                msg.Messaggio = JsonConvert.SerializeObject(objMex);
                client.Invia(msg);
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
    }
}
