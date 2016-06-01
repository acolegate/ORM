using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using ORM.Library.HelperFunctions;
using ORM.Library.Interfaces;
using ORM.Library.Models;

namespace ORM.Library
{
   public class DatabaseMessageEventArgs : EventArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DatabaseMessageEventArgs" /> class.
      /// </summary>
      /// <param name="message">The message.</param>
      /// <param name="ex">The ex.</param>
      public DatabaseMessageEventArgs(string message, Exception ex)
      {
         Message = message;
         Exception = ex;
      }

      /// <summary>Gets the exception.</summary>
      /// <value>The exception.</value>
      public Exception Exception { get; private set; }

      /// <summary>Gets the message.</summary>
      /// <value>The message.</value>
      public string Message { get; private set; }
   }

   public class ProgressMessageEventArgs : EventArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ProgressMessageEventArgs" /> class.
      /// </summary>
      /// <param name="message">The message.</param>
      /// <param name="advanceProgressBar">if set to <c>true</c> [is progress event].</param>
      public ProgressMessageEventArgs(string message, bool advanceProgressBar = true)
      {
         Message = message;
         AdvanceProgressBar = advanceProgressBar;
      }

      public bool AdvanceProgressBar { get; private set; }

      /// <summary>Gets the message.</summary>
      /// <value>The message.</value>
      public string Message { get; private set; }
   }

   public partial class Modeller
   {
      private const string SqlRetrieveEnumValuesFromTable = "select [{0}],[{1}] from [{2}].[{3}] order by [{4}]";
      private const string SqlRetrieveStoredProcedureColumns = "SET FMTONLY ON; EXEC [{0}].[{1}] {2}; SET FMTONLY OFF;";
      private const string SqlRetrieveStoredProcedureNames = "SELECT s.NAME AS [schema], so.NAME FROM sys.objects AS so JOIN sys.schemas AS s ON (so.schema_id = s.schema_id) WHERE so.type = 'P' AND so.NAME NOT LIKE ('sp_%diagram%') ORDER BY s.NAME, so.NAME;";
      private const string SqlRetrieveStoredProcedureParams = "SELECT par.name, t.name as datatype FROM sys.procedures as p join sys.parameters as par on (par.object_id = p.object_id) join sys.types as t on (t.system_type_id = par.system_type_id) where p.name = @name order by par.parameter_id;";
      private const string SqlRetrieveTableColumns = "SELECT sc.NAME, st.NAME AS datatype, sc.is_nullable, sc.is_identity, CASE WHEN pkc.column_id IS NULL THEN 0 ELSE 1 END AS is_primary_key, fk.PKSchemaName, fk.PKTable, fk.PKColumn FROM sys.objects AS so JOIN sys.columns AS sc ON (sc.object_id = so.object_id) JOIN sys.types AS st ON (st.system_type_id = sc.system_type_id) LEFT OUTER JOIN ( SELECT ic.column_id FROM sys.objects AS o JOIN sys.indexes AS i ON (i.object_id = o.object_id) JOIN sys.index_columns AS ic ON (ic.object_id = o.object_id) WHERE o.NAME = @name AND i.is_primary_key = 1 ) pkc ON (pkc.column_id = sc.column_id) LEFT OUTER JOIN ( SELECT OBJECT_NAME(fk.parent_object_id) AS FKTable, COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS FKColumn, SCHEMA_NAME(o.SCHEMA_ID) PKSchemaName, OBJECT_NAME(fk.referenced_object_id) AS PKTable, COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS PKColumn FROM sys.foreign_keys AS fk JOIN sys.foreign_key_columns AS fkc ON fk.OBJECT_ID = fkc.constraint_object_id JOIN sys.objects AS o ON o.OBJECT_ID = fkc.referenced_object_id ) fk ON ( fk.FKTable = @name AND fk.FKColumn = sc.NAME ) WHERE so.NAME = @name AND st.NAME <> 'sysname' ORDER BY sc.column_id;";
      private const string SqlRetrieveTableNames = "SELECT s.name as [schema], so.name FROM sys.objects as so join sys.schemas as s on (s.schema_id = so.schema_id) WHERE so.[type] = 'U' AND so.[NAME] <> 'sysdiagrams' ORDER BY s.name, so.name;";
      private const string SqlRetrieveViewColumns = "SELECT sc.NAME, st.NAME AS datatype FROM sysobjects AS so JOIN syscolumns AS sc ON (sc.id = so.id) JOIN systypes AS st ON (st.xtype = sc.xtype) WHERE so.xtype = 'V' AND so.NAME = @name AND so.xtype = 'V' AND st.name != 'sysname' ORDER BY colorder;";
      private const string SqlRetrieveViewNames = "SELECT s.name as [schema], so.name FROM sys.objects as so join sys.schemas as s on (s.schema_id = so.schema_id) WHERE so.[type] = 'V' ORDER BY s.name, so.name;";
      private const string UnableToReadColumnsForTableMessage = "Unable to read columns for table {0}.{1}";
      private const string UnableToReadColumnsForViewMessage = "Unable to read columns for view {0}.{1}";
      private const string UnableToReadInputParametersForStoredProcedureMessage = "Unable to read input parameters for stored procedure {0}.{1}";
      private const string UnableToReadOutputColumnsForStoredProcedureMessage = "Unable to read output columns for stored procedure {0}.{1}";
      private const string UnableToReadStoredProcedureNamesFromDatabaseMessage = "Unable to read stored procedure names from database";
      private const string UnableToReadTableNamesFromDatabaseMessage = "Unable to read table names from database";
      private const string UnableToRetrieveViewNamesFromDatabaseMessage = "Unable to read view names from database";

      private bool _entitiesRead;
      private IEnumerable<SqlStoredProcedure> _storedProcedures;
      private IEnumerable<SqlTable> _tables;
      private IEnumerable<SqlView> _views;

      /// <summary>
      /// Initializes a new instance of the <see cref="Modeller" /> class.
      /// </summary>
      /// <param name="projectSettings">The project settings.</param>
      public Modeller(ProjectSettings projectSettings)
      {
         Settings.Instance.ProjectSettings = projectSettings;
         _entitiesRead = false;
      }

      public delegate void DatabaseMessageEventHandler(object sender, DatabaseMessageEventArgs e);

      public delegate void ProgressMessageEventHandler(object sender, ProgressMessageEventArgs e);

      /// <summary>Occurs when [on database message].</summary>
      public event DatabaseMessageEventHandler OnDatabaseMessage;

      /// <summary>Occurs when [on database message].</summary>
      public event ProgressMessageEventHandler OnProgressMessage;

      /// <summary>Gets the commands file contents.</summary>
      /// <value>The commands file contents.</value>
      public string CommandsFileContents { get; private set; }

      /// <summary>Gets the entity count.</summary>
      /// <value>The entity count.</value>
      public int EntityCount
      {
         get
         {
            return (_tables.Count() * 2) + (_storedProcedures.Count() * 2) + _views.Count();
         }
      }

      /// <summary>Gets the model file contents.</summary>
      /// <value>The model file contents.</value>
      public string ModelsFileContents { get; private set; }

      /// <summary>Gets the services file contents.</summary>
      /// <value>The service file contents.</value>
      public string ServicesFileContents { get; private set; }

      public static List<string> RetrieveStoredProcedureNames()
      {
         return (from DataRow dataRow in Commands.RetrieveDataTable(new SqlCommand(SqlRetrieveStoredProcedureNames)).Rows select dataRow["name"]).Cast<string>().ToList();
      }

      /// <summary>Retrieves the table names.</summary>
      /// <returns>The table names</returns>
      public static List<string> RetrieveTableNames()
      {
         return (from DataRow dataRow in Commands.RetrieveDataTable(new SqlCommand(SqlRetrieveTableNames)).Rows select dataRow["name"]).Cast<string>().ToList();
      }

      public static List<string> RetrieveViewNames()
      {
         return (from DataRow dataRow in Commands.RetrieveDataTable(new SqlCommand(SqlRetrieveViewNames)).Rows select dataRow["name"]).Cast<string>().ToList();
      }

      /// <summary>Processes this instance.</summary>
      public void Process()
      {
         if (_entitiesRead)
         {
            ModelsFileContents = MakeModelsFile();
            ServicesFileContents = MakeServicesFile();
            CommandsFileContents = MakeCommandsFile();
         }
         else
         {
            RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs("Entities have not been read", null));
         }
      }

      /// <summary>Reads the entities.</summary>
      public void ReadEntities()
      {
         _tables = ReadTables();
         _storedProcedures = ReadStoredProcedures();
         _views = ReadViews();

         _entitiesRead = true;
      }

      /// <summary>Gets the enum values from a table.</summary>
      /// <param name="valueColumn">The value column.</param>
      /// <param name="nameColumn">The name column.</param>
      /// <param name="sqlTable">The SQL table.</param>
      /// <returns>A datatable</returns>
      private static DataTable GetEnumValuesFromTable(ISqlName valueColumn, ISqlName nameColumn, ISqlEntity sqlTable)
      {
         return Commands.RetrieveDataTable(new SqlCommand(string.Format(SqlRetrieveEnumValuesFromTable, valueColumn.SqlName, nameColumn.SqlName, sqlTable.Schema, sqlTable.SqlName, nameColumn.SqlName)) {
                                                                                                                                                                                                            CommandType = CommandType.Text
                                                                                                                                                                                                         });
      }

      /// <summary>Raises the database message event.</summary>
      /// <param name="data">The <see cref="DatabaseMessageEventArgs" /> instance containing the event data.</param>
      private void RaiseDatabaseMessageEvent(DatabaseMessageEventArgs data)
      {
         if (OnDatabaseMessage != null)
         {
            OnDatabaseMessage(this, data);
         }
      }

      /// <summary>Raises the progress message event.</summary>
      /// <param name="data">The <see cref="ProgressMessageEventArgs" /> instance containing the event data.</param>
      private void RaiseProgressMessageEvent(ProgressMessageEventArgs data)
      {
         if (OnProgressMessage != null)
         {
            OnProgressMessage(this, data);
         }
      }

      /// <summary>Reads the stored procedures.</summary>
      /// <returns>An enumarable set of sql stored procedures</returns>
      private IEnumerable<SqlStoredProcedure> ReadStoredProcedures()
      {
         List<SqlStoredProcedure> storedProcedures = new List<SqlStoredProcedure>();

         string schemaName;
         string storedProcedureName;

         try
         {
            SqlStoredProcedure sqlStoredProcedure;

            foreach (DataRow storedProcedureDataRow in Commands.RetrieveDataTable(new SqlCommand(SqlRetrieveStoredProcedureNames)).Rows)
            {
               schemaName = Convert.ToString(storedProcedureDataRow["schema"]);
               storedProcedureName = Convert.ToString(storedProcedureDataRow["name"]);

               // INPUT PARAMETERS
               sqlStoredProcedure = new SqlStoredProcedure(schemaName, storedProcedureName);

               try
               {
                  using (SqlCommand sqlCommand = new SqlCommand(SqlRetrieveStoredProcedureParams))
                  {
                     sqlCommand.Parameters.AddWithValue("@name", storedProcedureName);

                     foreach (DataRow paramDataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
                     {
                        sqlStoredProcedure.Parameters.Add(new SqlStoredProcedureParameter(Convert.ToString(paramDataRow["name"]), Data.ParseSqlDataType(Convert.ToString(paramDataRow["datatype"]))));
                     }

                     // OUTPUT COLUMNS
                     try
                     {
                        SqlCommand outputColumnsSqlCommand = new SqlCommand(string.Format(SqlRetrieveStoredProcedureColumns, schemaName, storedProcedureName, sqlStoredProcedure.ParametersAsNullsCsv));

                        foreach (DataColumn outputColumn in Commands.RetrieveDataTable(outputColumnsSqlCommand).Columns)
                        {
                           sqlStoredProcedure.Columns.Add(new SqlStoredProcedureColumn(Convert.ToString(outputColumn.ColumnName), Data.GetSqlDbType(outputColumn.DataType)));
                        }

                        storedProcedures.Add(sqlStoredProcedure);
                     }
                     catch (Exception ex)
                     {
                        RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(string.Format(UnableToReadOutputColumnsForStoredProcedureMessage, schemaName, storedProcedureName), ex));
                     }
                  }
               }
               catch (Exception ex)
               {
                  RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(string.Format(UnableToReadInputParametersForStoredProcedureMessage, schemaName, storedProcedureName), ex));
               }
            }
         }
         catch (Exception ex)
         {
            RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(UnableToReadStoredProcedureNamesFromDatabaseMessage, ex));
         }

         return storedProcedures;
      }

      /// <summary>Reads the tables.</summary>
      /// <returns>An enumerable set of sqltables</returns>
      private IEnumerable<SqlTable> ReadTables()
      {
         List<SqlTable> tables = new List<SqlTable>();
         SqlTable sqlTable;
         string tableSchema;
         string tableName;

         string columnName;
         SqlDbType sqlDbType;
         bool isPrimaryKey;
         bool isIdentity;
         bool isNullable;

         SqlCommand sqlCommand;
         string uniqueClrName;

         string primaryKeySchema;
         string primaryKeyTable;
         string primaryKeyColumn;

         try
         {
            foreach (DataRow tableNameRow in Commands.RetrieveDataTable(new SqlCommand(SqlRetrieveTableNames)).Rows)
            {
               tableSchema = Convert.ToString(tableNameRow["schema"]);
               tableName = Convert.ToString(tableNameRow["name"]);

               sqlCommand = new SqlCommand(SqlRetrieveTableColumns);
               sqlCommand.Parameters.AddWithValue("@name", tableName);

               sqlTable = new SqlTable(tableSchema, tableName);

               try
               {
                  foreach (DataRow columnRow in Commands.RetrieveDataTable(sqlCommand).Rows)
                  {
                     columnName = Convert.ToString(columnRow["name"]);
                     sqlDbType = Data.ParseSqlDataType(Convert.ToString(columnRow["datatype"]));
                     isIdentity = Convert.ToBoolean(columnRow["is_identity"]);
                     isPrimaryKey = Convert.ToBoolean(columnRow["is_primary_key"]);
                     isNullable = Convert.ToBoolean(columnRow["is_nullable"]);

                     primaryKeySchema = Convert.ToString(columnRow["pkschemaname"]);
                     primaryKeyTable = Convert.ToString(columnRow["pktable"]);
                     primaryKeyColumn = Convert.ToString(columnRow["pkcolumn"]);

                     SqlTableColumn newSqlTableColumn = new SqlTableColumn(columnName, sqlDbType, isIdentity, isPrimaryKey, isNullable);

                     if (string.IsNullOrEmpty(primaryKeySchema) == false && string.IsNullOrEmpty(primaryKeyTable) == false && string.IsNullOrEmpty(primaryKeyColumn) == false)
                     {
                        newSqlTableColumn.ForeignKey = new ForeignKey {
                                                                         IsEnum = Settings.Instance.ProjectSettings.EnumTables.Contains(primaryKeyTable), 
                                                                         PrimaryKeySchemaName = primaryKeySchema, 
                                                                         PrimaryKeySqlTableName = primaryKeyTable, 
                                                                         PrimaryKeySqlColumnName = primaryKeyColumn
                                                                      };
                     }

                     if (sqlTable.Columns.Find(x => x.ClrName == newSqlTableColumn.ClrName) != null)
                     {
                        // a column with the same clrname already exists
                        uniqueClrName = Text.MakeUniqueName(sqlTable.Columns.Select(x => x.ClrName).ToList(), newSqlTableColumn.ClrName);
                        newSqlTableColumn.ClrName = uniqueClrName;
                        newSqlTableColumn.CamelCaseClrName = Text.LowercaseFirstLetter(uniqueClrName);
                     }

                     sqlTable.Columns.Add(newSqlTableColumn);
                  }

                  if (tables.Find(x => x.ClrName == sqlTable.ClrName) != null)
                  {
                     // a table with this ClrName already exists
                     uniqueClrName = Text.MakeUniqueName(tables.Select(x => x.ClrName).ToList(), sqlTable.ClrName);
                     sqlTable.ClrName = uniqueClrName;
                     sqlTable.CamelCaseClrName = Text.LowercaseFirstLetter(uniqueClrName);
                  }

                  tables.Add(sqlTable);
               }
               catch (Exception ex)
               {
                  RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(string.Format(UnableToReadColumnsForTableMessage, tableSchema, tableName), ex));
               }

               sqlCommand.Dispose();
            }
         }
         catch (Exception ex)
         {
            RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(UnableToReadTableNamesFromDatabaseMessage, ex));
         }

         return tables;
      }

      /// <summary>Reads the views.</summary>
      /// <returns>An enumerable set of Sql Views</returns>
      private IEnumerable<SqlView> ReadViews()
      {
         List<SqlView> views = new List<SqlView>();
         SqlView sqlView;
         string viewSchema;
         string viewName;

         string columnName;
         SqlDbType sqlDbType;

         SqlCommand sqlCommand;

         try
         {
            foreach (DataRow viewNameRow in Commands.RetrieveDataTable(new SqlCommand(SqlRetrieveViewNames)).Rows)
            {
               viewSchema = Convert.ToString(viewNameRow["schema"]);
               viewName = Convert.ToString(viewNameRow["name"]);

               sqlCommand = new SqlCommand(SqlRetrieveViewColumns);

               sqlCommand.Parameters.AddWithValue("@name", viewName);

               sqlView = new SqlView(viewSchema, viewName);

               try
               {
                  foreach (DataRow columnRow in Commands.RetrieveDataTable(sqlCommand).Rows)
                  {
                     columnName = Convert.ToString(columnRow["name"]);
                     sqlDbType = Data.ParseSqlDataType(Convert.ToString(columnRow["datatype"]));

                     sqlView.Columns.Add(new SqlViewColumn(columnName, sqlDbType));
                  }

                  views.Add(sqlView);

                  sqlCommand.Dispose();
               }
               catch (Exception ex)
               {
                  RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(string.Format(UnableToReadColumnsForViewMessage, viewSchema, viewName), ex));
               }
            }
         }
         catch (Exception ex)
         {
            RaiseDatabaseMessageEvent(new DatabaseMessageEventArgs(UnableToRetrieveViewNamesFromDatabaseMessage, ex));
         }

         return views;
      }
   }
}
