using System;

namespace GetLab.Models
{
    /// <summary>
    /// Represents a lab reservation in the system
    /// </summary>
    public class Reservation
    {
        public int ReservationID { get; set; }
    public string UniversityID { get; set; }
        public string LabName { get; set; }
   public DateTime ReservationDate { get; set; }
    public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }  // Pending, Approved, Rejected, Completed
        public DateTime CreatedDate { get; set; }
     public string ApprovedBy { get; set; }

 /// <summary>
  /// Check if reservation is pending
   /// </summary>
    public bool IsPending => Status?.Equals("Pending", StringComparison.OrdinalIgnoreCase) ?? false;

        /// <summary>
    /// Check if reservation is approved
 /// </summary>
   public bool IsApproved => Status?.Equals("Approved", StringComparison.OrdinalIgnoreCase) ?? false;

        /// <summary>
     /// Check if reservation is in the future
 /// </summary>
        public bool IsFuture => ReservationDate.Date > DateTime.Now.Date ||
            (ReservationDate.Date == DateTime.Now.Date && StartTime > DateTime.Now.TimeOfDay);

   /// <summary>
        /// Get formatted date and time
   /// </summary>
  public string FormattedDateTime => $"{ReservationDate:yyyy-MM-dd} {StartTime:hh\\:mm} - {EndTime:hh\\:mm}";

   /// <summary>
     /// Get duration in hours
        /// </summary>
  public double DurationInHours => (EndTime - StartTime).TotalHours;

      public override string ToString()
  {
       return $"{LabName} on {ReservationDate:yyyy-MM-dd} ({StartTime:hh\\:mm}-{EndTime:hh\\:mm})";
        }
    }
}
