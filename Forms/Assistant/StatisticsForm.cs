using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;
// IMPORTANT: Add this 'using' statement for charts!
using System.Windows.Forms.DataVisualization.Charting;

namespace GetLab.Forms.Assistant
    {
    public partial class StatisticsForm : Form
        {
        private ControllerClass controller;

        public StatisticsForm ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

        private void StatisticsForm_Load ( object sender, EventArgs e )
            {
            LoadPopularityChart ( );
            LoadStatusChart ( );
            }

        
        private void LoadPopularityChart ( )
            {
            try
                {
                DataTable dt = controller.GetMostReservedEquipment ( );
                chartPopularity.DataSource = dt;

                // Clear any default series
                chartPopularity.Series.Clear ( );

                // Create a new series
                Series series = new Series ( "Popularity" )
                    {
                    ChartType = SeriesChartType.Bar // Bar Chart
                    };

                
                series.XValueMember = "EquipmentName";
                series.YValueMembers = "ReservationCount";

                chartPopularity.Series.Add ( series );

                // Add a title
                chartPopularity.Titles.Add ( "Most Popular Equipment" );

                // Refresh the chart
                chartPopularity.DataBind ( );
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading popularity chart: " + ex.Message );
                }
            }

        // --- CHART 2: PIE CHART ---
        private void LoadStatusChart ( )
            {
            try
                {
                DataTable dt = controller.GetEquipmentStatusCount ( );
                chartStatus.DataSource = dt;

                chartStatus.Series.Clear ( );

                Series series = new Series ( "Status" )
                    {
                    ChartType = SeriesChartType.Pie // Pie Chart
                    };

                series.XValueMember = "CurrentStatus";
                series.YValueMembers = "StatusCount";
                series["PieLabelStyle"] = "Outside"; // Make labels readable

                chartStatus.Series.Add ( series );
                chartStatus.Titles.Add ( "Equipment Status Breakdown" );

                chartStatus.DataBind ( );
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading status chart: " + ex.Message );
                }
            }
        }
    }