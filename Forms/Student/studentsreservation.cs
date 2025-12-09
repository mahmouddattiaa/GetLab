using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Student
    {
    // 1. Inherit from standard 'Form', not 'BaseForm'
    public partial class studentsreservation : Form
        {
        private ControllerClass controller;
        private string loggedInUserID;

        // Constructor: Accepts the ID passed from Welcome Screen
        public studentsreservation ( string userID )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            loggedInUserID = userID;
            }

        // Load: Show available equipment
        private void studentsreservation_Load ( object sender, EventArgs e )
            {
            try
                {
                DataTable dt = controller.GetAvailableEquipment ( );
                dataGridView1.DataSource = dt;

                // Standard Grid Formatting
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading equipment: " + ex.Message );
                }
            }

        // Search: Filter the list
        private void txtSearch_TextChanged ( object sender, EventArgs e )
            {
            DataTable dt = controller.SearchEquipment ( txtSearch.Text );
            dataGridView1.DataSource = dt;
            }

        // Reserve Button
        private void reserveBtn_Click ( object sender, EventArgs e )
            {
            // A. Validation: Selection
            if ( dataGridView1.SelectedRows.Count == 0 )
                {
                MessageBox.Show ( "Please select an item from the list first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            // B. Validation: Date
            if ( dateTimePicker1.Value <= DateTime.Now )
                {
                MessageBox.Show ( "Return date must be in the future.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            // C. Get Equipment ID
            // Note: Ensure your Grid actually has a column named "EquipmentID"
            int equipmentID = Convert.ToInt32 ( dataGridView1.SelectedRows[0].Cells["EquipmentID"].Value );
            string equipmentName = dataGridView1.SelectedRows[0].Cells["EquipmentName"].Value.ToString ( );

            // D. Confirmation Dialog (Standard C#)
            DialogResult result = MessageBox.Show ( $"Do you want to reserve {equipmentName}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question );

            if ( result == DialogResult.Yes )
                {
                // E. Call Controller
                bool isSuccess = controller.ReserveEquipment ( loggedInUserID, equipmentID, dateTimePicker1.Value );

                if ( isSuccess )
                    {
                    MessageBox.Show ( "Reservation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );

                    // Refresh the grid
                    dataGridView1.DataSource = controller.GetAvailableEquipment ( );
                    }
                else
                    {
                    MessageBox.Show ( "Reservation failed. The item might be taken.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }
            }

        // Clean up connection
        private void studentsreservation_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }
        }
    }