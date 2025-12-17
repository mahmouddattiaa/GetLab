namespace GetLab.Forms.Professor
{
    partial class viewLabs
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
            this.viewEquipmentProfGrid = new System.Windows.Forms.DataGridView();
            this.showAvailEquipments = new System.Windows.Forms.Button();
            this.showReservedEquipments = new System.Windows.Forms.Button();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.viewEquipmentProfGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // viewEquipmentProfGrid
            // 
            this.viewEquipmentProfGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.viewEquipmentProfGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewEquipmentProfGrid.Location = new System.Drawing.Point(30, 298);
            this.viewEquipmentProfGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.viewEquipmentProfGrid.Name = "viewEquipmentProfGrid";
            this.viewEquipmentProfGrid.RowHeadersWidth = 51;
            this.viewEquipmentProfGrid.RowTemplate.Height = 24;
            this.viewEquipmentProfGrid.Size = new System.Drawing.Size(1173, 381);
            this.viewEquipmentProfGrid.TabIndex = 0;
            this.viewEquipmentProfGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // showAvailEquipments
            // 
            this.showAvailEquipments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showAvailEquipments.Location = new System.Drawing.Point(30, 195);
            this.showAvailEquipments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showAvailEquipments.Name = "showAvailEquipments";
            this.showAvailEquipments.Size = new System.Drawing.Size(539, 75);
            this.showAvailEquipments.TabIndex = 1;
            this.showAvailEquipments.Text = "Show Available Labs";
            this.showAvailEquipments.UseVisualStyleBackColor = true;
            this.showAvailEquipments.Click += new System.EventHandler(this.showAvailEquipments_Click);
            // 
            // showReservedEquipments
            // 
            this.showReservedEquipments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showReservedEquipments.Location = new System.Drawing.Point(676, 195);
            this.showReservedEquipments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showReservedEquipments.Name = "showReservedEquipments";
            this.showReservedEquipments.Size = new System.Drawing.Size(539, 75);
            this.showReservedEquipments.TabIndex = 2;
            this.showReservedEquipments.Text = "Show Reserved Labs";
            this.showReservedEquipments.UseVisualStyleBackColor = true;
            this.showReservedEquipments.Click += new System.EventHandler(this.showReservedEquipments_Click);
            // 
            // searchBar
            // 
            this.searchBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBar.Location = new System.Drawing.Point(573, 34);
            this.searchBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(530, 39);
            this.searchBar.TabIndex = 4;
            this.searchBar.TextChanged += new System.EventHandler(this.searchBar_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(433, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search :";
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Location = new System.Drawing.Point(573, 95);
            this.search.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(531, 71);
            this.search.TabIndex = 6;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::GetLab.Properties.Resources.home;
            this.button1.Location = new System.Drawing.Point(12, 21);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 68);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Image = global::GetLab.Properties.Resources.reserve;
            this.button2.Location = new System.Drawing.Point(93, 34);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 49);
            this.button2.TabIndex = 10;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // viewLabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1228, 714);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.showReservedEquipments);
            this.Controls.Add(this.showAvailEquipments);
            this.Controls.Add(this.viewEquipmentProfGrid);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "viewLabs";
            this.Text = "viewEquipments";
            this.Load += new System.EventHandler(this.viewEquipments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.viewEquipmentProfGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView viewEquipmentProfGrid;
        private System.Windows.Forms.Button showAvailEquipments;
        private System.Windows.Forms.Button showReservedEquipments;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        }
}