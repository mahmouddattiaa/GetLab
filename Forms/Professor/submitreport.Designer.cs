namespace GetLab.Forms.Student
    {
    partial class submitreport
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEquipment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(273, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 85);
            this.label1.TabIndex = 5;
            this.label1.Text = "Submit Report";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Uighur", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(45, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 62);
            this.label2.TabIndex = 6;
            this.label2.Text = "Equipment";
            // 
            // cmbEquipment
            // 
            this.cmbEquipment.FormattingEnabled = true;
            this.cmbEquipment.Location = new System.Drawing.Point(271, 205);
            this.cmbEquipment.Name = "cmbEquipment";
            this.cmbEquipment.Size = new System.Drawing.Size(229, 28);
            this.cmbEquipment.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Uighur", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(409, 62);
            this.label3.TabIndex = 8;
            this.label3.Text = "Please describe the issue:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(447, 286);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(376, 119);
            this.txtDescription.TabIndex = 9;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Uighur", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSubmit.Location = new System.Drawing.Point(56, 456);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(214, 67);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // submitreport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(944, 592);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbEquipment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "submitreport";
            this.Text = "submitreport";
            this.Load += new System.EventHandler(this.submitreport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEquipment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSubmit;
        }
    }