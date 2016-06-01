using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace ORM.Library
{
	[ExcludeFromCodeCoverage]
	public static class Commands
	{
		///// <summary>Executes the non query.</summary>
		///// <param name="sqlCommand">The SQL command.</param>
		//public static void ExecuteNonQuery(SqlCommand sqlCommand)
		//{
		//	using (SqlConnection sqlConnection = new SqlConnection(Settings.Instance.ConnectionString))
		//	{
		//		sqlConnection.Open();

		//		sqlCommand.Connection = sqlConnection;

		//		sqlCommand.ExecuteNonQuery();

		//		// close connection is implicit
		//	}
		//}

		///// <summary>Retrieves the data row.</summary>
		///// <param name="sqlCommand">The SQL command.</param>
		///// <returns>The data row</returns>
		//public static DataRow RetrieveDataRow(SqlCommand sqlCommand)
		//{
		//	DataRow dataRow = null;

		//	using (SqlConnection sqlConnection = new SqlConnection(Settings.Instance.ConnectionString))
		//	{
		//		sqlConnection.Open();

		//		sqlCommand.Connection = sqlConnection;

		//		using (DataTable dataTable = new DataTable())
		//		{
		//			new SqlDataAdapter(sqlCommand).Fill(dataTable);

		//			if (dataTable.Rows.Count >= 1)
		//			{
		//				dataRow = dataTable.Rows[0];
		//			}
		//		}

		//		// close connection is implicit
		//	}

		//	return dataRow;
		//}

		///// <summary>Retrieves the data set.</summary>
		///// <param name="sqlCommand">The SQL command.</param>
		///// <returns>A Data Set</returns>
		//public static DataSet RetrieveDataSet(SqlCommand sqlCommand)
		//{
		//	DataSet dataSet = new DataSet();

		//	using (SqlConnection sqlConnection = new SqlConnection(Settings.Instance.ConnectionString))
		//	{
		//		sqlConnection.Open();

		//		sqlCommand.Connection = sqlConnection;

		//		new SqlDataAdapter(sqlCommand).Fill(dataSet);

		//		// close connection is implicit
		//	}

		//	return dataSet;
		//}

		/// <summary>Retrieves a data table.</summary>
		/// <param name="sqlCommand">The SQL command.</param>
		/// <returns>The datatable</returns>
		public static DataTable RetrieveDataTable(SqlCommand sqlCommand)
		{
			DataTable dataTable = new DataTable();

			using (SqlConnection sqlConnection = new SqlConnection(Settings.Instance.ProjectSettings.ConnectionString))
			{
				sqlConnection.Open();

				sqlCommand.Connection = sqlConnection;

				new SqlDataAdapter(sqlCommand).Fill(dataTable);

				// close connection is implicit
			}

			return dataTable;
		}

		///// <summary>Retrieves the scalar value.</summary>
		///// <param name="sqlCommand">The SQL command.</param>
		///// <returns>The scalar value as an object</returns>
		//public static object RetrieveScalarValue(SqlCommand sqlCommand)
		//{
		//	using (SqlConnection sqlConnection = new SqlConnection(Settings.Instance.ConnectionString))
		//	{
		//		sqlConnection.Open();

		//		sqlCommand.Connection = sqlConnection;

		//		return sqlCommand.ExecuteScalar();

		//		// close connection is implicit
		//	}
		//}
	}
}