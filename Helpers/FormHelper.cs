using System;
using System.Windows.Forms;
using GetLab.Forms.Authentication;
using GetLab.Forms.Student;
using GetLab.Forms.Professor;
using GetLab.Forms.Assistant;

namespace GetLab.Helpers
    {
    public static class FormHelper
        {
        // This is just a big "Switch" statement to open the right dashboard
        public static void NavigateBasedOnRole ( string role, string userName, string userId, Form currentForm )
            {
            Form targetForm = null;

            // I added .ToLower() so "Student" and "student" both work
            switch ( role?.ToLower ( ) )
                {
                case "student":
                    targetForm = new Welcome_student ( userId );
                    break;

                case "teacher": // Changed from "professor" to match your Database Role
                case "professor":
                    // FIX: Pass the userId here too!
                    targetForm = new Welcome_Professor ( userId, role );
                    break;

                case "admin":
                case "assistant":
                    // FIX: Pass the userId here too!
                    targetForm = new Welcome_Assistant ( userId );
                    break;

                default:
                    MessageBox.Show ( "Role not recognized: " + role );
                    return;
                }

            if ( targetForm != null )
                {
                targetForm.Show ( );
                currentForm.Hide ( );
                }
            }

        // This just opens the Reservation Form
        public static void OpenStudentReservation ( string userId, Form currentForm )
            {
            // It passes the ID correctly here
            var reservationForm = new studentsreservation ( userId );
            reservationForm.Show ( );
            currentForm.Hide ( ); // Or currentForm.Close() if you want
            }

        // This handles the Logout logic
        public static void Logout ( Form currentForm )
            {
            var loginForm = new login ( );
            loginForm.Show ( );
            currentForm.Close ( );
            }
        }
    }