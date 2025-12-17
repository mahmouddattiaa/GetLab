using GetLab.Forms.Authentication;
using System;
using System.Windows.Forms;

namespace GetLab.Forms.Student
{
    public partial class Welcome_student : GetLab.Forms.BaseForm
    {
        private string loggedInUserID;

        public Welcome_student(string uniID)
        {
            InitializeComponent();
            loggedInUserID = uniID; // Store the ID passed from Login
        }

        private void Welcome_student_Load(object sender, EventArgs e)
        {
            this.Text = "Student Dashboard - " + loggedInUserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentsreservation resForm = new studentsreservation(loggedInUserID);
            resForm.Show();
        }

        private void viewReservationsBT_Click(object sender, EventArgs e)
        {
            MyReservations historyForm = new MyReservations(loggedInUserID);
            historyForm.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            submitreport reportForm = new submitreport(this.loggedInUserID, false);
            reportForm.Show();
        }

        private void Welcome_student_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            loginForm.Show();

            // 3. Close this dashboard
            this.Close();
        }
    }
}