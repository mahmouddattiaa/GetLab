using GetLab.Forms.Authentication;
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
        public Welcome_Assistant( string userID )
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReturnItemForm returnForm = new ReturnItemForm ( );
            returnForm.Show ( );
            }

        private void Welcome_Assistant_Load ( object sender, EventArgs e )
            {

            }

        private void btnMaintenance_Click ( object sender, EventArgs e )
            {
            MaintenanceForm maintenanceForm = new MaintenanceForm ( );
            maintenanceForm.Show ( );
            }

        private void button1_Click ( object sender, EventArgs e )
            {
            AddEquipmentForm addEquipmentForm = new AddEquipmentForm ( );
            addEquipmentForm.Show ( );
            }

        private void btnLogout_Click ( object sender, EventArgs e )
            {
            // 1. Close the connection (Good practice)
            // controller.TerminateConnection(); // If you have a controller instance here

            // 2. Open Login Form
            // Assuming your login form is named 'LoginForm' or 'login'
            login loginForm = new login ( );
            loginForm.Show ( );

            // 3. Close this dashboard
            this.Close ( );
            }
        }
}
