using System;

namespace GetLab.Models
{
    /// <summary>
    /// Represents a user in the system
    /// </summary>
    public class User
    {
        public string UniversityID { get; set; }
        public string UserName { get; set; }
 public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Check if user is a student
        /// </summary>
        public bool IsStudent => Role?.Equals("Student", StringComparison.OrdinalIgnoreCase) ?? false;

   /// <summary>
        /// Check if user is a professor
        /// </summary>
        public bool IsProfessor => Role?.Equals("Professor", StringComparison.OrdinalIgnoreCase) ?? false;

      /// <summary>
     /// Check if user is an assistant
      /// </summary>
        public bool IsAssistant => Role?.Equals("Assistant", StringComparison.OrdinalIgnoreCase) ?? false;

        /// <summary>
      /// Check if user is an admin
        /// </summary>
        public bool IsAdmin => Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;

        /// <summary>
        /// Get display name with role
 /// </summary>
        public string DisplayNameWithRole => $"{UserName} ({Role})";

        public override string ToString()
        {
          return $"{UniversityID} - {UserName} ({Role})";
  }
    }
}
