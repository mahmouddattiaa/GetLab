using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Professor
    {
    public partial class teacherReservation : Form
        {
        private string currentUserID;
        private ControllerClass controller;

        public teacherReservation ( string userID )
            {
            InitializeComponent ( );
            this.currentUserID = userID;
            controller = new ControllerClass ( );
            }

        private void teacherReservation_Load ( object sender, EventArgs e )
            {
            timePicker.MinDate = DateTime.Now;
            LoadLabs ( );
            }

        private void LoadLabs ( )
            {
            DataTable dt = controller.GetLocationsList ( );

            DataView dv = new DataView ( dt );
            dv.RowFilter = "RoomType = 'Lab'";

            comboBox1.DataSource = dv;
            comboBox1.DisplayMember = "RoomName";
            comboBox1.ValueMember = "LocationID";
            }

        private void UpdateSlots ( )
            {
            if ( comboBox1.SelectedValue == null ) return;

            clbSlots.Items.Clear ( );

            int locID;
            if ( !int.TryParse ( comboBox1.SelectedValue.ToString ( ), out locID ) ) return;

            DateTime date = timePicker.Value.Date;

            DataTable dtBusy = controller.GetRoomBusyTimes ( locID, date );
            for ( int hour = 8; hour < 19; hour++ )
                {
                bool isTaken = false;
                DateTime slotStart = date.AddHours ( hour );
                DateTime slotEnd = date.AddHours ( hour + 1 );

                if ( dtBusy != null )
                    {
                    foreach ( DataRow row in dtBusy.Rows )
                        {
                        DateTime dbStart = Convert.ToDateTime ( row["StartTime"] );
                        DateTime dbEnd = Convert.ToDateTime ( row["EndTime"] );

                        if ( slotStart < dbEnd && slotEnd > dbStart )
                            {
                            isTaken = true;
                            break;
                            }
                        }
                    }

                if ( !isTaken )
                    {
                    string label = $"{hour:00}:00 - {hour + 1:00}:00";
                    clbSlots.Items.Add ( new TimeSlotItem { Hour = hour, Display = label } );
                    }
                }
            clbSlots.DisplayMember = "Display";
            }

        private class TimeSlotItem { public int Hour { get; set; } public string Display { get; set; } }

        private void comboBox1_SelectedIndexChanged ( object sender, EventArgs e ) { UpdateSlots ( ); }
        private void timePicker_ValueChanged ( object sender, EventArgs e ) { UpdateSlots ( ); }

        private void reserveBtn_Click ( object sender, EventArgs e )
            {
            if ( clbSlots.CheckedItems.Count == 0 )
                {
                MessageBox.Show ( "Please select at least one time slot." );
                return;
                }
            if ( string.IsNullOrWhiteSpace ( txtPurpose.Text ) )
                {
                MessageBox.Show ( "Please enter a Purpose (e.g., 'Electronics Class')." );
                return;
                }

            int locID = Convert.ToInt32 ( comboBox1.SelectedValue );
            DateTime date = timePicker.Value.Date;

            List<int> hours = new List<int> ( );
            foreach ( TimeSlotItem item in clbSlots.CheckedItems )
                {
                hours.Add ( item.Hour );
                }
            hours.Sort ( );
            int successCount = 0;
            for ( int i = 0; i < hours.Count; i++ )
                {
                int startH = hours[i];
                int endH = startH + 1;

                while ( i + 1 < hours.Count && hours[i + 1] == endH )
                    {
                    endH++;
                    i++;
                    }

                DateTime start = date.AddHours ( startH );
                DateTime end = date.AddHours ( endH );

                bool result = controller.ReserveRoom ( currentUserID, locID, start, end, txtPurpose.Text );
                if ( result ) successCount++;
                }

            if ( successCount > 0 )
                {
                MessageBox.Show ( "Lab Reserved Successfully!" );
                UpdateSlots ( );
                txtPurpose.Clear ( );
                }
            else
                {
                MessageBox.Show ( "Reservation Failed. Please try again." );
                }
            }

        private void homeBtn_Click ( object sender, EventArgs e )
            {
            this.Close ( );
            Welcome_Professor welcome = new Welcome_Professor ( currentUserID, "Teacher" );
            welcome.Show ( );
            }
        }
    }