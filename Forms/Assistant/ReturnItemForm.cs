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

        private void ReturnItemForm_Load ( object sender, EventArgs e )
            {
            try
                {
                cmbCondition.Items.Clear ( );
                cmbCondition.Items.Add ( "Good" );
                cmbCondition.Items.Add ( "Damaged" );
                cmbCondition.SelectedIndex = 0;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;

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

        private void dataGridView1_CellClick ( object sender, DataGridViewCellEventArgs e )
            {
            if ( e.RowIndex >= 0 )
                {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if ( row.Cells["EquipmentID"].Value != null )
                    {
                    txtEquipmentID.Text = row.Cells["EquipmentID"].Value.ToString ( );
                    }
                }
            }

        private void txtEquipmentID_TextChanged ( object sender, EventArgs e )
            {
            if ( dtReservations == null ) return;

            try
                {
                string searchText = txtEquipmentID.Text.Trim ( );

                if ( string.IsNullOrEmpty ( searchText ) )
                    {
                    dtReservations.DefaultView.RowFilter = "";
                    }
                else
                    {
                    dtReservations.DefaultView.RowFilter = $"Convert(EquipmentID, 'System.String') LIKE '{searchText}%'";
                    }
                }
            catch ( Exception )
                {
                }
            }

        private void ReturnItemForm_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }

        private void btnReturn_Click ( object sender, EventArgs e )
            {
            if ( string.IsNullOrWhiteSpace ( txtEquipmentID.Text ) )
                {
                MessageBox.Show ( "❌ Please enter or select an Equipment ID.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            int equipID;
            if ( !int.TryParse ( txtEquipmentID.Text, out equipID ) )
                {
                MessageBox.Show ( "❌ Equipment ID must be a valid number.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            if ( cmbCondition.SelectedItem == null )
                {
                MessageBox.Show ( "❌ Please select the condition (Good/Damaged).", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            DialogResult confirm = MessageBox.Show (
                $"Are you sure you want to return Equipment #{equipID} as '{cmbCondition.SelectedItem}'?",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question );

            if ( confirm == DialogResult.No ) return;

            try
                {
                bool success = controller.ReturnEquipment ( equipID, cmbCondition.SelectedItem.ToString ( ) );

                if ( success )
                    {
                    MessageBox.Show ( "✅ Return Processed Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
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
                MessageBox.Show ( "An error occurred while processing the return. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

        private void cmbCondition_SelectedIndexChanged ( object sender, EventArgs e )
            {

            }
        }
    }