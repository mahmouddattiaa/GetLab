using System;
using System.Windows.Forms;

namespace GetLab.Forms
{
  /// <summary>
    /// Base form class that all forms should inherit from.
    /// Provides common functionality like navigation, error handling, etc.
    /// </summary>
    public class BaseForm : Form
    {
        /// <summary>
     /// Navigate to a new form
    /// </summary>
        /// <param name="newForm">The form to navigate to</param>
     /// <param name="closeCurrentForm">Whether to close the current form</param>
        protected void NavigateTo(Form newForm, bool closeCurrentForm = false)
        {
      if (newForm == null)
         throw new ArgumentNullException(nameof(newForm));

   newForm.Show();
            
if (closeCurrentForm)
            {
              this.Close();
}
         else
    {
        this.Hide();
         }
        }

     /// <summary>
      /// Show an error message to the user
    /// </summary>
    protected void ShowError(string message, string title = "Error")
        {
          MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

   /// <summary>
        /// Show a success message to the user
     /// </summary>
 protected void ShowSuccess(string message, string title = "Success")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
     }

        /// <summary>
        /// Show a warning message to the user
        /// </summary>
      protected void ShowWarning(string message, string title = "Warning")
        {
    MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
     /// Ask the user to confirm an action
 /// </summary>
        /// <returns>True if user clicked Yes, false otherwise</returns>
        protected bool ConfirmAction(string message, string title = "Confirm")
   {
        return MessageBox.Show(message, title, 
 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
  }

  /// <summary>
        /// Validate that a text input is not empty
        /// </summary>
        protected bool ValidateNotEmpty(TextBox textBox, string fieldName)
     {
    if (string.IsNullOrWhiteSpace(textBox.Text))
   {
            ShowWarning($"Please enter {fieldName}.", "Validation Error");
       textBox.Focus();
         return false;
        }
            return true;
        }

        /// <summary>
        /// Validate that a masked text box is not empty
  /// </summary>
   protected bool ValidateNotEmpty(MaskedTextBox maskedTextBox, string fieldName)
        {
     if (string.IsNullOrWhiteSpace(maskedTextBox.Text))
 {
   ShowWarning($"Please enter {fieldName}.", "Validation Error");
        maskedTextBox.Focus();
       return false;
    }
        return true;
        }

        /// <summary>
        /// Handle exceptions in a consistent way
        /// </summary>
        protected void HandleException(Exception ex, string operation)
        {
    string message = $"An error occurred during {operation}:\n\n{ex.Message}";
         
            if (ex.InnerException != null)
      {
            message += $"\n\nDetails: {ex.InnerException.Message}";
            }

     ShowError(message);
        }

        private void InitializeComponent ( )
            {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(803, 543);
            this.Name = "BaseForm";
            this.ResumeLayout(false);

            }
        }
}
