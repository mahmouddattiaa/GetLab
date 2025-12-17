namespace GetLab.Forms.Student
    {
    partial class studentsreservation
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.reserveBtn = new System.Windows.Forms.Button();
            this.rbLab = new System.Windows.Forms.RadioButton();
            this.rbHome = new System.Windows.Forms.RadioButton();
            this.clbTimeSlots = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(286, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 65);
            this.label1.TabIndex = 4;
            this.label1.Text = "Reservation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(30, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "Equipment";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(225, 131);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(373, 30);
            this.txtSearch.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 185);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(609, 375);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Location = new System.Drawing.Point(770, 197);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(328, 30);
            this.dtpDate.TabIndex = 8;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(765, 163);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "Return Date";
            // 
            // reserveBtn
            // 
            this.reserveBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.reserveBtn.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reserveBtn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.reserveBtn.Location = new System.Drawing.Point(337, 583);
            this.reserveBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reserveBtn.Name = "reserveBtn";
            this.reserveBtn.Size = new System.Drawing.Size(420, 77);
            this.reserveBtn.TabIndex = 13;
            this.reserveBtn.Text = "Reserve";
            this.reserveBtn.UseVisualStyleBackColor = true;
            this.reserveBtn.Click += new System.EventHandler(this.reserveBtn_Click);
            // 
            // rbLab
            // 
            this.rbLab.AutoSize = true;
            this.rbLab.Checked = true;
            this.rbLab.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLab.ForeColor = System.Drawing.Color.White;
            this.rbLab.Location = new System.Drawing.Point(630, 131);
            this.rbLab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbLab.Name = "rbLab";
            this.rbLab.Size = new System.Drawing.Size(216, 27);
            this.rbLab.TabIndex = 14;
            this.rbLab.TabStop = true;
            this.rbLab.Text = "Work in Lab (Hourly)";
            this.rbLab.UseVisualStyleBackColor = true;
            this.rbLab.CheckedChanged += new System.EventHandler(this.rbLab_CheckedChanged);
            // 
            // rbHome
            // 
            this.rbHome.AutoSize = true;
            this.rbHome.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHome.ForeColor = System.Drawing.Color.White;
            this.rbHome.Location = new System.Drawing.Point(885, 131);
            this.rbHome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbHome.Name = "rbHome";
            this.rbHome.Size = new System.Drawing.Size(197, 27);
            this.rbHome.TabIndex = 15;
            this.rbHome.Text = "Take Home (Daily)";
            this.rbHome.UseVisualStyleBackColor = true;
            this.rbHome.CheckedChanged += new System.EventHandler(this.rbHome_CheckedChanged);
            // 
            // clbTimeSlots
            // 
            this.clbTimeSlots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbTimeSlots.CheckOnClick = true;
            this.clbTimeSlots.ColumnWidth = 120;
            this.clbTimeSlots.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbTimeSlots.FormattingEnabled = true;
            this.clbTimeSlots.Location = new System.Drawing.Point(630, 185);
            this.clbTimeSlots.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbTimeSlots.MultiColumn = true;
            this.clbTimeSlots.Name = "clbTimeSlots";
            this.clbTimeSlots.Size = new System.Drawing.Size(139, 229);
            this.clbTimeSlots.TabIndex = 17;
            // 
            // studentsreservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1124, 683);
            this.Controls.Add(this.clbTimeSlots);
            this.Controls.Add(this.rbHome);
            this.Controls.Add(this.rbLab);
            this.Controls.Add(this.reserveBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(839, 739);
            this.Name = "studentsreservation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Equipment Reservation";
            this.Load += new System.EventHandler(this.studentsreservation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button reserveBtn;
        private System.Windows.Forms.RadioButton rbLab;
        private System.Windows.Forms.RadioButton rbHome;
        private System.Windows.Forms.CheckedListBox clbTimeSlots;
        }
    }