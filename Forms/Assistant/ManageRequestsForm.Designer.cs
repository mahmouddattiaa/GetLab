namespace GetLab.Forms.Assistant
    {
    partial class ManageRequestsForm
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
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnDeny = new System.Windows.Forms.Button();
            this.Requests = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRequests
            // 
            this.dgvRequests.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Location = new System.Drawing.Point(131, 52);
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.RowHeadersWidth = 62;
            this.dgvRequests.RowTemplate.Height = 28;
            this.dgvRequests.Size = new System.Drawing.Size(545, 238);
            this.dgvRequests.TabIndex = 0;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.Green;
            this.btnApprove.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnApprove.Location = new System.Drawing.Point(729, 59);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(133, 55);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnDeny
            // 
            this.btnDeny.BackColor = System.Drawing.Color.Crimson;
            this.btnDeny.Location = new System.Drawing.Point(729, 140);
            this.btnDeny.Name = "btnDeny";
            this.btnDeny.Size = new System.Drawing.Size(133, 55);
            this.btnDeny.TabIndex = 2;
            this.btnDeny.Text = "Deny";
            this.btnDeny.UseVisualStyleBackColor = false;
            this.btnDeny.Click += new System.EventHandler(this.btnDeny_Click);
            // 
            // Requests
            // 
            this.Requests.AutoSize = true;
            this.Requests.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Requests.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Requests.ForeColor = System.Drawing.Color.Cornsilk;
            this.Requests.Location = new System.Drawing.Point(2, 59);
            this.Requests.Name = "Requests";
            this.Requests.Size = new System.Drawing.Size(123, 29);
            this.Requests.TabIndex = 3;
            this.Requests.Text = "Requests";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(25, 541);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(147, 63);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ManageRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1084, 652);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.Requests);
            this.Controls.Add(this.btnDeny);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.dgvRequests);
            this.Name = "ManageRequestsForm";
            this.Text = "ManageRequestsForm";
            this.Load += new System.EventHandler(this.ManageRequestsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnDeny;
        private System.Windows.Forms.Label Requests;
        private System.Windows.Forms.Button btnClose;
        }
    }