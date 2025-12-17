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
      if (string.IsNullOrEmpty(connectionString))
      {
        throw new Exception("Connection string 'GetLabConnection' not found in App.config");
      }

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

    public void CloseConnection()
    {
    }
  }
}
