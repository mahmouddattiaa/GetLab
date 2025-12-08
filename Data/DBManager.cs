using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // Required for App.config

namespace GetLab.Data
    {
    public class DBManager
        {
        // Read the connection string from App.config
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["GetLabConnection"].ConnectionString;

        private SqlConnection myConnection;

        public DBManager ( )
            {
            myConnection = new SqlConnection ( connectionString );
            try
                {
                myConnection.Open ( );
                }
            catch ( Exception ex )
                {
                // This is critical for debugging connection issues
                throw new Exception ( "Database connection failed.", ex );
                }
            }

        // SECURE method for executing queries that READ data
        public DataTable ExecuteReader ( string storedProcedureName, SqlParameter[] parameters )
            {
            SqlCommand myCommand = new SqlCommand ( storedProcedureName, myConnection );
            myCommand.CommandType = CommandType.StoredProcedure;
            if ( parameters != null )
                {
                myCommand.Parameters.AddRange ( parameters );
                }

            SqlDataReader reader = myCommand.ExecuteReader ( );
            DataTable dt = new DataTable ( );
            if ( reader.HasRows )
                {
                dt.Load ( reader );
                }
            reader.Close ( );
            return dt;
            }

        // SECURE method for executing queries that CHANGE data
        public int ExecuteNonQuery ( string storedProcedureName, SqlParameter[] parameters )
            {
            SqlCommand myCommand = new SqlCommand ( storedProcedureName, myConnection );
            myCommand.CommandType = CommandType.StoredProcedure;
            if ( parameters != null )
                {
                myCommand.Parameters.AddRange ( parameters );
                }
            return myCommand.ExecuteNonQuery ( );
            }
        // Add this inside your DBManager class
        public object ExecuteScalar ( string storedProcedureName, SqlParameter[] parameters )
            {
            SqlCommand myCommand = new SqlCommand ( storedProcedureName, myConnection );
            myCommand.CommandType = CommandType.StoredProcedure;
            if ( parameters != null )
                {
                myCommand.Parameters.AddRange ( parameters );
                }

            // ExecuteScalar returns the first column of the first row (e.g., "1")
            return myCommand.ExecuteScalar ( );
            }
        public void CloseConnection ( )
            {
            if ( myConnection != null && myConnection.State == ConnectionState.Open )
                {
                myConnection.Close ( );
                }
            }
        }
    }