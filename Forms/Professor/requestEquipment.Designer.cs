namespace GetLab.Forms.Professor
{
    partial class requestEquipment
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
            this.equipLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.equipNameTxt = new System.Windows.Forms.TextBox();
            this.justificationTxtBx = new System.Windows.Forms.TextBox();
            this.Header = new System.Windows.Forms.Label();
            this.makeRequest = new System.Windows.Forms.Button();
            this.viewRequest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // equipLbl
            // 
            this.equipLbl.AutoSize = true;
            this.equipLbl.Location = new System.Drawing.Point(43, 91);
            this.equipLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.equipLbl.Name = "equipLbl";
            this.equipLbl.Size = new System.Drawing.Size(157, 20);
            this.equipLbl.TabIndex = 0;
            this.equipLbl.Text = "Equipment Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 146);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Justification:";
            // 
            // equipNameTxt
            // 
            this.equipNameTxt.Location = new System.Drawing.Point(228, 91);
            this.equipNameTxt.Name = "equipNameTxt";
            this.equipNameTxt.Size = new System.Drawing.Size(344, 27);
            this.equipNameTxt.TabIndex = 2;
            // 
            // justificationTxtBx
            // 
            this.justificationTxtBx.Location = new System.Drawing.Point(228, 146);
            this.justificationTxtBx.Multiline = true;
            this.justificationTxtBx.Name = "justificationTxtBx";
            this.justificationTxtBx.Size = new System.Drawing.Size(766, 218);
            this.justificationTxtBx.TabIndex = 3;
            // 
            // Header
            // 
            this.Header.AutoSize = true;
            this.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.Location = new System.Drawing.Point(257, 9);
            this.Header.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(545, 52);
            this.Header.TabIndex = 4;
            this.Header.Text = "Make Equipment Request";
            // 
            // makeRequest
            // 
            this.makeRequest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.makeRequest.Location = new System.Drawing.Point(639, 461);
            this.makeRequest.Name = "makeRequest";
            this.makeRequest.Size = new System.Drawing.Size(289, 61);
            this.makeRequest.TabIndex = 5;
            this.makeRequest.Text = "Request";
            this.makeRequest.UseVisualStyleBackColor = true;
            this.makeRequest.Click += new System.EventHandler(this.makeRequest_Click);
            // 
            // viewRequest
            // 
            this.viewRequest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.viewRequest.Location = new System.Drawing.Point(170, 461);
            this.viewRequest.Name = "viewRequest";
            this.viewRequest.Size = new System.Drawing.Size(289, 61);
            this.viewRequest.TabIndex = 6;
            this.viewRequest.Text = "View Requests";
            this.viewRequest.UseVisualStyleBackColor = true;
            this.viewRequest.Click += new System.EventHandler(this.viewRequest_Click);
            // 
            // requestEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1092, 571);
            this.Controls.Add(this.viewRequest);
            this.Controls.Add(this.makeRequest);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.justificationTxtBx);
            this.Controls.Add(this.equipNameTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.equipLbl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "requestEquipment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "requestEquipment";
            this.Load += new System.EventHandler(this.requestEquipment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label equipLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox equipNameTxt;
        private System.Windows.Forms.TextBox justificationTxtBx;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.Button makeRequest;
        private System.Windows.Forms.Button viewRequest;
    }
}