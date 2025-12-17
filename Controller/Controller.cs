using GetLab.Data; // Use our Data folder
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace GetLab.Controller
{
    public class Controller
    {
        private DBManager dbMan;

        public Controller()
        {
            dbMan = new DBManager();
        }


        public bool CheckUserExists(string universityID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID)
            };
            DataTable result = dbMan.ExecuteReader("sp_CheckUserExists", parameters);

            if (Convert.ToInt32(result.Rows[0]["UserExists"]) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable ValidatePassword(string universityID, string plainPassword)
        {
            string passwordHash = Data.SecurityHelper.ComputeSha256Hash(plainPassword);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            return dbMan.ExecuteReader("sp_UserLogin", parameters);
        }

        public bool ReturnEquipment(int equipmentID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", equipmentID)
            };

            object result = dbMan.ExecuteScalar("sp_ReturnEquipment", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }

        // Overload for ReturnEquipment with condition parameter
        public bool ReturnEquipment(int equipmentID, string condition)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", equipmentID),
                new SqlParameter("@Condition", condition)
            };

            object result = dbMan.ExecuteScalar("sp_ReturnEquipmentWithCondition", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }

        public DataTable GetMyReservations(string universityID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID)
            };

            return dbMan.ExecuteReader("sp_GetMyReservations", parameters);
        }

        public bool AddEquipment(string name, string model, string serial, int supplierID, int locationID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Model", model),
                new SqlParameter("@Serial", serial),
                new SqlParameter("@SupplierID", supplierID),
                new SqlParameter("@LocationID", locationID)
            };
            object result = dbMan.ExecuteScalar("sp_AddEquipment", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }

        public DataTable GetAllSuppliers() { return dbMan.ExecuteReader("sp_GetAllSuppliers", null); }
        public DataTable GetAllLocations() { return dbMan.ExecuteReader("sp_GetAllLocations", null); }
        public DataTable GetAvailableEquipment()
        {
            return dbMan.ExecuteReader("sp_GetAvailableEquipment", null);
        }


        public DataTable getRoomName()
        {
            return dbMan.ExecuteReader("sp_GetRoomNameByType", null);
        }

        public DataTable getRoomDetails(string labStatus)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LabStatus", labStatus)
            };

            return dbMan.ExecuteReader("sp_GetRoomNameByStatusLocation", parameters);
        }

        public DataTable GetAvailableEquipmentByLab(string locationID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LocationID", locationID)
            };

            return dbMan.ExecuteReader("sp_GetAvailableEquipmentByLab", parameters);
        }

        public int SubmitEquipmentRequest(string teacherID, string equipmentName, string justification)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TeacherUniversityID", teacherID),
                new SqlParameter("@EquipmentName", equipmentName),
                new SqlParameter("@Justification", justification)
            };

            return dbMan.ExecuteNonQuery("sp_SubmitEquipmentRequest", parameters);
        }


        public DataTable GetTeacherEquipmentRequests(string teacherUniversityID)
        {
            SqlParameter[] parameters = { new SqlParameter("@TeacherUniversityID", teacherUniversityID) };
            return dbMan.ExecuteReader("sp_GetTeacherEquipmentRequests", parameters);
        }


        public DataTable SearchEquipment(string keyword)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword)
            };

            return dbMan.ExecuteReader("sp_SearchEquipment", parameters);
        }

        public bool ReserveEquipment(string universityID, int equipmentID, DateTime dueDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@EquipmentID", equipmentID),
                new SqlParameter("@DueDate", dueDate)
            };

            object result = dbMan.ExecuteScalar("sp_ReserveEquipment", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }

        public DataTable GetAllActiveReservations()
        {
            return dbMan.ExecuteReader("sp_GetAllActiveReservations", null);
        }
        public DataTable GetDamagedItems()
        {
            return dbMan.ExecuteReader("sp_GetDamagedEquipment", null);
        }

        public bool FixEquipment(int equipmentID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", equipmentID)
            };

            int rowsAffected = dbMan.ExecuteNonQuery("sp_FixEquipment", parameters);
            return rowsAffected > 0;
        }
        public bool SubmitReport(string universityID, int equipmentID, string description)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@EquipmentID", equipmentID),
                new SqlParameter("@Description", description)
            };

            object result = dbMan.ExecuteScalar("sp_CreateMaintenanceReport", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }
        public bool AddLocation(string name, string type, int capacity)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RoomName", name),
                new SqlParameter("@RoomType", type),
                new SqlParameter("@Capacity", capacity)
            };

            object result = dbMan.ExecuteScalar("sp_AddLocation", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }

        public DataTable GetLocationsList()
        {
            return dbMan.ExecuteReader("sp_GetLocationsList", null);
        }
        public DataTable GetAllEquipmentList()
        {
            return dbMan.ExecuteReader("sp_GetAllEquipmentList", null);
        }
        public DataTable GetAvailableLabEquipment()
        {
            return dbMan.ExecuteReader("sp_GetAvailableLabEquipment", null);
        }

        public DataTable GetEquipmentBusyTimes(int equipID, DateTime date)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EquipmentID", equipID),
                new SqlParameter("@SelectedDate", date.Date)
            };
            return dbMan.ExecuteReader("sp_GetEquipmentBusyTimes", parameters);
        }

        public DataTable GetRoomBusyTimes(int locationID, DateTime date)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LocationID", locationID),
                new SqlParameter("@SelectedDate", date.Date)
            };
            return dbMan.ExecuteReader("sp_GetRoomBusyTimes", parameters);
        }

        public bool ReserveSlot(string universityID, int equipmentID, DateTime startTime, DateTime endTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@EquipmentID", equipmentID),
                new SqlParameter("@StartTime", startTime),
                new SqlParameter("@EndTime", endTime)
            };
            object result = dbMan.ExecuteScalar("sp_ReserveSlot", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }
        public DataTable GetPendingRequests()
        {
            return dbMan.ExecuteReader("sp_GetPendingRequests", null);
        }

        public bool UpdateRequestStatus(int requestID, string status, string adminUniversityID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID", requestID),
                new SqlParameter("@Status", status),
                new SqlParameter("@AdminID", DBNull.Value)
            };

            object result = dbMan.ExecuteNonQuery("sp_UpdateRequestStatus", parameters);
            return (int)result > 0;
        }
        public bool ReserveRoom(string universityID, int locationID, DateTime startTime, DateTime endTime, string purpose)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", universityID),
                new SqlParameter("@LocationID", locationID),
                new SqlParameter("@StartTime", startTime),
                new SqlParameter("@EndTime", endTime),
                new SqlParameter("@Purpose", purpose)
            };
            object result = dbMan.ExecuteScalar("sp_ReserveRoom", parameters);
            return result != null && Convert.ToInt32(result) == 1;
        }
        public DataTable GetAvailableStorageEquipment()
        {
            return dbMan.ExecuteReader("sp_GetAvailableStorageEquipment", null);
        }
        public int RegisterUser(string fullName, string email, string uniID, string password)
        {
            string passwordHash = Data.SecurityHelper.ComputeSha256Hash(password);

            string role = "Student";
            if (uniID.ToUpper().StartsWith("PROF")) role = "Teacher";
            else if (uniID.ToUpper().StartsWith("ADM")) role = "Admin";
            else if (uniID.ToUpper().StartsWith("ASST")) role = "Admin";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityID", uniID),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@UserRole", role)
            };

            object result = dbMan.ExecuteScalar("sp_RegisterUser", parameters);

            if (result != null)
                return Convert.ToInt32(result);

            return 0;
        }
        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }
    }
}