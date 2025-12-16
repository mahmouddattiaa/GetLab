using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Student
    {
    public partial class submitreport : Form
        {
        private ControllerClass controller;
        private string loggedInUserID;
        private bool isAdmin;

        public submitreport ( string userID, bool isAdminUser )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            loggedInUserID = userID;
            isAdmin = isAdminUser;
            }

        private void submitreport_Load ( object sender, EventArgs e )
            {
            // 1. Configure the ComboBox to act like a Search Bar
            cmbEquipment.Visible = true;
            cmbEquipment.DropDownStyle = ComboBoxStyle.DropDown; // Allows typing!
            cmbEquipment.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Shows suggestions
            cmbEquipment.AutoCompleteSource = AutoCompleteSource.ListItems; // Suggests from the data we load

            // 2. Hide the manual text box if you still have it on the form
            // (You can delete it from the designer, but this ensures it's hidden)
            if ( Controls.ContainsKey ( "txtEquipmentID" ) )
                {
                Controls["txtEquipmentID"].Visible = false;
                }

            // 3. Load Data based on Role
            if ( isAdmin )
                {
                LoadAllEquipment ( ); // Admin sees everything
                }
            else
                {
                LoadMyEquipment ( ); // Student sees only their items
                }
            }

        private void LoadAllEquipment ( )
            {
            DataTable dt = controller.GetAllEquipmentList ( );

            if ( dt != null && dt.Rows.Count > 0 )
                {
                cmbEquipment.DataSource = dt;
                cmbEquipment.DisplayMember = "DisplayName"; // Shows "Oscilloscope - Tek (ID: 5)"
                cmbEquipment.ValueMember = "EquipmentID";   // Stores "5"
                }
            }

        private void LoadMyEquipment ( )
            {
            DataTable dt = controller.GetMyReservations ( loggedInUserID );

            if ( dt != null && dt.Rows.Count > 0 )
                {
                cmbEquipment.DataSource = dt;
                cmbEquipment.DisplayMember = "EquipmentName";
                cmbEquipment.ValueMember = "EquipmentID";
                }
            else
                {
                MessageBox.Show ( "You have no active items to report." );
                if ( !isAdmin ) this.Close ( ); // Only close for students
                }
            }

        private void btnSubmit_Click ( object sender, EventArgs e )
            {
            // Validation
            if ( cmbEquipment.SelectedValue == null )
                {
                MessageBox.Show ( "Please select a valid item from the list." );
                return;
                }

            if ( string.IsNullOrWhiteSpace ( txtDescription.Text ) )
                {
                MessageBox.Show ( "Please describe the issue." );
                return;
                }

            // Get the ID directly from the ComboBox (works for both Admin and Student now)
            int equipmentID;
            try
                {
                equipmentID = Convert.ToInt32 ( cmbEquipment.SelectedValue );
                }
            catch
                {
                MessageBox.Show ( "Invalid selection. Please click an item from the dropdown list." );
                return;
                }

            // Submit
            bool success = controller.SubmitReport ( loggedInUserID, equipmentID, txtDescription.Text );

            if ( success )
                {
                MessageBox.Show ( "Report submitted successfully!" );
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