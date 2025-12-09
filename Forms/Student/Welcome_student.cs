using System;
using System.Windows.Forms;

namespace GetLab.Forms.Student
    {
    // We keep 'BaseForm' here just so the Designer doesn't break, 
    // but inside we will use standard code.
    public partial class Welcome_student : GetLab.Forms.BaseForm
        {
        private string loggedInUserID;

        public Welcome_student ( string uniID )
            {
            InitializeComponent ( );
            loggedInUserID = uniID; // Store the ID passed from Login
            }

        private void Welcome_student_Load ( object sender, EventArgs e )
            {
            // Optional: You can change the window title
            this.Text = "Student Dashboard - " + loggedInUserID;
            }

        // Button: Go to Reservation
        private void button1_Click ( object sender, EventArgs e )
            {
            // This is the standard way to open a form:
            studentsreservation resForm = new studentsreservation ( loggedInUserID );
            resForm.Show ( );
            // this.Hide(); // Uncomment this if you want the Welcome screen to disappear
            }

        // Button: View My History
        private void viewReservationsBT_Click ( object sender, EventArgs e )
            {
            // Standard way to open a form:
            MyReservations historyForm = new MyReservations ( loggedInUserID );
            historyForm.Show ( );
            }
        }
    }