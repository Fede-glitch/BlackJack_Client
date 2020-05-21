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
        }

        #region eventi form

        private void Form1_Load(object sender, EventArgs e)
        {
            LblStatoConnessione.Text = "Non connesso";
            TxtEmail.Text = "f.carollo.0729@vallauri.edu";
            TxtPassword.Text = "Password1";
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
                    MessageBox.Show("Connessione al server non ancora stabilita");
            }
            else
                MessageBox.Show(errMsg);
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
        }

        #endregion

       
    }
}
