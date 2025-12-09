using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller; // Make sure to use your Controller namespace
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Student
    {
    public partial class studentsreservation : Form
        {
        private ControllerClass controller;
        private string loggedInUserID; // To store the ID of the student

        // 1. Modify Constructor to accept UserID
        public studentsreservation ( string userID )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            loggedInUserID = userID; // Save the ID for later
            }

        // 2. Form Load: Show available equipment in the Grid
        private void studentsreservation_Load ( object sender, EventArgs e )
            {
            // We need a function in Controller to get available items
            // Assuming you added 'GetAvailableEquipment' to Controller as discussed before
            DataTable dt = controller.GetAvailableEquipment( );

            // Bind the data to the gray box (DataGridView)
            // Note: Make sure your DataGridView is named 'dataGridView1' or change this name
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }

        // 3. Reserve Button Click


        private void txtSearch_TextChanged ( object sender, EventArgs e )
            {
            DataTable dt = controller.SearchEquipment ( txtSearch.Text );
            dataGridView1.DataSource = dt;
            }

        private void reserveBtn_Click ( object sender, EventArgs e )
            {
            // A. Validation: Did they select a row?
            if ( dataGridView1.SelectedRows.Count == 0 )
                {
                MessageBox.Show ( "Please select an item from the list first." );
                return;
                }

            // B. Validation: Is the date in the future?
            if ( dateTimePicker1.Value <= DateTime.Now )
                {
                MessageBox.Show ( "Return date must be in the future." );
                return;
                }

            // C. Get the EquipmentID from the selected row
            // We assume the column name in DB is "EquipmentID". 
            // If hidden, we access it via Cells["EquipmentID"] or index Cells[0]
            int equipmentID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );

            // D. Call Controller to Reserve
            bool isSuccess = controller.ReserveEquipment ( loggedInUserID, equipmentID, dateTimePicker1.Value );

            if ( isSuccess )
                {
                MessageBox.Show ( "Reservation Successful!" );

                // Refresh the grid so the borrowed item disappears
                DataTable dt = controller.GetAvailableEquipment ( );
                dataGridView1.DataSource = dt;
                }
            else
                {
                MessageBox.Show ( "Reservation Failed. The item might be taken." );
                }
            }
        }
    }