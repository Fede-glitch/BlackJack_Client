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
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.lblBenvenuto = new System.Windows.Forms.Label();
            this.LblStatoConnessione = new System.Windows.Forms.Label();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.lineaUsername = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pcbLogoPassword = new System.Windows.Forms.PictureBox();
            this.pcbLogoUsername = new System.Windows.Forms.PictureBox();
            this.pcbShowHide = new System.Windows.Forms.PictureBox();
            this.pnlImg = new System.Windows.Forms.Panel();
            this.btnQuitta = new System.Windows.Forms.Button();
            this.txtErrore = new System.Windows.Forms.TextBox();
            this.lblPlaceHolder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogoPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogoUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbShowHide)).BeginInit();
            this.pnlImg.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnAccedi
            // 
            this.BtnAccedi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAccedi.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 27.84905F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAccedi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAccedi.Location = new System.Drawing.Point(12, 644);
            this.BtnAccedi.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAccedi.Name = "BtnAccedi";
            this.BtnAccedi.Size = new System.Drawing.Size(539, 57);
            this.BtnAccedi.TabIndex = 21;
            this.BtnAccedi.Text = "Login";
            this.BtnAccedi.UseVisualStyleBackColor = true;
            this.BtnAccedi.Click += new System.EventHandler(this.BtnAccedi_Click);
            // 
            // TxtPassword
            // 
            this.TxtPassword.BackColor = System.Drawing.Color.Black;
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPassword.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 25.81132F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtPassword.Location = new System.Drawing.Point(76, 523);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '•';
            this.TxtPassword.Size = new System.Drawing.Size(475, 46);
            this.TxtPassword.TabIndex = 16;
            this.TxtPassword.Text = "Password";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BackColor = System.Drawing.Color.Black;
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtEmail.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 25.81132F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtEmail.Location = new System.Drawing.Point(76, 453);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(475, 46);
            this.TxtEmail.TabIndex = 15;
            this.TxtEmail.Text = "Username";
            this.TxtEmail.Enter += new System.EventHandler(this.TxtEmail_Enter);
            this.TxtEmail.Leave += new System.EventHandler(this.TxtEmail_Leave);
            // 
            // lblBenvenuto
            // 
            this.lblBenvenuto.AutoSize = true;
            this.lblBenvenuto.BackColor = System.Drawing.Color.Transparent;
            this.lblBenvenuto.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 21.73585F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBenvenuto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBenvenuto.Location = new System.Drawing.Point(88, 172);
            this.lblBenvenuto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBenvenuto.Name = "lblBenvenuto";
            this.lblBenvenuto.Size = new System.Drawing.Size(205, 39);
            this.lblBenvenuto.TabIndex = 11;
            this.lblBenvenuto.Text = "BLACKJACK";
            // 
            // LblStatoConnessione
            // 
            this.LblStatoConnessione.AutoSize = true;
            this.LblStatoConnessione.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatoConnessione.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LblStatoConnessione.Location = new System.Drawing.Point(209, 419);
            this.LblStatoConnessione.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblStatoConnessione.Name = "LblStatoConnessione";
            this.LblStatoConnessione.Size = new System.Drawing.Size(143, 19);
            this.LblStatoConnessione.TabIndex = 22;
            this.LblStatoConnessione.Text = "Stato connessione";
            // 
            // BtnRegister
            // 
            this.BtnRegister.BackColor = System.Drawing.Color.Silver;
            this.BtnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegister.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 27.84905F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRegister.ForeColor = System.Drawing.Color.Black;
            this.BtnRegister.Location = new System.Drawing.Point(12, 705);
            this.BtnRegister.Margin = new System.Windows.Forms.Padding(2);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(539, 57);
            this.BtnRegister.TabIndex = 25;
            this.BtnRegister.Text = "Registrati";
            this.BtnRegister.UseVisualStyleBackColor = false;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // lineaUsername
            // 
            this.lineaUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lineaUsername.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lineaUsername.Location = new System.Drawing.Point(12, 502);
            this.lineaUsername.Name = "lineaUsername";
            this.lineaUsername.Size = new System.Drawing.Size(539, 3);
            this.lineaUsername.TabIndex = 28;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(12, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 3);
            this.panel1.TabIndex = 29;
            // 
            // pcbLogoPassword
            // 
            this.pcbLogoPassword.Image = global::BlackJack_Client.Properties.Resources.Lock;
            this.pcbLogoPassword.Location = new System.Drawing.Point(12, 520);
            this.pcbLogoPassword.Name = "pcbLogoPassword";
            this.pcbLogoPassword.Size = new System.Drawing.Size(50, 50);
            this.pcbLogoPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbLogoPassword.TabIndex = 27;
            this.pcbLogoPassword.TabStop = false;
            // 
            // pcbLogoUsername
            // 
            this.pcbLogoUsername.Image = global::BlackJack_Client.Properties.Resources.Picche;
            this.pcbLogoUsername.Location = new System.Drawing.Point(12, 453);
            this.pcbLogoUsername.Name = "pcbLogoUsername";
            this.pcbLogoUsername.Size = new System.Drawing.Size(50, 50);
            this.pcbLogoUsername.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbLogoUsername.TabIndex = 26;
            this.pcbLogoUsername.TabStop = false;
            // 
            // pcbShowHide
            // 
            this.pcbShowHide.Image = global::BlackJack_Client.Properties.Resources.VisiblePassword;
            this.pcbShowHide.Location = new System.Drawing.Point(501, 523);
            this.pcbShowHide.Name = "pcbShowHide";
            this.pcbShowHide.Size = new System.Drawing.Size(50, 50);
            this.pcbShowHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbShowHide.TabIndex = 24;
            this.pcbShowHide.TabStop = false;
            this.pcbShowHide.Click += new System.EventHandler(this.pcbShowHide_Click);
            // 
            // pnlImg
            // 
            this.pnlImg.BackColor = System.Drawing.Color.Black;
            this.pnlImg.BackgroundImage = global::BlackJack_Client.Properties.Resources.Picche_Sfondo;
            this.pnlImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlImg.Controls.Add(this.lblBenvenuto);
            this.pnlImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlImg.Location = new System.Drawing.Point(94, 7);
            this.pnlImg.Name = "pnlImg";
            this.pnlImg.Size = new System.Drawing.Size(400, 400);
            this.pnlImg.TabIndex = 23;
            // 
            // btnQuitta
            // 
            this.btnQuitta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitta.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 27.84905F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitta.ForeColor = System.Drawing.Color.Red;
            this.btnQuitta.Location = new System.Drawing.Point(12, 766);
            this.btnQuitta.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuitta.Name = "btnQuitta";
            this.btnQuitta.Size = new System.Drawing.Size(539, 57);
            this.btnQuitta.TabIndex = 30;
            this.btnQuitta.Text = "Esci";
            this.btnQuitta.UseVisualStyleBackColor = true;
            this.btnQuitta.Click += new System.EventHandler(this.btnQuitta_Click);
            // 
            // txtErrore
            // 
            this.txtErrore.BackColor = System.Drawing.Color.Black;
            this.txtErrore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtErrore.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 18.33962F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrore.ForeColor = System.Drawing.Color.Maroon;
            this.txtErrore.Location = new System.Drawing.Point(12, 590);
            this.txtErrore.Margin = new System.Windows.Forms.Padding(2);
            this.txtErrore.Name = "txtErrore";
            this.txtErrore.Size = new System.Drawing.Size(539, 33);
            this.txtErrore.TabIndex = 31;
            this.txtErrore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtErrore.Visible = false;
            this.txtErrore.Enter += new System.EventHandler(this.tuNonPuoiScrivere);
            // 
            // lblPlaceHolder
            // 
            this.lblPlaceHolder.AutoSize = true;
            this.lblPlaceHolder.Location = new System.Drawing.Point(-1, 12);
            this.lblPlaceHolder.Name = "lblPlaceHolder";
            this.lblPlaceHolder.Size = new System.Drawing.Size(0, 15);
            this.lblPlaceHolder.TabIndex = 32;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(563, 835);
            this.Controls.Add(this.lblPlaceHolder);
            this.Controls.Add(this.txtErrore);
            this.Controls.Add(this.btnQuitta);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lineaUsername);
            this.Controls.Add(this.pcbLogoPassword);
            this.Controls.Add(this.pcbLogoUsername);
            this.Controls.Add(this.BtnRegister);
            this.Controls.Add(this.pcbShowHide);
            this.Controls.Add(this.LblStatoConnessione);
            this.Controls.Add(this.pnlImg);
            this.Controls.Add(this.BtnAccedi);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogoPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogoUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbShowHide)).EndInit();
            this.pnlImg.ResumeLayout(false);
            this.pnlImg.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAccedi;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Label lblBenvenuto;
        private System.Windows.Forms.Label LblStatoConnessione;
        private System.Windows.Forms.Panel pnlImg;
        private System.Windows.Forms.PictureBox pcbShowHide;
        private System.Windows.Forms.Button BtnRegister;
        private System.Windows.Forms.PictureBox pcbLogoUsername;
        private System.Windows.Forms.PictureBox pcbLogoPassword;
        private System.Windows.Forms.Panel lineaUsername;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuitta;
        private System.Windows.Forms.TextBox txtErrore;
        private System.Windows.Forms.Label lblPlaceHolder;
    }
}

