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
            // Setup DatePicker
            timePicker.MinDate = DateTime.Now;

            // Load Labs (Only Rooms where Type = 'Lab')
            LoadLabs ( );
            }

        private void LoadLabs ( )
            {
            DataTable dt = controller.GetLocationsList ( );

            // Filter to show only Labs
            DataView dv = new DataView ( dt );
            dv.RowFilter = "RoomType = 'Lab'";

            comboBox1.DataSource = dv;
            comboBox1.DisplayMember = "RoomName";
            comboBox1.ValueMember = "LocationID";
            }

        // --- THE SLOT LOGIC ---
        private void UpdateSlots ( )
            {
            if ( comboBox1.SelectedValue == null ) return;

            clbSlots.Items.Clear ( ); // Clear previous slots

            // 1. Get Selected Lab and Date
            // (Use TryParse to handle potential casting errors safely)
            int locID;
            if ( !int.TryParse ( comboBox1.SelectedValue.ToString ( ), out locID ) ) return;

            DateTime date = timePicker.Value.Date;

            // 2. Get Busy Times from DB (Using the SP we created earlier)
            DataTable dtBusy = controller.GetRoomBusyTimes ( locID, date );

            // 3. Generate Hours (8 AM to 7 PM)
            for ( int hour = 8; hour < 19; hour++ )
                {
                bool isTaken = false;
                DateTime slotStart = date.AddHours ( hour );
                DateTime slotEnd = date.AddHours ( hour + 1 );

                // Check for overlaps
                if ( dtBusy != null )
                    {
                    foreach ( DataRow row in dtBusy.Rows )
                        {
                        DateTime dbStart = Convert.ToDateTime ( row["StartTime"] );
                        DateTime dbEnd = Convert.ToDateTime ( row["EndTime"] );

                        // If the slot overlaps with a reservation, mark as taken
                        if ( slotStart < dbEnd && slotEnd > dbStart )
                            {
                            isTaken = true;
                            break;
                            }
                        }
                    }

                // Only add to list if FREE
                if ( !isTaken )
                    {
                    string label = $"{hour:00}:00 - {hour + 1:00}:00";
                    clbSlots.Items.Add ( new TimeSlotItem { Hour = hour, Display = label } );
                    }
                }
            clbSlots.DisplayMember = "Display";
            }

        // Helper Class for List Items
        private class TimeSlotItem { public int Hour { get; set; } public string Display { get; set; } }

        // EVENTS: Update slots when Lab or Date changes
        private void comboBox1_SelectedIndexChanged ( object sender, EventArgs e ) { UpdateSlots ( ); }
        private void timePicker_ValueChanged ( object sender, EventArgs e ) { UpdateSlots ( ); }

        // --- RESERVE BUTTON ---
        private void reserveBtn_Click ( object sender, EventArgs e )
            {
            // Validation
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

            // Get Checked Hours
            List<int> hours = new List<int> ( );
            foreach ( TimeSlotItem item in clbSlots.CheckedItems )
                {
                hours.Add ( item.Hour );
                }
            hours.Sort ( ); // Ensure they are in order (9, 10, 11)

            // MERGE LOGIC: Combine consecutive hours into one reservation
            int successCount = 0;
            for ( int i = 0; i < hours.Count; i++ )
                {
                int startH = hours[i];
                int endH = startH + 1;

                // Check next items to see if they are consecutive
                while ( i + 1 < hours.Count && hours[i + 1] == endH )
                    {
                    endH++; // Extend the block
                    i++;    // Skip next
                    }

                DateTime start = date.AddHours ( startH );
                DateTime end = date.AddHours ( endH );

                // Call Controller
                bool result = controller.ReserveRoom ( currentUserID, locID, start, end, txtPurpose.Text );
                if ( result ) successCount++;
                }

            if ( successCount > 0 )
                {
                MessageBox.Show ( "Lab Reserved Successfully!" );
                UpdateSlots ( ); // Refresh to hide taken slots
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
            // Assuming you have a Welcome_Professor form
            Welcome_Professor welcome = new Welcome_Professor ( currentUserID, "Teacher" );
            welcome.Show ( );
            }
        }
    }