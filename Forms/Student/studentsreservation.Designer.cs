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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.reserveBtn = new System.Windows.Forms.Button();
            this.rbLab = new System.Windows.Forms.RadioButton();
            this.rbHome = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(369, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 85);
            this.label1.TabIndex = 4;
            this.label1.Text = "Reservation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Uighur", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(34, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 62);
            this.label2.TabIndex = 5;
            this.label2.Text = "Equipment";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(266, 155);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(197, 26);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 210);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(562, 323);
            this.dataGridView1.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(714, 180);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(274, 26);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Uighur", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(735, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 62);
            this.label3.TabIndex = 9;
            this.label3.Text = "Return Date";
            // 
            // reserveBtn
            // 
            this.reserveBtn.Font = new System.Drawing.Font("Microsoft Uighur", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reserveBtn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.reserveBtn.Location = new System.Drawing.Point(398, 540);
            this.reserveBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reserveBtn.Name = "reserveBtn";
            this.reserveBtn.Size = new System.Drawing.Size(214, 67);
            this.reserveBtn.TabIndex = 13;
            this.reserveBtn.Text = "Reserve";
            this.reserveBtn.UseVisualStyleBackColor = true;
            this.reserveBtn.Click += new System.EventHandler(this.reserveBtn_Click);
            // 
            // rbLab
            // 
            this.rbLab.AutoSize = true;
            this.rbLab.Checked = true;
            this.rbLab.Location = new System.Drawing.Point(538, 150);
            this.rbLab.Name = "rbLab";
            this.rbLab.Size = new System.Drawing.Size(177, 24);
            this.rbLab.TabIndex = 14;
            this.rbLab.TabStop = true;
            this.rbLab.Text = "Work in Lab (Hourly)";
            this.rbLab.UseVisualStyleBackColor = true;
            this.rbLab.CheckedChanged += new System.EventHandler(this.rbLab_CheckedChanged);
            // 
            // rbHome
            // 
            this.rbHome.AutoSize = true;
            this.rbHome.Location = new System.Drawing.Point(538, 180);
            this.rbHome.Name = "rbHome";
            this.rbHome.Size = new System.Drawing.Size(164, 24);
            this.rbHome.TabIndex = 15;
            this.rbHome.Text = "Take Home (Daily)";
            this.rbHome.UseVisualStyleBackColor = true;
            this.rbHome.CheckedChanged += new System.EventHandler(this.rbHome_CheckedChanged);
            // 
            // studentsreservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::GetLab.Properties.Resources.LOGOOO1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1175, 710);
            this.Controls.Add(this.rbHome);
            this.Controls.Add(this.rbLab);
            this.Controls.Add(this.reserveBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "studentsreservation";
            this.Text = "studentsreservation";
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
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button reserveBtn;
        private System.Windows.Forms.RadioButton rbLab;
        private System.Windows.Forms.RadioButton rbHome;
        }
    }