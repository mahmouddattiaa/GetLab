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

            object result = dbMan.ExecuteScalar ( "sp_ReturnEquipment", parameters );
            return result != null && Convert.ToInt32 ( result ) == 1;
            }
        // Add this inside your Controller class
        public DataTable GetAllActiveReservations ( )
            {
            return dbMan.ExecuteReader ( "sp_GetAllActiveReservations", null );
            }
        // ... existing code ...
        // Add this to Controller.cs

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