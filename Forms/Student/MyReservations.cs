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

namespace GetLab.Forms.Student
{
    public partial class MyReservations : GetLab.Forms.BaseForm
    {
        private ControllerClass controller;
        private string loggedInUniID;

        public MyReservations(string ID)
        {
            InitializeComponent();
            controller = new ControllerClass();
            loggedInUniID = ID;
        }

        private void MyReservations_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = controller.GetMyReservations(loggedInUniID);
                dgvHistory.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    ShowWarning("You have no reservations yet.", "No Reservations");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "loading reservations");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            controller?.TerminateConnection();
            base.OnFormClosing(e);
        }
    }
}
