namespace GetLab.Forms.Assistant
    {
    partial class StatisticsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartPopularity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPopularity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPopularity
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPopularity.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPopularity.Legends.Add(legend1);
            this.chartPopularity.Location = new System.Drawing.Point(2, 2);
            this.chartPopularity.Name = "chartPopularity";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPopularity.Series.Add(series1);
            this.chartPopularity.Size = new System.Drawing.Size(465, 566);
            this.chartPopularity.TabIndex = 0;
            this.chartPopularity.Text = "chartPopularity";
            // 
            // chartStatus
            // 
            chartArea2.Name = "ChartArea1";
            this.chartStatus.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartStatus.Legends.Add(legend2);
            this.chartStatus.Location = new System.Drawing.Point(473, 2);
            this.chartStatus.Name = "chartStatus";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartStatus.Series.Add(series2);
            this.chartStatus.Size = new System.Drawing.Size(636, 566);
            this.chartStatus.TabIndex = 1;
            this.chartStatus.Text = "chart2";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1121, 668);
            this.Controls.Add(this.chartStatus);
            this.Controls.Add(this.chartPopularity);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartPopularity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatus)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPopularity;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatus;
        }
    }