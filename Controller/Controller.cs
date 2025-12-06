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

        public void TerminateConnection ( )
            {
            dbMan.CloseConnection ( );
            }
        }
    }