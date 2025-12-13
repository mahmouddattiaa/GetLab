using System.Data;
using System.Data.SqlClient;
using GetLab.Data; // Use our Data folder

namespace GetLab.Controller
    {
    public class Controller
        {
        private DBManager dbMan;

        public Controller ( )
            {
            dbMan = new DBManager ( );
            }

        // This is the LOGIN business logic
        public DataTable CheckLogin ( string universityID, string plainPassword )
            {
            // 1. Hash the password
            string passwordHash = SecurityHelper.ComputeSha256Hash ( plainPassword );

            // 2. Prepare the parameters for the stored procedure
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            // 3. Call the DBManager to execute the stored procedure
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