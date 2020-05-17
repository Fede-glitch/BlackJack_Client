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


namespace BlackJack_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                
            }
            else
                MessageBox.Show(errMsg);
        }

        private string ValidateFields()
        {
            Regex regPassword = new Regex(@"(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.{8,})");
            Regex regEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            if (!regEmail.IsMatch(TxtEmail.Text))
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
