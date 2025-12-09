using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetLab.Forms.Student
    {
    public partial class MyReservations : GetLab.Forms.BaseForm
        {
        private ControllerClass controller;
        private string loggedInUniID;
        public MyReservations (string ID )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            loggedInUniID = ID;
            }

        private void MyReservations_Load ( object sender, EventArgs e )
            {
            DataTable dt = controller.GetMyReservations ( loggedInUniID );
            dgvHistory.DataSource = dt;
            }
        }
    }
