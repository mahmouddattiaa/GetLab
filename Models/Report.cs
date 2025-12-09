using System;

namespace GetLab.Models
{
    /// <summary>
    /// Represents a lab report in the system
    /// </summary>
    public class Report
 {
        public int ReportID { get; set; }
   public int ReservationID { get; set; }
        public string SubmittedBy { get; set; }
        public string ReportTitle { get; set; }
   public string ReportDescription { get; set; }
 public string IssuesFound { get; set; }
 public string EquipmentStatus { get; set; }
        public DateTime SubmittedDate { get; set; }
      public string Status { get; set; }  // Submitted, Reviewed, Resolved
  public string ReviewedBy { get; set; }
  public DateTime? ReviewedDate { get; set; }

        /// <summary>
    /// Check if report has been reviewed
    /// </summary>
        public bool IsReviewed => Status?.Equals("Reviewed", StringComparison.OrdinalIgnoreCase) ?? false;

     /// <summary>
     /// Check if issues have been resolved
     /// </summary>
  public bool IsResolved => Status?.Equals("Resolved", StringComparison.OrdinalIgnoreCase) ?? false;

        /// <summary>
/// Check if report was submitted recently (within last 24 hours)
     /// </summary>
        public bool IsRecent => (DateTime.Now - SubmittedDate).TotalHours <= 24;

  public override string ToString()
    {
return $"{ReportTitle} - Submitted by {SubmittedBy} on {SubmittedDate:yyyy-MM-dd}";
  }
    }
}
