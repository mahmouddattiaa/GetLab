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
     catch (Exception ex)
            {
            MessageBox.Show(
           $"Failed to initialize reservation form:\n\n{ex.Message}\n\nPlease check your database connection.",
     "Initialization Error",
        MessageBoxButtons.OK,
   MessageBoxIcon.Error);
                
     // Close the form if initialization failed
  this.Load += (s, e) => this.Close();
            }
      }

      private void studentsreservation_Load ( object sender, EventArgs e )
      {
        try
            {
         rbLab.Checked = true; // Default to Lab
                dtpDate.MinDate = DateTime.Now;
        LoadEquipmentList ( );
    }
       catch (Exception ex)
      {
MessageBox.Show(
        $"Error loading equipment list:\n\n{ex.Message}",
     "Load Error",
    MessageBoxButtons.OK,
      MessageBoxIcon.Error);
       }
            }

    // 1. Load Equipment based on Mode (Lab vs Storage)
        private void LoadEquipmentList ( )
{
         try
     {
     DataTable dt;
       if ( rbLab.Checked )
            {
                 dt = controller.GetAvailableLabEquipment ( );
  clbTimeSlots.Visible = true; // Show slots for Lab
   }
  else
   {
      dt = controller.GetAvailableStorageEquipment ( );
    clbTimeSlots.Visible = false; // Hide slots for Home
     }
     dataGridView1.DataSource = dt;
         
         // Auto-select first row if available
           if (dataGridView1.Rows.Count > 0)
      {
        dataGridView1.Rows[0].Selected = true;
      }
        }
            catch (Exception ex)
            {
 MessageBox.Show(
     $"Error loading equipment:\n\n{ex.Message}",
   "Database Error",
 MessageBoxButtons.OK,
       MessageBoxIcon.Error);
        }
       }

        // 2. Populate the Checklist with FREE hours
        private void UpdateSlots ( )
            {
 try
  {
       // Only run if in Lab Mode and an item is selected
     if ( !rbLab.Checked || dataGridView1.SelectedRows.Count == 0 ) return;

          clbTimeSlots.Items.Clear ( );
    int equipID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );
                DateTime date = dtpDate.Value.Date;

      // Get busy times from DB
  DataTable dtBusy = controller.GetEquipmentBusyTimes ( equipID, date );

     // Loop 8 AM to 7 PM
        for ( int hour = 8; hour < 19; hour++ )
      {
     bool isTaken = false;
         DateTime slotStart = date.AddHours ( hour );
                    DateTime slotEnd = date.AddHours ( hour + 1 );

             // Check overlap
   foreach ( DataRow row in dtBusy.Rows )
           {
 DateTime dbStart = Convert.ToDateTime ( row["StartTime"] );
       DateTime dbEnd = Convert.ToDateTime ( row["EndTime"] );
             if ( slotStart < dbEnd && slotEnd > dbStart ) { isTaken = true; break; }
  }

                // Add to list if free
         if ( !isTaken )
           {
           clbTimeSlots.Items.Add ( new TimeSlotItem { Hour = hour, Display = $"{hour:00}:00 - {hour + 1:00}:00" } );
             }
           }
        clbTimeSlots.DisplayMember = "Display";
        }
            catch (Exception ex)
     {
       MessageBox.Show(
            $"Error updating time slots:\n\n{ex.Message}",
     "Error",
          MessageBoxButtons.OK,
       MessageBoxIcon.Warning);
            }
      }

        // Helper class for the list items
   private class TimeSlotItem { public int Hour { get; set; } public string Display { get; set; } }

        // Events
        private void dataGridView1_SelectionChanged ( object sender, EventArgs e ) { UpdateSlots ( ); }
   private void dtpDate_ValueChanged ( object sender, EventArgs e ) { UpdateSlots ( ); }
        private void rbLab_CheckedChanged ( object sender, EventArgs e ) { LoadEquipmentList ( ); }
        private void rbHome_CheckedChanged ( object sender, EventArgs e ) { LoadEquipmentList ( ); }

        // 3. Reserve Button (The Merge Logic)
        private void reserveBtn_Click ( object sender, EventArgs e )
   {
            try
      {
        if ( dataGridView1.SelectedRows.Count == 0 ) { MessageBox.Show ( "Select an item." ); return; }
    int equipID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );
       DateTime date = dtpDate.Value.Date;

   if ( rbLab.Checked )
       {
        // --- LAB MODE ---
    if ( clbTimeSlots.CheckedItems.Count == 0 ) { MessageBox.Show ( "Select at least one time slot." ); return; }

          // Get checked hours
           List<int> hours = new List<int> ( );
                    foreach ( TimeSlotItem item in clbTimeSlots.CheckedItems ) hours.Add ( item.Hour );
     hours.Sort ( );

              // Merge Logic: Group 9, 10, 11 into 9-12
   for ( int i = 0; i < hours.Count; i++ )
        {
     int startH = hours[i];
             int endH = startH + 1;

     // Look ahead for consecutive hours
                while ( i + 1 < hours.Count && hours[i + 1] == endH )
             {
      endH++;
            i++;
      }

     // Save this block
   controller.ReserveSlot ( loggedInUserID, equipID, date.AddHours ( startH ), date.AddHours ( endH ) );
     }
    MessageBox.Show ( "Reservation Successful!" );
            UpdateSlots ( ); // Refresh list
         }
     else
{
     // --- TAKE HOME MODE ---
     if ( date <= DateTime.Now.Date ) { MessageBox.Show ( "Take-home must be for tomorrow or later." ); return; }

  // Reuse the old ReserveEquipment logic (Daily)
     controller.ReserveEquipment ( loggedInUserID, equipID, date );
              MessageBox.Show ( "Item Borrowed!" );
            LoadEquipmentList ( );
          }
      }
     catch (Exception ex)
            {
       MessageBox.Show(
      $"Error making reservation:\n\n{ex.Message}",
             "Reservation Error",
          MessageBoxButtons.OK,
           MessageBoxIcon.Error);
}
            }
        }
    }