using System;
using System.Windows.Forms;
using GetLab.Forms.Authentication;
using GetLab.Forms.Student;
using GetLab.Forms.Professor;
using GetLab.Forms.Assistant;

namespace GetLab.Helpers
{
    /// <summary>
    /// Helper class for form navigation throughout the application
    /// </summary>
    public static class FormHelper
    {
        /// <summary>
  /// Navigate to the appropriate dashboard based on user role
        /// </summary>
        /// <param name="role">User's role (Student, Professor, Assistant, Admin)</param>
     /// <param name="userName">User's name for personalization</param>
      /// <param name="userId">User's ID</param>
    /// <param name="currentForm">The form to close/hide</param>
    public static void NavigateBasedOnRole(string role, string userName, string userId, Form currentForm)
        {
            Form targetForm = null;

     switch (role?.ToLower())
            {
  case "student":
   targetForm = new Welcome_student();
        break;
            
  case "professor":
   targetForm = new Welcome_Professor();
  break;
 
  case "assistant":
     targetForm = new Welcome_Assistant();
break;
 
      case "admin":
            // TODO: Create admin dashboard
  MessageBox.Show("Admin dashboard not yet implemented.", "Info", 
    MessageBoxButtons.OK, MessageBoxIcon.Information);
         return;
       
        default:
    MessageBox.Show($"Unknown user role: {role}", "Error", 
               MessageBoxButtons.OK, MessageBoxIcon.Error);
           return;
      }

        if (targetForm != null)
            {
        targetForm.Show();
  currentForm.Hide();
   }
        }

        /// <summary>
   /// Logout and return to login form
        /// </summary>
        /// <param name="currentForm">The form to close</param>
        public static void Logout(Form currentForm)
        {
     var result = MessageBox.Show(
 "Are you sure you want to logout?", 
      "Confirm Logout", 
     MessageBoxButtons.YesNo, 
          MessageBoxIcon.Question);

       if (result == DialogResult.Yes)
            {
      var loginForm = new login();
           loginForm.Show();
         currentForm.Close();
            }
 }

        /// <summary>
 /// Navigate to student reservation form
    /// </summary>
    public static void OpenStudentReservation(Form currentForm)
        {
            var reservationForm = new studentsreservation();
  reservationForm.Show();
  currentForm.Hide();
        }

        /// <summary>
        /// Navigate to my reservations form
   /// </summary>
 public static void OpenMyReservations(Form currentForm)
        {
        var myReservationsForm = new MyReservations();
            myReservationsForm.Show();
    currentForm.Hide();
 }

        /// <summary>
   /// Navigate to submit report form
   /// </summary>
     public static void OpenSubmitReport(Form currentForm)
{
            var submitReportForm = new submitreport();
        submitReportForm.Show();
            currentForm.Hide();
    }

      /// <summary>
      /// Close the application
    /// </summary>
        public static void ExitApplication()
        {
   var result = MessageBox.Show(
                "Are you sure you want to exit?", 
            "Confirm Exit", 
    MessageBoxButtons.YesNo, 
 MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
            {
        Application.Exit();
            }
        }
    }
}
