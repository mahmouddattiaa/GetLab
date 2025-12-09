using System;
using System.Text.RegularExpressions;

namespace GetLab.Helpers
{
    /// <summary>
    /// Helper class for input validation
    /// </summary>
    public static class ValidationHelper
  {
        /// <summary>
        /// Validate email format
        /// </summary>
        public static bool IsValidEmail(string email)
{
       if (string.IsNullOrWhiteSpace(email))
    return false;

            try
            {
            // Basic email regex pattern
  string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
      return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
 }
     catch
            {
            return false;
     }
        }

    /// <summary>
        /// Validate University ID format (alphanumeric, specific length)
        /// </summary>
        /// <param name="universityId">The ID to validate</param>
        /// <param name="minLength">Minimum length (default: 3)</param>
        /// <param name="maxLength">Maximum length (default: 20)</param>
        public static bool IsValidUniversityId(string universityId, int minLength = 3, int maxLength = 20)
{
            if (string.IsNullOrWhiteSpace(universityId))
      return false;

  if (universityId.Length < minLength || universityId.Length > maxLength)
                return false;

  // Only alphanumeric characters
 return Regex.IsMatch(universityId, @"^[a-zA-Z0-9]+$");
        }

        /// <summary>
        /// Validate password strength
        /// </summary>
 /// <param name="password">The password to validate</param>
  /// <param name="minLength">Minimum length (default: 6)</param>
     public static bool IsValidPassword(string password, int minLength = 6)
        {
            if (string.IsNullOrWhiteSpace(password))
          return false;

  return password.Length >= minLength;
   }

      /// <summary>
        /// Validate password strength with specific requirements
     /// </summary>
  public static (bool isValid, string message) ValidatePasswordStrength(string password)
        {
  if (string.IsNullOrWhiteSpace(password))
      return (false, "Password cannot be empty.");

            if (password.Length < 8)
     return (false, "Password must be at least 8 characters long.");

       bool hasUpper = Regex.IsMatch(password, @"[A-Z]");
            bool hasLower = Regex.IsMatch(password, @"[a-z]");
     bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");

    if (!hasUpper)
            return (false, "Password must contain at least one uppercase letter.");

        if (!hasLower)
                return (false, "Password must contain at least one lowercase letter.");

  if (!hasDigit)
      return (false, "Password must contain at least one digit.");

     // Optional: Uncomment if you want to require special characters
            // if (!hasSpecial)
       //     return (false, "Password must contain at least one special character.");

         return (true, "Password is strong.");
        }

        /// <summary>
  /// Validate phone number format
  /// </summary>
    public static bool IsValidPhoneNumber(string phoneNumber)
      {
            if (string.IsNullOrWhiteSpace(phoneNumber))
              return false;

    // Remove common separators
   string cleaned = Regex.Replace(phoneNumber, @"[\s\-\(\)]", "");

 // Check if it's all digits and has reasonable length
       return Regex.IsMatch(cleaned, @"^\d{10,15}$");
}

        /// <summary>
        /// Validate that input contains only letters and spaces
        /// </summary>
        public static bool IsValidName(string name)
        {
    if (string.IsNullOrWhiteSpace(name))
   return false;

    return Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }

        /// <summary>
        /// Validate numeric input
        /// </summary>
        public static bool IsNumeric(string input)
     {
            if (string.IsNullOrWhiteSpace(input))
           return false;

        return int.TryParse(input, out _);
        }

        /// <summary>
        /// Validate date is not in the past
        /// </summary>
        public static bool IsValidFutureDate(DateTime date)
    {
            return date.Date >= DateTime.Now.Date;
        }

        /// <summary>
        /// Validate date is within a specific range
        /// </summary>
      public static bool IsDateInRange(DateTime date, DateTime minDate, DateTime maxDate)
    {
     return date.Date >= minDate.Date && date.Date <= maxDate.Date;
        }

/// <summary>
        /// Sanitize input to prevent SQL injection (additional safety layer)
        /// </summary>
        public static string SanitizeInput(string input)
        {
        if (string.IsNullOrWhiteSpace(input))
      return string.Empty;

 // Remove potentially dangerous characters
            // Note: This is just an extra layer - always use parameterized queries!
      return input.Replace("'", "''")
        .Replace(";", "")
           .Replace("--", "")
        .Replace("/*", "")
              .Replace("*/", "")
  .Replace("xp_", "")
         .Replace("sp_", "")
       .Trim();
        }
    }
}
