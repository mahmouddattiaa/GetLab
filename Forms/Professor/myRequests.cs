using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Professor
{
    public partial class myRequests : Form
    {
        string currentUserID;
        Controller.Controller Controller = new Controller.Controller();
        
        public myRequests(string currentUserID)
        {
            InitializeComponent();
            this.currentUserID = currentUserID;
        }
        
        private void myRequests_Load(object sender, EventArgs e)
        {
            showReqGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            showReqGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            showReqGrid.ReadOnly = true;
            showReqGrid.AllowUserToAddRows = false;
            showReqGrid.AllowUserToDeleteRows = false;
            showReqGrid.MultiSelect = false;
            DataTable dt = Controller.GetTeacherEquipmentRequests(currentUserID);
            showReqGrid.DataSource = dt;
            if (showReqGrid.Columns.Contains("Justification"))
            {
                showReqGrid.Columns["Justification"].Width = 275;
                showReqGrid.Columns["Justification"].HeaderText = "Justification";
            }
            if (showReqGrid.Columns.Contains("RequestID"))
            {
                showReqGrid.Columns["RequestID"].Width = 50;
            }

        }

        private void showReqGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deleteReqBtn_Click(object sender, EventArgs e)
        {
        }
    }
}
