using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using SOCKET_UDP;
using Newtonsoft.Json;

namespace BlackJack_Client
{
    public partial class FrmNewUser : Form
    {
        Net interfacciaRete;
        public FrmNewUser(Net interfacciaRete)
        {
            InitializeComponent();
            this.interfacciaRete = interfacciaRete;
            this.interfacciaRete.Server.datiRicevutiEvent += Server_DatiRicevutiEventNewUser;
        }

        private void Server_DatiRicevutiEventNewUser(ClsMessaggio message)
        {
            string[] ricevuti = message.toArray();
            ObjMex msg = new ObjMex(null, null);
            msg = JsonConvert.DeserializeObject<ObjMex>(ricevuti[2]);
            switch (msg.Action)
            {
                case "response-register":
                    if(Convert.ToBoolean(msg.Data[0]))
                    {
                        MailAddress destinatario = new MailAddress(TxtEmail.Text);
                        MailAddress mittente = new MailAddress("noreplycarollofederico@gmail.com");
                        MailMessage messaggio = new MailMessage(mittente, destinatario);
                        messaggio.Subject = $"Benvenuto {TxtUsername.Text} in BlackJack casinò";
                        messaggio.Body = "Registrazione avvenuta con successo sulla nostra piattaforma\n" +
                                        "Le tue credenziali:\n" +
                                        $"Email: {TxtEmail.Text}\n" +
                                        $"Password: {TxtPassword.Text}";
                        SmtpClient client = new SmtpClient();
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential("noreplycarollofederico@gmail.com", "noreplypass2002");
                        client.Send(messaggio);
                        interfacciaRete.Server.datiRicevutiEvent -= Server_DatiRicevutiEventNewUser;
                        MessageBox.Show("Registrazione completata");
                        BeginInvoke((MethodInvoker)delegate
                        {
                            this.Close();
                        });
                    }
                    else
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show((msg.Data[1].ToString() == "email"? "Email":"Username") + " già esistente");
                        });
                    }
                    break;
            }
        }

        private void BtnConferma_Click(object sender, EventArgs e)
        {
            string errMsg = "";
            if ((errMsg = ValidateFields()) != "")
                MessageBox.Show(errMsg, "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAnnulla_Click(object sender, EventArgs e)
        {
            interfacciaRete.Server.datiRicevutiEvent -= Server_DatiRicevutiEventNewUser;
            this.Close();
        }

        private string ValidateFields()
        {
            List<object> lst = new List<object>();
            Regex regEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            Regex regPassword = new Regex(@"(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.{8,})");
            if (regEmail.IsMatch(TxtUsername.Text))
                return "Non credi che questo username assomigli di più ad una email?";
            if (!regEmail.IsMatch(TxtEmail.Text))
                return "Email non valida";
            if(!regPassword.IsMatch(TxtPassword.Text))
                return "La Password deve contenere un carattere numerico, " +
                    "un carattere maiuscolo, " +
                    "uno minuscolo ed essere lunga almeno 8 caratteri";
            lst.Add(interfacciaRete.log_id);
            lst.Add(TxtEmail.Text);
            lst.Add(TxtUsername.Text);
            lst.Add(TxtPassword.Text);
            interfacciaRete.Client.Invia(Net.GeneraMessaggio("register", lst));
            return "";
        }
    }
}
