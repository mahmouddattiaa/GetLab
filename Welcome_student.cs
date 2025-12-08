using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetLab
{
    public partial class Welcome_student : Form
    {
        public string ID;
        public Welcome_student( string uniID )
        {
             ID = uniID;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentsreservation s1 = new studentsreservation(ID);
            s1.Show ( );
        }

        private void Welcome_student_Load ( object sender, EventArgs e )
            {

            }
        }
}
