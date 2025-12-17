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

        public submitreport(string userID, bool isAdminUser)
        {
            InitializeComponent();
            controller = new ControllerClass();
            loggedInUserID = userID;
            isAdmin = isAdminUser;
        }

        private void submitreport_Load(object sender, EventArgs e)
        {
            cmbEquipment.Visible = true;
            cmbEquipment.DropDownStyle = ComboBoxStyle.DropDown;
            cmbEquipment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEquipment.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (Controls.ContainsKey("txtEquipmentID"))
            {
                Controls["txtEquipmentID"].Visible = false;
            }

            if (isAdmin)
            {
                LoadAllEquipment();
            }
            else
            {
                LoadMyEquipment();
            }
        }

        private void LoadAllEquipment()
        {
            DataTable dt = controller.GetAllEquipmentList();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbEquipment.DataSource = dt;
                cmbEquipment.DisplayMember = "DisplayName";
                cmbEquipment.ValueMember = "EquipmentID";
            }
        }

        private void LoadMyEquipment()
        {
            DataTable dt = controller.GetMyReservations(loggedInUserID);

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbEquipment.DataSource = dt;
                cmbEquipment.DisplayMember = "EquipmentName";
                cmbEquipment.ValueMember = "EquipmentID";
            }
            else
            {
                MessageBox.Show("You have no active items to report.");
                if (!isAdmin) this.Close();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid item from the list.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please describe the issue.");
                return;
            }

            int equipmentID;
            try
            {
                equipmentID = Convert.ToInt32(cmbEquipment.SelectedValue);
            }
            catch
            {
                MessageBox.Show("Invalid selection. Please click an item from the dropdown list.");
                return;
            }
            bool success = controller.SubmitReport(loggedInUserID, equipmentID, txtDescription.Text);

            if (success)
            {
                MessageBox.Show("Report submitted successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error submitting report.");
            }
        }

        private void submitreport_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.TerminateConnection();
        }
    }
}