using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Student // Or whatever namespace your form is in
    {
    public partial class submitreport : Form
        {
        private ControllerClass controller;
        private string loggedInUserID;

        // Constructor accepts the User ID
        public submitreport ( string userID )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            loggedInUserID = userID;
            }

        private void submitreport_Load ( object sender, EventArgs e )
            {
            LoadMyEquipment ( );
            }

        private void LoadMyEquipment ( )
            {
            // 1. Get the items this user currently has borrowed
            DataTable dt = controller.GetMyReservations ( loggedInUserID );

            if ( dt != null && dt.Rows.Count > 0 )
                {
                // 2. Bind to ComboBox
                // DisplayMember: What the user SEES (Name)
                // ValueMember: What the code USES (ID)
                cmbEquipment.DataSource = dt;
                cmbEquipment.DisplayMember = "EquipmentName";
                cmbEquipment.ValueMember = "EquipmentID";
                }
            else
                {
                MessageBox.Show ( "You have no active items to report." );
                this.Close ( ); // Close if nothing to report
                }
            }

        private void btnSubmit_Click ( object sender, EventArgs e )
            {
            // Validation
            if ( cmbEquipment.SelectedValue == null )
                {
                MessageBox.Show ( "Please select an item." );
                return;
                }

            if ( string.IsNullOrWhiteSpace ( txtDescription.Text ) )
                {
                MessageBox.Show ( "Please describe the issue." );
                return;
                }

            // Get the ID from the hidden ValueMember
            int equipmentID = Convert.ToInt32 ( cmbEquipment.SelectedValue );
            string description = txtDescription.Text;

            // Call Controller
            bool success = controller.SubmitReport ( loggedInUserID, equipmentID, description );

            if ( success )
                {
                MessageBox.Show ( "Report submitted successfully! The item has been flagged for maintenance." );
                this.Close ( );
                }
            else
                {
                MessageBox.Show ( "Error submitting report." );
                }
            }

        private void submitreport_FormClosing ( object sender, FormClosingEventArgs e )
            {
            controller.TerminateConnection ( );
            }
        }
    }