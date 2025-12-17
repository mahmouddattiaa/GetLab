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


namespace GetLab.Forms.Authentication
    {
    public partial class login : GetLab.Forms.BaseForm
        {
        private ControllerClass controller;
   public login ( )
          {
      InitializeComponent ( );
       controller = new ControllerClass ( );
            
    // Set password masking
      passBX.PasswordChar = '*';
      }

        private void label1_Click(object sender, EventArgs e)
        {

   }

  private void login_Load ( object sender, EventArgs e )
     {

         }

   private void loginBT_Click(object sender, EventArgs e)
     {
   try
            {
      string universityID = idBX.Text.Trim();
             string password = passBX.Text;

   // Validate input
    if (string.IsNullOrEmpty(universityID))
 {
       ShowWarning("Please enter your University ID.", "Validation Error");
   idBX.Focus();
          return;
  }

if (string.IsNullOrEmpty(password))
     {
    ShowWarning("Please enter your password.", "Validation Error");
     passBX.Focus();
      return;
     }

     // Check if user exists
      if (!controller.CheckUserExists(universityID))
   {
  ShowError("User ID not found.", "Login Failed");
   return;
  }

       // Validate password
    DataTable result = controller.ValidatePassword(universityID, password);

     if (result.Rows.Count > 0)
  {
          string role = result.Rows[0]["UserRole"].ToString();
   string userName = result.Rows[0]["FullName"].ToString();

   ShowSuccess($"Welcome, {userName}!", "Login Successful");

 // Navigate based on role using FormHelper
            GetLab.Helpers.FormHelper.NavigateBasedOnRole(role, userName, universityID, this);
      }
   else
      {
         ShowError("Invalid password.", "Login Failed");
     }
    }
  catch (Exception ex)
   {
     HandleException(ex, "login");
     }
   }

        protected override void OnFormClosing(FormClosingEventArgs e)
{
            // Clean up database connection when form closes
            controller?.TerminateConnection();
  base.OnFormClosing(e);
     }

        private void button2_Click ( object sender, EventArgs e )
            {
            Create create = new Create();
            create.Show(this);
            this.Close();
            }
        }
    }
