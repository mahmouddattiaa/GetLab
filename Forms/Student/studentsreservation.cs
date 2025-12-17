using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Student
    {
    public partial class studentsreservation : Form
        {
        private ControllerClass controller;
        private string loggedInUserID;

        public studentsreservation ( string userID )
            {
            InitializeComponent ( );
            try
                {
                controller = new ControllerClass ( );
                loggedInUserID = userID;
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Failed to initialize reservation form. Please check your database connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Load += ( s, e ) => this.Close ( );
                }
            }

        private void studentsreservation_Load ( object sender, EventArgs e )
            {
            try
                {
                rbLab.Checked = true;
                dtpDate.MinDate = DateTime.Now;
                LoadEquipmentList ( );
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading equipment list.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

        private void LoadEquipmentList ( )
            {
            try
                {
                DataTable dt;
                if ( rbLab.Checked )
                    {
                    dt = controller.GetAvailableLabEquipment ( );
                    clbTimeSlots.Visible = true;
                    }
                else
                    {
                    dt = controller.GetAvailableStorageEquipment ( );
                    clbTimeSlots.Visible = false;
                    }
                dataGridView1.DataSource = dt;

                if ( dataGridView1.Rows.Count > 0 )
                    {
                    dataGridView1.Rows[0].Selected = true;
                    }
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading equipment.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

        private void UpdateSlots ( )
            {
            try
                {
                if ( !rbLab.Checked || dataGridView1.SelectedRows.Count == 0 ) return;

                clbTimeSlots.Items.Clear ( );
                int equipID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );
                DateTime date = dtpDate.Value.Date;

                DataTable dtBusy = controller.GetEquipmentBusyTimes ( equipID, date );

                for ( int hour = 8; hour < 19; hour++ )
                    {
                    bool isTaken = false;
                    DateTime slotStart = date.AddHours ( hour );
                    DateTime slotEnd = date.AddHours ( hour + 1 );

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

                    if ( !isTaken )
                        {
                        clbTimeSlots.Items.Add ( new TimeSlotItem { Hour = hour, Display = $"{hour:00}:00 - {hour + 1:00}:00" } );
                        }
                    }
                clbTimeSlots.DisplayMember = "Display";
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error updating time slots.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                }
            }

        private class TimeSlotItem
            {
            public int Hour { get; set; }
            public string Display { get; set; }
            }

        private void dataGridView1_SelectionChanged ( object sender, EventArgs e )
            {
            UpdateSlots ( );
            }

        private void dtpDate_ValueChanged ( object sender, EventArgs e )
            {
            UpdateSlots ( );
            }

        private void rbLab_CheckedChanged ( object sender, EventArgs e )
            {
            LoadEquipmentList ( );
            }

        private void rbHome_CheckedChanged ( object sender, EventArgs e )
            {
            LoadEquipmentList ( );
            }

        private void reserveBtn_Click ( object sender, EventArgs e )
            {
            try
                {
                if ( dataGridView1.SelectedRows.Count == 0 )
                    {
                    MessageBox.Show ( "Please select an item." );
                    return;
                    }

                int equipID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );
                DateTime date = dtpDate.Value.Date;

                if ( rbLab.Checked )
                    {
                    if ( clbTimeSlots.CheckedItems.Count == 0 )
                        {
                        MessageBox.Show ( "Select at least one time slot." );
                        return;
                        }

                    List<int> hours = new List<int> ( );
                    foreach ( TimeSlotItem item in clbTimeSlots.CheckedItems )
                        {
                        hours.Add ( item.Hour );
                        }
                    hours.Sort ( );

                    for ( int i = 0; i < hours.Count; i++ )
                        {
                        int startH = hours[i];
                        int endH = startH + 1;

                        while ( i + 1 < hours.Count && hours[i + 1] == endH )
                            {
                            endH++;
                            i++;
                            }

                        controller.ReserveSlot ( loggedInUserID, equipID, date.AddHours ( startH ), date.AddHours ( endH ) );
                        }

                    MessageBox.Show ( "Reservation Successful!" );
                    UpdateSlots ( );
                    }
                else
                    {
                    if ( date <= DateTime.Now.Date )
                        {
                        MessageBox.Show ( "Take-home must be for tomorrow or later." );
                        return;
                        }

                    controller.ReserveEquipment ( loggedInUserID, equipID, date );
                    MessageBox.Show ( "Item Borrowed!" );
                    LoadEquipmentList ( );
                    }
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error making reservation. Please try again.", "Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }
    }