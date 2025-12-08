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


namespace GetLab
    {
    public partial class login : Form
   {
        private ControllerClass controller;
        public login ( )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            }

    private void label1_Click(object sender, EventArgs e)
      {

 }

 private void login_Load ( object sender, EventArgs e )
            {

      }

        private void loginBT_Click ( object sender, EventArgs e )
            {
            string uniID = idBX.Text.Trim ( );
            string password = passBX.Text;

            if ( string.IsNullOrEmpty ( uniID ) || string.IsNullOrEmpty ( password ) )
                {
                MessageBox.Show ( "Please enter both University ID and Password." );
                return;
                }

            // 1. Check if ID exists
            if ( controller.CheckUserExists ( uniID ) )
                {
                // 2. Validate Password
                DataTable userData = controller.ValidatePassword ( uniID, password );

                if ( userData != null && userData.Rows.Count > 0 )
                    {
                    
                    string role = userData.Rows[0]["UserRole"].ToString ( );
                    string name = userData.Rows[0]["FullName"].ToString ( );

                    MessageBox.Show ( "Welcome " + name );
                    switch ( role )
                        {
                        case "Student":
                            Welcome_student studentForm = new Welcome_student (  uniID);
                            studentForm.Show ( );
                            this.Hide ( ); 
                            break;

                        case "Teacher":
                            Welcome_Professor profForm = new Welcome_Professor ( );
                            profForm.Show ( );
                            this.Hide ( ); 
                            break;

                        case "Admin":
                            Welcome_Assistant adminForm = new Welcome_Assistant ( );
                            adminForm.Show ( );
                            this.Hide ( ); 
                            break;

                        default:
                            MessageBox.Show ( "Error: Role not recognized." );
                            break;
                        }
          
                    }
                else
                    {
                    MessageBox.Show ( "Incorrect Password." );
                    }
                }
            else
                {
                MessageBox.Show ( "University ID not found." );
                }
            }
        private void LoginForm_FormClosing ( object sender, FormClosingEventArgs e )
            {
            if ( controller != null )
                {
                controller.TerminateConnection ( );
                }
            }

        }
    }
