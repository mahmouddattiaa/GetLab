using GetLab.Data; // Use our Data folder
using System;
using System.Data;
using System.Data.SqlClient;

namespace GetLab.Controller
    {
    public class Controller
        {
        private DBManager dbMan;

        public Controller ( )
            {
            dbMan = new DBManager ( );
            }


        public bool CheckUserExists ( string universityID )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID)
            };
            DataTable result = dbMan.ExecuteReader ( "sp_CheckUserExists", parameters );

            if ( Convert.ToInt32 ( result.Rows[0]["UserExists"] ) == 1 )
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public DataTable ValidatePassword ( string universityID, string plainPassword )
            {
            string passwordHash = Data.SecurityHelper.ComputeSha256Hash ( plainPassword );

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            return dbMan.ExecuteReader ( "sp_UserLogin", parameters );
            }

        public bool ReturnEquipment ( int equipmentID )
            {
            SqlParameter[] parameters = new SqlParameter[]
 {
            new SqlParameter("@EquipmentID", equipmentID)
 };

        object result = dbMan.ExecuteScalar ( "sp_ReturnEquipment", parameters );
            return result != null && Convert.ToInt32 ( result ) == 1;
          }

        // Overload for ReturnEquipment with condition parameter
        public bool ReturnEquipment ( int equipmentID, string condition )
            {
   SqlParameter[] parameters = new SqlParameter[]
    {
         new SqlParameter("@EquipmentID", equipmentID),
      new SqlParameter("@Condition", condition)
          };

        object result = dbMan.ExecuteScalar ( "sp_ReturnEquipmentWithCondition", parameters );
            return result != null && Convert.ToInt32 ( result ) == 1;
            }

        public DataTable GetMyReservations ( string universityID )
        {
     SqlParameter[] parameters = new SqlParameter[]
    {
    new SqlParameter("@UniversityID", universityID)
            };

  return dbMan.ExecuteReader ( "sp_GetMyReservations", parameters );
  }

        // Add these 3 methods to Controller.cs

        public bool AddEquipment ( string name, string model, string serial, int supplierID, int locationID )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Name", name),
        new SqlParameter("@Model", model),
        new SqlParameter("@Serial", serial),
        new SqlParameter("@SupplierID", supplierID),
        new SqlParameter("@LocationID", locationID)
            };
            object result = dbMan.ExecuteScalar ( "sp_AddEquipment", parameters );
            return result != null && Convert.ToInt32 ( result ) == 1;
            }

        public DataTable GetAllSuppliers ( ) { return dbMan.ExecuteReader ( "sp_GetAllSuppliers", null ); }
        public DataTable GetAllLocations ( ) { return dbMan.ExecuteReader ( "sp_GetAllLocations", null ); }
        public DataTable GetAvailableEquipment ( )
            {
        return dbMan.ExecuteReader ( "sp_GetAvailableEquipment", null );
        }

      public DataTable SearchEquipment ( string searchTerm )
            {
            SqlParameter[] parameters = new SqlParameter[]
    {
                new SqlParameter("@SearchTerm", searchTerm)
         };

  return dbMan.ExecuteReader ( "sp_SearchEquipment", parameters );
      }

        public bool ReserveEquipment ( string universityID, int equipmentID, DateTime reservationDate )
        {
     SqlParameter[] parameters = new SqlParameter[]
{
          new SqlParameter("@UniversityID", universityID),
    new SqlParameter("@EquipmentID", equipmentID),
                new SqlParameter("@ReservationDate", reservationDate)
       };

         object result = dbMan.ExecuteScalar ( "sp_ReserveEquipment", parameters );
 return result != null && Convert.ToInt32 ( result ) == 1;
            }

        // Add this inside your Controller class
        public DataTable GetAllActiveReservations ( )
  {
            return dbMan.ExecuteReader ( "sp_GetAllActiveReservations", null );
          }
        // FEATURE 6: Maintenance Management
        public DataTable GetDamagedItems ( )
            {
            return dbMan.ExecuteReader ( "sp_GetDamagedEquipment", null );
            }

        public bool FixEquipment ( int equipmentID )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@EquipmentID", equipmentID)
            };

            // ExecuteNonQuery returns the number of rows affected. 
            // If > 0, it means the update happened.
            int rowsAffected = dbMan.ExecuteNonQuery ( "sp_FixEquipment", parameters );
            return rowsAffected > 0;
            }
        // Add this to Controller.cs

        // FEATURE: Submit a Report
        public bool SubmitReport ( string universityID, int equipmentID, string description )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UniversityID", universityID),
        new SqlParameter("@EquipmentID", equipmentID),
        new SqlParameter("@Description", description)
            };

            object result = dbMan.ExecuteScalar ( "sp_CreateMaintenanceReport", parameters );
            return result != null && Convert.ToInt32 ( result ) == 1;
            }
        public void TerminateConnection ( )
            {
            dbMan.CloseConnection ( );
            }
        }
    }