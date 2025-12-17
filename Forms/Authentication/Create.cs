using System;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Authentication
    {
    public partial class Create : Form
        {
        private ControllerClass controller;

        public Create ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

        private void btnSignup_Click ( object sender, EventArgs e )
            {
            // 1. Validation
            if ( string.IsNullOrWhiteSpace ( txtName.Text ) ||
                string.IsNullOrWhiteSpace ( txtEmail.Text ) ||
                string.IsNullOrWhiteSpace ( txtID.Text ) ||
                string.IsNullOrWhiteSpace ( txtPassword.Text ) )
                {
                MessageBox.Show ( "Please fill in all fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
                }

            // 2. Call Controller
            int result = controller.RegisterUser (
                txtName.Text.Trim ( ),
                txtEmail.Text.Trim ( ),
                txtID.Text.Trim ( ),
                txtPassword.Text
            );

            // 3. Handle Result
            if ( result == 1 )
                {
                MessageBox.Show ( "Account Created Successfully! You can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );

                // --- THE CHANGE IS HERE ---

                // 1. Create a new instance of the Login form
                // (Make sure 'login' matches the exact class name of your login form)
                login loginForm = new login ( );

                // 2. Show the Login form
                loginForm.Show ( );

                // 3. Close this Create form
                this.Close ( );

                // --------------------------
                }
            else if ( result == -1 )
                {
                MessageBox.Show ( "This University ID is already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            else if ( result == -2 )
                {
                MessageBox.Show ( "This Email is already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            else
                {
                MessageBox.Show ( "Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

        // Optional: Link "Back to Login" label if you have one
        private void lblBackToLogin_Click ( object sender, EventArgs e )
            {
            this.Close ( );
            }
        }
    }