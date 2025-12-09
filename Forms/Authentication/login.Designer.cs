namespace GetLab.Forms.Authentication
    {
    partial class login
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
            {
            if ( disposing && ( components != null ) )
                {
                components.Dispose ( );
                }
            base.Dispose ( disposing );
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
            {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.idBX = new System.Windows.Forms.MaskedTextBox();
            this.passBX = new System.Windows.Forms.MaskedTextBox();
            this.loginBT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Uighur", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(82, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 57);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Uighur", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(82, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 57);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // idBX
            // 
            this.idBX.Location = new System.Drawing.Point(91, 234);
            this.idBX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.idBX.Name = "idBX";
            this.idBX.Size = new System.Drawing.Size(504, 26);
            this.idBX.TabIndex = 3;
            // 
            // passBX
            // 
            this.passBX.Location = new System.Drawing.Point(91, 329);
            this.passBX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.passBX.Name = "passBX";
            this.passBX.Size = new System.Drawing.Size(504, 26);
            this.passBX.TabIndex = 4;
            // 
            // loginBT
            // 
            this.loginBT.Font = new System.Drawing.Font("Microsoft Uighur", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBT.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.loginBT.Location = new System.Drawing.Point(380, 384);
            this.loginBT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginBT.Name = "loginBT";
            this.loginBT.Size = new System.Drawing.Size(215, 74);
            this.loginBT.TabIndex = 5;
            this.loginBT.Text = "login";
            this.loginBT.UseVisualStyleBackColor = true;
            this.loginBT.Click += new System.EventHandler(this.loginBT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(550, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 85);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(84, 404);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 39);
            this.label4.TabIndex = 6;
            this.label4.Text = "Forgot Password?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Uighur", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(363, 476);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 34);
            this.label5.TabIndex = 7;
            this.label5.Text = "Don’t have an account?";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(565, 478);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 32);
            this.button2.TabIndex = 8;
            this.button2.Text = "Create";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::GetLab.Properties.Resources.LOGOOO;
            this.ClientSize = new System.Drawing.Size(1305, 769);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loginBT);
            this.Controls.Add(this.passBX);
            this.Controls.Add(this.idBX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "login";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox idBX;
        private System.Windows.Forms.MaskedTextBox passBX;
        private System.Windows.Forms.Button loginBT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
    }
    }

