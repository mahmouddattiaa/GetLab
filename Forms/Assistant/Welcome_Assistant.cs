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
        }
}
