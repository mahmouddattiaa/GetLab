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
        public DataTable GetAvailableEquipment ( )
            {
            return dbMan.ExecuteReader ( "sp_GetAvailableEquipment", null );
            }
        public DataTable getEquipmentUsingStatus(string status)
        {
           SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@Status", status)
           };
                return dbMan.ExecuteReader("sp_GetEquipmentByStatus", parameters);
        }


        public DataTable getRoomName()
        {
            return dbMan.ExecuteReader("sp_GetRoomNameByType", null);
        }

        public DataTable GetAvailableEquipmentByLab(string locationID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@LocationID", locationID)
            };

            return dbMan.ExecuteReader("sp_GetAvailableEquipmentByLab", parameters);
        }


        // FEATURE 2: Search for specific items (by name)
        public DataTable SearchEquipment ( string keyword )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword)
            };
            return dbMan.ExecuteReader ( "sp_SearchEquipment", parameters );
            }

        // FEATURE 3: Reserve an item
        // FEATURE 3: Reserve an item
        public bool ReserveEquipment ( string userID, int equipmentID, DateTime dueDate )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
        // FIX: Change "@UserID" to "@UniversityID" to match the Stored Procedure
        new SqlParameter("@UniversityID", userID),
        new SqlParameter("@EquipmentID", equipmentID),
        new SqlParameter("@DueDate", dueDate)
            };

            // We use ExecuteScalar because our SP returns a single number (1 or 0)
            object result = dbMan.ExecuteScalar ( "sp_ReserveEquipment", parameters );

            // If result is 1, return true (Success). If 0, return false.
            return result != null && Convert.ToInt32 ( result ) == 1;
            }
        // Add this to Controller.cs
        public DataTable GetMyReservations ( string universityID )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UniversityID", universityID)
            };
            return dbMan.ExecuteReader ( "sp_GetMyReservations", parameters );
            }
        // Add this to Controller.cs

        // FEATURE 4: Return Equipment (Admin Only)
        public bool ReturnEquipment ( int equipmentID, string condition )
            {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@EquipmentID", equipmentID),
        new SqlParameter("@Condition", condition)
            };

            object result = dbMan.ExecuteScalar ( "sp_ReturnEquipment", parameters );
            return result != null && Convert.ToInt32 ( result ) == 1;
            }
        // Add this inside your Controller class
        public DataTable GetAllActiveReservations ( )
            {
            return dbMan.ExecuteReader ( "sp_GetAllActiveReservations", null );
            }
        
        // ... existing code ...
        public void TerminateConnection ( )
            {
            dbMan.CloseConnection ( );
            }
        }
    }