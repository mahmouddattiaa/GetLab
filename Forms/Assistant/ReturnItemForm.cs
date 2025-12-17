using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Assistant
    {
    public partial class ReturnItemForm : Form
        {
        private ControllerClass controller;
        private DataTable dtReservations;

        public ReturnItemForm ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

        // 1. LOAD: Setup the Grid and Dropdown
        private void ReturnItemForm_Load ( object sender, EventArgs e )
            {
            try
                {
                // Setup Dropdown
                cmbCondition.Items.Clear ( );
                cmbCondition.Items.Add ( "Good" );
                cmbCondition.Items.Add ( "Damaged" );
                cmbCondition.SelectedIndex = 0; // Auto-select "Good"

                // Setup Grid Styling (Flexibility)
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.ReadOnly = true; // Prevent user from editing grid directly
                dataGridView1.AllowUserToAddRows = false;

                // Load Data
                LoadReservations ( );
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading form: " + ex.Message );
                }
            }

        private void LoadReservations ( )
            {
            dtReservations = controller.GetAllActiveReservations ( );
            dataGridView1.DataSource = dtReservations;
            }

        // 2. CLICK-TO-FILL: The part that wasn't working
        private void dataGridView1_CellClick ( object sender, DataGridViewCellEventArgs e )
            {
            // Validation: Ignore clicks on the header row (-1)
            if ( e.RowIndex >= 0 )
                {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Safety Check: Does the column exist?
                if ( row.Cells["EquipmentID"].Value != null )
                    {
                    txtEquipmentID.Text = row.Cells["EquipmentID"].Value.ToString ( );
                    }
                }
            }

        // 3. FILTERING: Search as you type
        private void txtEquipmentID_TextChanged ( object sender, EventArgs e )
            {
            if ( dtReservations == null ) return;

            try
                {
                string searchText = txtEquipmentID.Text.Trim ( );

                if ( string.IsNullOrEmpty ( searchText ) )
                    {
                    dtReservations.DefaultView.RowFilter = ""; // Reset filter
                    }
                else
                    {
                    // Filter logic: Check if ID starts with the typed number
                    dtReservations.DefaultView.RowFilter = $"Convert(EquipmentID, 'System.String') LIKE '{searchText}%'";
                    }
                }
            catch ( Exception )
                {
                // Ignore filter errors (e.g. if user types weird symbols)
                }
            }

        // 4. RETURN BUTTON: The Logic with Validations
        

        private void ReturnItemForm_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }

        private void btnReturn_Click ( object sender, EventArgs e )
            {
            // VALIDATION 1: Is the box empty?
            if ( string.IsNullOrWhiteSpace ( txtEquipmentID.Text ) )
                {
                MessageBox.Show ( "❌ Please enter or select an Equipment ID.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            // VALIDATION 2: Is it a number?
            int equipID;
            if ( !int.TryParse ( txtEquipmentID.Text, out equipID ) )
                {
                MessageBox.Show ( "❌ Equipment ID must be a valid number.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            // VALIDATION 3: Did they verify the condition?
            if ( cmbCondition.SelectedItem == null )
                {
                MessageBox.Show ( "❌ Please select the condition (Good/Damaged).", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            // VALIDATION 4: Confirmation Dialog
            DialogResult confirm = MessageBox.Show (
                $"Are you sure you want to return Equipment #{equipID} as '{cmbCondition.SelectedItem}'?",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question );

            if ( confirm == DialogResult.No ) return;

            // EXECUTION
            try
                {
                bool success = controller.ReturnEquipment ( equipID, cmbCondition.SelectedItem.ToString ( ) );

                if ( success )
                    {
                    MessageBox.Show ( "✅ Return Processed Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );

                    // Clear and Refresh
                    txtEquipmentID.Clear ( );
                    LoadReservations ( );
                    }
                else
                    {
                    MessageBox.Show ( "❌ Failed. This item is NOT currently borrowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Database Error Details:\n" + ex.Message, "Debug Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

        private void cmbCondition_SelectedIndexChanged ( object sender, EventArgs e )
            {

            }
        }
    }