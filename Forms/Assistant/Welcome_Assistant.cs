using GetLab.Forms.Authentication;
using GetLab.Forms.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetLab.Forms.Assistant
{
    public partial class Welcome_Assistant : GetLab.Forms.BaseForm
    {
        public string name;

        public Welcome_Assistant(string userID)
        {
            InitializeComponent();
            name = userID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReturnItemForm returnForm = new ReturnItemForm();
            returnForm.Show();
        }

        private void Welcome_Assistant_Load(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            MaintenanceForm maintenanceForm = new MaintenanceForm();
            maintenanceForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEquipmentForm addEquipmentForm = new AddEquipmentForm();
            addEquipmentForm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            loginForm.Show();
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            submitreport reprtForm = new submitreport(name, true);
            reprtForm.Show();
        }

        private void btnRoomManagement_Click(object sender, EventArgs e)
        {
            ManageLocationsForm locForm = new ManageLocationsForm();
            locForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageRequestsForm req = new ManageRequestsForm(name);
            req.Show();
        }

        private void btnStatistics_Click ( object sender, EventArgs e )
            {
            StatisticsForm statsForm = new StatisticsForm ( );
            statsForm.Show ( );
            }
        }
}
