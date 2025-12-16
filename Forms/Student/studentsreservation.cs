using System;
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
            controller = new ControllerClass ( );
            loggedInUserID = userID;
            }

        private void studentsreservation_Load ( object sender, EventArgs e )
            {
            // Default: Load Lab Equipment
            rbLab.Checked = true;
            LoadData ( );

            // Set DatePicker format to show Time for Lab mode
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            }

        // Helper to load correct data based on selection
        private void LoadData ( )
            {
            DataTable dt;
            if ( rbLab.Checked )
                {
                dt = controller.GetAvailableLabEquipment ( );
                // Hint for user
                dateTimePicker1.CustomFormat = "HH:mm"; // Show only time for today
                }
            else
                {
                dt = controller.GetAvailableStorageEquipment ( );
                // Hint for user
                dateTimePicker1.CustomFormat = "dd/MM/yyyy"; // Show full date
                }

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }

        // EVENT: Radio Button Changed (Switching Modes)
        private void rbLab_CheckedChanged ( object sender, EventArgs e )
            {
            LoadData ( );
            }

        private void rbHome_CheckedChanged ( object sender, EventArgs e )
            {
            LoadData ( );
            }

        // EVENT: Reserve Button
        private void reserveBtn_Click ( object sender, EventArgs e )
            {
            // 1. Selection Validation
            if ( dataGridView1.SelectedRows.Count == 0 )
                {
                MessageBox.Show ( "Please select an item." );
                return;
                }

            DateTime selectedDate = dateTimePicker1.Value;
            int equipmentID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );

            // 2. LOGIC VALIDATION (The Rules)
            if ( rbLab.Checked )
                {
                // RULE: Must be TODAY
                if ( selectedDate.Date != DateTime.Now.Date )
                    {
                    MessageBox.Show ( "Lab reservations must be for TODAY only." );
                    return;
                    }

                // RULE: Must be before 7 PM (19:00)
                if ( selectedDate.Hour >= 19 )
                    {
                    MessageBox.Show ( "The Lab closes at 7:00 PM. Please select an earlier time." );
                    return;
                    }

                // RULE: Must be in the future (can't reserve for 2 hours ago)
                if ( selectedDate < DateTime.Now )
                    {
                    MessageBox.Show ( "Time must be in the future." );
                    return;
                    }
                }
            else // rbHome.Checked
                {
                // RULE: Must be in the future
                if ( selectedDate.Date <= DateTime.Now.Date )
                    {
                    MessageBox.Show ( "Take-home borrowing must be for at least one day (Tomorrow or later)." );
                    return;
                    }
                }

            // 3. Execute
            bool success = controller.ReserveEquipment ( loggedInUserID, equipmentID, selectedDate );

            if ( success )
                {
                MessageBox.Show ( "Reservation Successful!" );
                LoadData ( ); // Refresh grid
                }
            else
                {
                MessageBox.Show ( "Reservation Failed. Item might be taken." );
                }
            }

        // Search Logic (Works for both lists)
        private void txtSearch_TextChanged ( object sender, EventArgs e )
            {
            // Note: If you want strict searching on the filtered list, 
            // it's better to filter the DataTable directly in C# here.
            // But for now, calling the DB search is okay, though it searches ALL items.
            // A quick fix for the UI:
            ( dataGridView1.DataSource as DataTable ).DefaultView.RowFilter = $"EquipmentName LIKE '%{txtSearch.Text}%'";
            }

        private void studentsreservation_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }
        }
    }