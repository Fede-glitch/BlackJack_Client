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

namespace BlackJack_Client
{
    public partial class Form1 : Form
    {
        clsClientUDP client;
        clsServerUDP server;
        public Form1()
        {
            InitializeComponent();
            client = new clsClientUDP(IPAddress.Parse("127.0.0.1"), 7777);
            server = new clsServerUDP(IPAddress.Parse("127.0.0.1"), 7777);
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

                ObjMex objMex = new ObjMex("Prova", new Utente(TxtEmail.Text, TxtPassword.Text));
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
