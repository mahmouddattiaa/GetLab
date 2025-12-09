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
    ConfigurationManager.ConnectionStrings["GetLabConnection"]?.ConnectionString;

        public DBManager()
        {
  // Validate connection string exists
        if (string.IsNullOrEmpty(connectionString))
   {
       throw new Exception("Connection string 'GetLabConnection' not found in App.config");
     }

 // Test connection on initialization
try
            {
  using (SqlConnection testConnection = new SqlConnection(connectionString))
     {
       testConnection.Open();
     }
            }
 catch (Exception ex)
         {
         throw new Exception("Database connection failed during initialization.", ex);
     }
        }

        // SECURE method for executing queries that READ data
        public DataTable ExecuteReader(string storedProcedureName, SqlParameter[] parameters)
{
            DataTable dt = new DataTable();

          using (SqlConnection connection = new SqlConnection(connectionString))
            {
   using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
           {
   command.CommandType = CommandType.StoredProcedure;
  
     if (parameters != null)
             {
     command.Parameters.AddRange(parameters);
    }

 try
                {
       connection.Open();
using (SqlDataReader reader = command.ExecuteReader())
         {
  if (reader.HasRows)
       {
   dt.Load(reader);
          }
 }
             }
           catch (Exception ex)
        {
        throw new Exception($"Error executing stored procedure '{storedProcedureName}'", ex);
       }
             }
       }

            return dt;
      }

        // SECURE method for executing queries that CHANGE data
    public int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters)
        {
      using (SqlConnection connection = new SqlConnection(connectionString))
          {
        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
      {
             command.CommandType = CommandType.StoredProcedure;
  
         if (parameters != null)
  {
       command.Parameters.AddRange(parameters);
     }

             try
         {
 connection.Open();
   return command.ExecuteNonQuery();
             }
           catch (Exception ex)
   {
     throw new Exception($"Error executing stored procedure '{storedProcedureName}'", ex);
           }
         }
            }
        }

        // Method for executing scalar queries (returns single value)
      public object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters)
        {
   using (SqlConnection connection = new SqlConnection(connectionString))
         {
   using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
          {
            command.CommandType = CommandType.StoredProcedure;
    
   if (parameters != null)
             {
        command.Parameters.AddRange(parameters);
    }

        try
            {
          connection.Open();
      return command.ExecuteScalar();
       }
      catch (Exception ex)
         {
            throw new Exception($"Error executing stored procedure '{storedProcedureName}'", ex);
                    }
     }
     }
        }

        // This method is now obsolete but kept for backward compatibility
        public void CloseConnection()
      {
            // No longer needed since connections are managed per operation
      // Method kept for backward compatibility
        }
    }
}