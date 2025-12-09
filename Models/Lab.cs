using System;

namespace GetLab.Models
{
    /// <summary>
    /// Represents a lab in the system
    /// </summary>
    public class Lab
  {
     public int LabID { get; set; }
        public string LabName { get; set; }
        public string Building { get; set; }
        public int RoomNumber { get; set; }
  public int Capacity { get; set; }
  public string Equipment { get; set; }
  public bool IsAvailable { get; set; }
        public string Description { get; set; }

     /// <summary>
   /// Get full lab location
        /// </summary>
 public string FullLocation => $"{Building} - Room {RoomNumber}";

      /// <summary>
        /// Get availability status as string
     /// </summary>
    public string AvailabilityStatus => IsAvailable ? "Available" : "Not Available";

        public override string ToString()
{
     return $"{LabName} ({FullLocation})";
     }
    }
}
