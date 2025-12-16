using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Assistant
    {
    public partial class ManageLocationsForm : Form
        {
        private ControllerClass controller;

        public ManageLocationsForm ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

        private void ManageLocationsForm_Load ( object sender, EventArgs e )
            {
            // 1. Setup the Dropdown (Enforcing Option A)
            cmbType.Items.Clear ( );
            cmbType.Items.Add ( "Lab" );
            cmbType.Items.Add ( "Storage" );

            // CRITICAL: Prevent typing. User MUST pick from the list.
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbType.SelectedIndex = 0; // Default to "Lab"

            // 2. Load the list
            LoadLocations ( );
            }

        private void LoadLocations ( )
            {
            DataTable dt = controller.GetLocationsList ( );
            dgvLocations.DataSource = dt;
            dgvLocations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        private void btnAdd_Click ( object sender, EventArgs e )
            {
            // Validation
            if ( string.IsNullOrWhiteSpace ( txtRoomName.Text ) )
                {
                MessageBox.Show ( "Please enter a Room Name." );
                return;
                }

            string name = txtRoomName.Text.Trim ( );
            string type = cmbType.SelectedItem.ToString ( ); // Will be "Lab" or "Storage"
            int capacity = Convert.ToInt32( numCapacity.Text.ToString());

            // Call Controller
            bool success = controller.AddLocation ( name, type, capacity );

            if ( success )
                {
                MessageBox.Show ( "Location Added Successfully!" );
                txtRoomName.Clear ( );
                LoadLocations ( ); // Refresh the grid immediately
                }
            else
                {
                MessageBox.Show ( "Error: A room with this name already exists." );
                }
            }

        private void ManageLocationsForm_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }
        }
    }