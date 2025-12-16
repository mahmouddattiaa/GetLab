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

namespace GetLab.Forms.Professor
{
    public partial class Welcome_Professor : GetLab.Forms.BaseForm
    {
        string loggedInUniID;
        public Welcome_Professor(string userID)
        {
            InitializeComponent();
            loggedInUniID = userID;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_Professor_Load ( object sender, EventArgs e )
            {

            }

        private void btnReport_Click ( object sender, EventArgs e )
            {
            submitreport reportForm = new submitreport ( this.loggedInUniID,false );
            reportForm.Show ( );
            }
        }
}
