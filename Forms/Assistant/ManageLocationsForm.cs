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
            cmbType.Items.Clear ( );
            cmbType.Items.Add ( "Lab" );
            cmbType.Items.Add ( "Storage" );

            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.SelectedIndex = 0;

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
            if ( string.IsNullOrWhiteSpace ( txtRoomName.Text ) )
                {
                MessageBox.Show ( "Please enter a Room Name." );
                return;
                }

            string name = txtRoomName.Text.Trim ( );
            string type = cmbType.SelectedItem.ToString ( );
            int capacity = Convert.ToInt32( numCapacity.Text.ToString());
            bool success = controller.AddLocation ( name, type, capacity );

            if ( success )
                {
                MessageBox.Show ( "Location Added Successfully!" );
                txtRoomName.Clear ( );
                LoadLocations ( );
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