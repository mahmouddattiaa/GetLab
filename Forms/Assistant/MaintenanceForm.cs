using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Assistant
    {
    public partial class MaintenanceForm : Form
        {
        private ControllerClass controller;

        public MaintenanceForm ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

        private void MaintenanceForm_Load ( object sender, EventArgs e )
            {
            LoadDamagedItems ( );
            }

        private void LoadDamagedItems ( )
            {
            try
                {
                DataTable dt = controller.GetDamagedItems ( );
                dgvMaintenance.DataSource = dt;

                // Styling
                dgvMaintenance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMaintenance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMaintenance.MultiSelect = false;

                if ( dt.Rows.Count == 0 )
                    {
                    MessageBox.Show ( "Great! No equipment is currently damaged." );
                    }
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Error loading maintenance items." );
                }
            }

        private void btnFix_Click ( object sender, EventArgs e )
            {
            // Validation: Did they select a row?
            if ( dgvMaintenance.SelectedRows.Count == 0 )
                {
                MessageBox.Show ( "Please select an item to fix." );
                return;
                }

            // Get ID
            int equipID = Convert.ToInt32 ( dgvMaintenance.SelectedRows[0].Cells["EquipmentID"].Value );
            string name = dgvMaintenance.SelectedRows[0].Cells["EquipmentName"].Value.ToString ( );

            // Confirmation
            DialogResult result = MessageBox.Show (
                $"Are you sure '{name}' (ID: {equipID}) is fixed and ready for use?",
                "Confirm Repair",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question );

            if ( result == DialogResult.Yes )
                {
                bool success = controller.FixEquipment ( equipID );
                if ( success )
                    {
                    MessageBox.Show ( "Item marked as Available!" );
                    LoadDamagedItems ( ); // Refresh the list
                    }
                else
                    {
                    MessageBox.Show ( "Error updating status." );
                    }
                }
            }

        private void btnClose_Click ( object sender, EventArgs e )
            {
            this.Close ( );
            }

        private void MaintenanceForm_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }
        }
    }