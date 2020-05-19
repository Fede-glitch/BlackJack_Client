namespace BlackJack_Client
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnAccedi = new System.Windows.Forms.Button();
            this.LblNewUser = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.LblForgotPwd = new System.Windows.Forms.LinkLabel();
            this.LblShowPwd = new System.Windows.Forms.LinkLabel();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblStatoConnessione = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnAccedi
            // 
            this.BtnAccedi.Location = new System.Drawing.Point(206, 165);
            this.BtnAccedi.Name = "BtnAccedi";
            this.BtnAccedi.Size = new System.Drawing.Size(144, 51);
            this.BtnAccedi.TabIndex = 21;
            this.BtnAccedi.Text = "Accedi";
            this.BtnAccedi.UseVisualStyleBackColor = true;
            this.BtnAccedi.Click += new System.EventHandler(this.BtnAccedi_Click);
            // 
            // LblNewUser
            // 
            this.LblNewUser.AutoSize = true;
            this.LblNewUser.Location = new System.Drawing.Point(128, 182);
            this.LblNewUser.Name = "LblNewUser";
            this.LblNewUser.Size = new System.Drawing.Size(68, 17);
            this.LblNewUser.TabIndex = 20;
            this.LblNewUser.TabStop = true;
            this.LblNewUser.Text = "Clicca qui";
            this.LblNewUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblNewUser_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Nuovo utente?";
            // 
            // LblForgotPwd
            // 
            this.LblForgotPwd.AutoSize = true;
            this.LblForgotPwd.LinkColor = System.Drawing.Color.Red;
            this.LblForgotPwd.Location = new System.Drawing.Point(30, 138);
            this.LblForgotPwd.Name = "LblForgotPwd";
            this.LblForgotPwd.Size = new System.Drawing.Size(153, 17);
            this.LblForgotPwd.TabIndex = 18;
            this.LblForgotPwd.TabStop = true;
            this.LblForgotPwd.Text = "Password dimenticata?";
            // 
            // LblShowPwd
            // 
            this.LblShowPwd.AutoSize = true;
            this.LblShowPwd.Location = new System.Drawing.Point(235, 138);
            this.LblShowPwd.Name = "LblShowPwd";
            this.LblShowPwd.Size = new System.Drawing.Size(115, 17);
            this.LblShowPwd.TabIndex = 17;
            this.LblShowPwd.TabStop = true;
            this.LblShowPwd.Text = "Mostra password";
            this.LblShowPwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblShowPwd_LinkClicked);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(118, 113);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(232, 22);
            this.TxtPassword.TabIndex = 16;
            // 
            // TxtEmail
            // 
            this.TxtEmail.Location = new System.Drawing.Point(118, 75);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(232, 22);
            this.TxtEmail.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Accedi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "BENVENUTO";
            // 
            // LblStatoConnessione
            // 
            this.LblStatoConnessione.AutoSize = true;
            this.LblStatoConnessione.Location = new System.Drawing.Point(12, 219);
            this.LblStatoConnessione.Name = "LblStatoConnessione";
            this.LblStatoConnessione.Size = new System.Drawing.Size(156, 21);
            this.LblStatoConnessione.TabIndex = 22;
            this.LblStatoConnessione.Text = "Stato connessione";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 245);
            this.Controls.Add(this.LblStatoConnessione);
            this.Controls.Add(this.BtnAccedi);
            this.Controls.Add(this.LblNewUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LblForgotPwd);
            this.Controls.Add(this.LblShowPwd);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAccedi;
        private System.Windows.Forms.LinkLabel LblNewUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel LblForgotPwd;
        private System.Windows.Forms.LinkLabel LblShowPwd;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblStatoConnessione;
    }
}

