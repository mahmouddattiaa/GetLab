using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Assistant
    {
    public partial class AddEquipmentForm : Form
        {
        private ControllerClass controller;

        public AddEquipmentForm ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

        private void AddEquipmentForm_Load ( object sender, EventArgs e )
            {
            // Fill Dropdowns
            cmbSupplier.DataSource = controller.GetAllSuppliers ( );
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";

            cmbLocation.DataSource = controller.GetAllLocations ( );
            cmbLocation.DisplayMember = "RoomName";
            cmbLocation.ValueMember = "LocationID";
            }

        private void btnAdd_Click ( object sender, EventArgs e )
            {
            if ( string.IsNullOrWhiteSpace ( txtName.Text ) || string.IsNullOrWhiteSpace ( txtSerial.Text ) )
                {
                MessageBox.Show ( "Name and Serial Number are required." );
                return;
                }

            bool success = controller.AddEquipment (
                txtName.Text,
                txtModel.Text,
                txtSerial.Text,
                Convert.ToInt32 ( cmbSupplier.SelectedValue ),
                Convert.ToInt32 ( cmbLocation.SelectedValue )
            );

            if ( success )
                {
                MessageBox.Show ( "Equipment Added Successfully!" );
                this.Close ( );
                }
            else
                {
                MessageBox.Show ( "Error: Serial Number already exists." );
                }
            }
        }
    }