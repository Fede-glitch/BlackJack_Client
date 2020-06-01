namespace BlackJack_Client
{
    partial class FrmNewUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnConferma = new System.Windows.Forms.Button();
            this.BtnAnnulla = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtUsername
            // 
            this.TxtUsername.Location = new System.Drawing.Point(126, 59);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(148, 22);
            this.TxtUsername.TabIndex = 10;
            // 
            // TxtEmail
            // 
            this.TxtEmail.Location = new System.Drawing.Point(127, 96);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(148, 22);
            this.TxtEmail.TabIndex = 12;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(127, 133);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(148, 22);
            this.TxtPassword.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Email";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "Registrazione";
            // 
            // BtnConferma
            // 
            this.BtnConferma.Location = new System.Drawing.Point(153, 170);
            this.BtnConferma.Name = "BtnConferma";
            this.BtnConferma.Size = new System.Drawing.Size(122, 43);
            this.BtnConferma.TabIndex = 16;
            this.BtnConferma.Text = "Conferma";
            this.BtnConferma.UseVisualStyleBackColor = true;
            this.BtnConferma.Click += new System.EventHandler(this.BtnConferma_Click);
            // 
            // BtnAnnulla
            // 
            this.BtnAnnulla.Location = new System.Drawing.Point(15, 170);
            this.BtnAnnulla.Name = "BtnAnnulla";
            this.BtnAnnulla.Size = new System.Drawing.Size(122, 43);
            this.BtnAnnulla.TabIndex = 17;
            this.BtnAnnulla.Text = "Annulla";
            this.BtnAnnulla.UseVisualStyleBackColor = true;
            this.BtnAnnulla.Click += new System.EventHandler(this.BtnAnnulla_Click);
            // 
            // FrmNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 227);
            this.Controls.Add(this.BtnAnnulla);
            this.Controls.Add(this.BtnConferma);
            this.Controls.Add(this.TxtUsername);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Name = "FrmNewUser";
            this.Text = "FrmNewUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtUsername;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnConferma;
        private System.Windows.Forms.Button BtnAnnulla;
    }
}