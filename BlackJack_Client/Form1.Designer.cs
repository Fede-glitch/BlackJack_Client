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
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogoPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogoUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbShowHide)).BeginInit();
            this.pnlImg.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnAccedi
            // 
            this.BtnAccedi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAccedi.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.81132F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAccedi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAccedi.Location = new System.Drawing.Point(232, 697);
            this.BtnAccedi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnAccedi.Name = "BtnAccedi";
            this.BtnAccedi.Size = new System.Drawing.Size(719, 70);
            this.BtnAccedi.TabIndex = 21;
            this.BtnAccedi.Text = "Login";
            this.BtnAccedi.UseVisualStyleBackColor = true;
            this.BtnAccedi.Click += new System.EventHandler(this.BtnAccedi_Click);
            // 
            // TxtPassword
            // 
            this.TxtPassword.BackColor = System.Drawing.Color.Black;
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtPassword.Location = new System.Drawing.Point(317, 614);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '•';
            this.TxtPassword.Size = new System.Drawing.Size(633, 53);
            this.TxtPassword.TabIndex = 16;
            this.TxtPassword.Text = "Password";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BackColor = System.Drawing.Color.Black;
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtEmail.Location = new System.Drawing.Point(317, 528);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(633, 53);
            this.TxtEmail.TabIndex = 15;
            this.TxtEmail.Text = "Username";
            this.TxtEmail.Enter += new System.EventHandler(this.TxtEmail_Enter);
            this.TxtEmail.Leave += new System.EventHandler(this.TxtEmail_Leave);
            // 
            // lblBenvenuto
            // 
            this.lblBenvenuto.AutoSize = true;
            this.lblBenvenuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBenvenuto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBenvenuto.Location = new System.Drawing.Point(136, 206);
            this.lblBenvenuto.Name = "lblBenvenuto";
            this.lblBenvenuto.Size = new System.Drawing.Size(210, 38);
            this.lblBenvenuto.TabIndex = 11;
            this.lblBenvenuto.Text = "BLACKJACK";
            // 
            // LblStatoConnessione
            // 
            this.LblStatoConnessione.AutoSize = true;
            this.LblStatoConnessione.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatoConnessione.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LblStatoConnessione.Location = new System.Drawing.Point(927, 36);
            this.LblStatoConnessione.Name = "LblStatoConnessione";
            this.LblStatoConnessione.Size = new System.Drawing.Size(250, 32);
            this.LblStatoConnessione.TabIndex = 22;
            this.LblStatoConnessione.Text = "Stato connessione";
            // 
            // BtnRegister
            // 
            this.BtnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.81132F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnRegister.Location = new System.Drawing.Point(232, 772);
            this.BtnRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(719, 70);
            this.BtnRegister.TabIndex = 25;
            this.BtnRegister.Text = "Registrati";
            this.BtnRegister.UseVisualStyleBackColor = true;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // lineaUsername
            // 
            this.lineaUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lineaUsername.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lineaUsername.Location = new System.Drawing.Point(232, 588);
            this.lineaUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lineaUsername.Name = "lineaUsername";
            this.lineaUsername.Size = new System.Drawing.Size(719, 4);
            this.lineaUsername.TabIndex = 28;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(232, 672);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 4);
            this.panel1.TabIndex = 29;
            // 
            // pcbLogoPassword
            // 
            this.pcbLogoPassword.Image = global::BlackJack_Client.Properties.Resources.Lock;
            this.pcbLogoPassword.Location = new System.Drawing.Point(232, 610);
            this.pcbLogoPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcbLogoPassword.Name = "pcbLogoPassword";
            this.pcbLogoPassword.Size = new System.Drawing.Size(67, 62);
            this.pcbLogoPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbLogoPassword.TabIndex = 27;
            this.pcbLogoPassword.TabStop = false;
            // 
            // pcbLogoUsername
            // 
            this.pcbLogoUsername.Image = global::BlackJack_Client.Properties.Resources.Picche;
            this.pcbLogoUsername.Location = new System.Drawing.Point(232, 528);
            this.pcbLogoUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcbLogoUsername.Name = "pcbLogoUsername";
            this.pcbLogoUsername.Size = new System.Drawing.Size(67, 62);
            this.pcbLogoUsername.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbLogoUsername.TabIndex = 26;
            this.pcbLogoUsername.TabStop = false;
            // 
            // pcbShowHide
            // 
            this.pcbShowHide.Image = global::BlackJack_Client.Properties.Resources.VisiblePassword;
            this.pcbShowHide.Location = new System.Drawing.Point(884, 614);
            this.pcbShowHide.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcbShowHide.Name = "pcbShowHide";
            this.pcbShowHide.Size = new System.Drawing.Size(67, 62);
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
            this.pnlImg.Location = new System.Drawing.Point(341, 2);
            this.pnlImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlImg.Name = "pnlImg";
            this.pnlImg.Size = new System.Drawing.Size(533, 492);
            this.pnlImg.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1232, 857);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
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
    }
}

