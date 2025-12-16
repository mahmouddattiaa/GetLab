namespace GetLab.Forms.Professor
{
    partial class myRequests
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
            this.showReqGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteReqBtn = new System.Windows.Forms.Button();
            this.noOfReq = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.showReqGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // showReqGrid
            // 
            this.showReqGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.showReqGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.showReqGrid.Location = new System.Drawing.Point(27, 99);
            this.showReqGrid.Name = "showReqGrid";
            this.showReqGrid.RowHeadersWidth = 51;
            this.showReqGrid.RowTemplate.Height = 24;
            this.showReqGrid.Size = new System.Drawing.Size(1023, 366);
            this.showReqGrid.TabIndex = 0;
            this.showReqGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.showReqGrid_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(373, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 52);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Requests";
            // 
            // deleteReqBtn
            // 
            this.deleteReqBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.deleteReqBtn.Location = new System.Drawing.Point(132, 486);
            this.deleteReqBtn.Name = "deleteReqBtn";
            this.deleteReqBtn.Size = new System.Drawing.Size(253, 58);
            this.deleteReqBtn.TabIndex = 2;
            this.deleteReqBtn.Text = "Delete Request";
            this.deleteReqBtn.UseVisualStyleBackColor = true;
            this.deleteReqBtn.Click += new System.EventHandler(this.deleteReqBtn_Click);
            // 
            // noOfReq
            // 
            this.noOfReq.AutoSize = true;
            this.noOfReq.Location = new System.Drawing.Point(807, 504);
            this.noOfReq.Name = "noOfReq";
            this.noOfReq.Size = new System.Drawing.Size(0, 22);
            this.noOfReq.TabIndex = 3;
            // 
            // myRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1092, 571);
            this.Controls.Add(this.noOfReq);
            this.Controls.Add(this.deleteReqBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showReqGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "myRequests";
            this.Text = "myRequests";
            this.Load += new System.EventHandler(this.myRequests_Load);
            ((System.ComponentModel.ISupportInitialize)(this.showReqGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView showReqGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteReqBtn;
        private System.Windows.Forms.Label noOfReq;
    }
}