using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GeneratedCode
{
   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class Commands
   {
      /// <summary>Executes the non query.</summary>
      /// <param name="sqlCommand">The SQL command.</param>
      public static void ExecuteNonQuery(SqlCommand sqlCommand)
      {
         using (SqlConnection sqlConnection = new SqlConnection(Settings.ConnectionString))
         {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.ExecuteNonQuery();
         }
      }

      /// <summary>Retrieves the data row.</summary>
      /// <param name="sqlCommand">The SQL command.</param>
      /// <returns>The data row</returns>
      public static DataRow RetrieveDataRow(SqlCommand sqlCommand)
      {
         DataRow dataRow = null;
         using (SqlConnection sqlConnection = new SqlConnection(Settings.ConnectionString))
         {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;

            using (DataTable dataTable = new DataTable())
            {
               new SqlDataAdapter(sqlCommand).Fill(dataTable);
               if (dataTable.Rows.Count >= 1)
               {
                  dataRow = dataTable.Rows[0];
               }
            }
         }

         return dataRow;
      }

      /// <summary>Retrieves a data table.</summary>
      /// <param name="sqlCommand">The SQL command.</param>
      /// <returns>The datatable</returns>
      public static DataTable RetrieveDataTable(SqlCommand sqlCommand)
      {
         DataTable dataTable = new DataTable();
         using (SqlConnection sqlConnection = new SqlConnection(Settings.ConnectionString))
         {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            new SqlDataAdapter(sqlCommand).Fill(dataTable);
         }

         return dataTable;
      }

      /// <summary>Retrieves the scalar value.</summary>
      /// <param name="sqlCommand">The SQL command.</param>
      /// <returns>The scalar value as an object</returns>
      public static object RetrieveScalarValue(SqlCommand sqlCommand)
      {
         using (SqlConnection sqlConnection = new SqlConnection(Settings.ConnectionString))
         {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            return sqlCommand.ExecuteScalar();
         }
      }
   }

   public static class Settings
   {
      public static string ConnectionString
      {
         get
         {
            return ConfigurationManager.ConnectionStrings["main"].ConnectionString;
         }
      }
   }
}
