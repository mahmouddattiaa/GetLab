namespace GetLab.Forms.Professor
{
    partial class teacherReservation
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.combobxLbl = new System.Windows.Forms.Label();
            this.makeReserTeacherGrid = new System.Windows.Forms.DataGridView();
            this.reserveBtn = new System.Windows.Forms.Button();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.makeReserTeacherGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(132, 39);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(301, 30);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // combobxLbl
            // 
            this.combobxLbl.AutoSize = true;
            this.combobxLbl.Font = new System.Drawing.Font("Microsoft Uighur", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combobxLbl.ForeColor = System.Drawing.Color.Transparent;
            this.combobxLbl.Location = new System.Drawing.Point(23, 36);
            this.combobxLbl.Name = "combobxLbl";
            this.combobxLbl.Size = new System.Drawing.Size(69, 39);
            this.combobxLbl.TabIndex = 13;
            this.combobxLbl.Text = "Labs:";
            // 
            // makeReserTeacherGrid
            // 
            this.makeReserTeacherGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.makeReserTeacherGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.makeReserTeacherGrid.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.makeReserTeacherGrid.Location = new System.Drawing.Point(30, 125);
            this.makeReserTeacherGrid.Name = "makeReserTeacherGrid";
            this.makeReserTeacherGrid.RowHeadersWidth = 51;
            this.makeReserTeacherGrid.RowTemplate.Height = 24;
            this.makeReserTeacherGrid.Size = new System.Drawing.Size(904, 283);
            this.makeReserTeacherGrid.TabIndex = 14;
            this.makeReserTeacherGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.makeReserTeacherGrid_CellContentClick_1);
            // 
            // reserveBtn
            // 
            this.reserveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reserveBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.reserveBtn.Location = new System.Drawing.Point(239, 439);
            this.reserveBtn.Name = "reserveBtn";
            this.reserveBtn.Size = new System.Drawing.Size(472, 57);
            this.reserveBtn.TabIndex = 17;
            this.reserveBtn.Text = "Reserve";
            this.reserveBtn.UseVisualStyleBackColor = true;
            this.reserveBtn.Click += new System.EventHandler(this.reserveBtn_Click);
            // 
            // timePicker
            // 
            this.timePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker.Location = new System.Drawing.Point(697, 39);
            this.timePicker.Name = "timePicker";
            this.timePicker.Size = new System.Drawing.Size(200, 28);
            this.timePicker.TabIndex = 18;
            this.timePicker.ValueChanged += new System.EventHandler(this.timePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(510, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 39);
            this.label1.TabIndex = 19;
            this.label1.Text = "Return Date:";
            // 
            // teacherReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(972, 532);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timePicker);
            this.Controls.Add(this.reserveBtn);
            this.Controls.Add(this.makeReserTeacherGrid);
            this.Controls.Add(this.combobxLbl);
            this.Controls.Add(this.comboBox1);
            this.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.Name = "teacherReservation";
            this.Text = "teacherReservation";
            this.Load += new System.EventHandler(this.teacherReservation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.makeReserTeacherGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label combobxLbl;
        private System.Windows.Forms.DataGridView makeReserTeacherGrid;
        private System.Windows.Forms.Button reserveBtn;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Label label1;
    }
}