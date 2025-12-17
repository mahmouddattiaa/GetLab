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
    public partial class requestEquipment : Form
    {
        string currentUserID;
        ControllerClass Controller = new ControllerClass();

        public requestEquipment(string currentUserID)
        {
            InitializeComponent();
            this.currentUserID = currentUserID;
        }

        private void requestEquipment_Load(object sender, EventArgs e)
        {

        }

        private void makeRequest_Click(object sender, EventArgs e)
        {
            string equipmentName = equipNameTxt.Text;
            string justification = justificationTxtBx.Text;
            int dt = Controller.SubmitEquipmentRequest(currentUserID, equipmentName, justification);
            if(dt > 0)
            {
                MessageBox.Show("Request Submitted Successfully");
                equipmentName = string.Empty;
                justification = string.Empty;
            }
        }

        private void viewRequest_Click(object sender, EventArgs e)
        {
            this.Close();
            myRequests myRequests = new myRequests(currentUserID);
            myRequests.Show();
        }
    }
}
