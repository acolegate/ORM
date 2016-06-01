using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using ORM.Library.HelperFunctions;
using ORM.Library.Interfaces;
using ORM.Library.Models;

namespace ORM.Library
{
   public partial class Modeller
   {
      private const string ColumnCamelCaseClrNamePlaceholder = "{column.CamelCaseClrName}";
      private const string ColumnClrDataTypeNamePlaceholder = "{column.ClrDataType.Name}";
      private const string ColumnClrNamePlaceholder = "{column.ClrName}";
      private const string ColumnClrNamePlaceholderEnum = "{enumName}";
      private const string ColumnEnumPlaceholder = "{columnenum}";
      private const string ColumnEnumTemplate = "\t\t\t\t{0},\r\n";
      private const string ColumnSqlNamePlaceholder = "{column.SqlName}";
      private const string ContentPlaceholder = "{content}";
      private const string CrLf = "\r\n";
      private const string EnumSuffixPlaceholder = "{Settings.Instance.EnumSuffix}";
      private const string ExtensionMethodsPlaceholder = "{extensionmethods}";
      private const string MakePopulateModelPropertiesFromSqlValuesPlaceholder = "{MakePopulateModelPropertiesFromSqlValues}";
      private const string MakePopulateSqlEntityParamValuesPlaceholder = "{MakePopulateSqlEntityParamValues}";
      private const string MakeSprocClrParametersPlaceholder = "{MakeSprocClrParameters}";
      private const string MakeSqlSetsFromColumnsPlaceholder = "{MakeSqlSetsFromColumns}";
      private const string NoNonIdentityColumnsMessage = "\t\t\t\t// No non-identity columns to populate\r\n";
      private const string PrimaryKeyColumnCamelCaseClrNamePlaceholder = "{primaryKeyColumn.CamelCaseClrName}";
      private const string PrimaryKeyColumnClrDataTypeNamePlaceholder = "{primaryKeyColumn.ClrDataType.Name}";
      private const string PrimaryKeyColumnClrNativeDataTypeNamePlaceholder = "{primaryKeyColumn.ClrNativeDataTypeName}";
      private const string PrimaryKeyColumnSqlNamePlaceholder = "{primaryKeyColumn.SqlName}";
      private const string SettingsInstanceCommandsClassNamePlaceholder = "{Settings.Instance.CommandsClassName}";
      private const string SettingsInstanceConnectionStringNamePlaceholder = "{Settings.Instance.ConnectionStringName}";
      private const string SettingsInstanceModelClassNamePlaceholder = "{Settings.Instance.ModelClassName}";
      private const string SettingsInstanceModelClassSuffixPlaceholder = "{Settings.Instance.ModelClassSuffix}";
      private const string SettingsInstanceNamespacePlaceholder = "{Settings.Instance.Namespace}";
      private const string SettingsInstanceServiceClassSuffixPlaceholder = "{Settings.Instance.ServiceClassSuffix}";
      private const string SettingsInstanceStoredProceduresClassNamePlaceholder = "{Settings.Instance.StoredProceduresClassName}";
      private const string SettingsInstanceViewsClassNamePlaceholder = "{Settings.Instance.ViewsClassName}";
      private const string SortExtensionMethodColumnTemplate = "\t\t\t\t\t\t\t\t\tcase {0}.Column.{1}:\r\n\t\t\t\t\t\t\t\t\t\t{{\r\n\t\t\t\t\t\t\t\t\t\t\treturn left.{1}.CompareTo(right.{1}) * (sortOrder == Commands.SortOrder.Descending ? -1 : 1);\r\n\t\t\t\t\t\t\t\t\t\t}}\r\n";
      private const string SortExtensionMethodNullableColumnTemplate = "\t\t\t\t\t\t\t\t\tcase {0}.Column.{1}:\r\n\t\t\t\t\t\t\t\t\t\t{{\r\n\t\t\t\t\t\t\t\t\t\t\treturn Nullable.Compare<{2}>(left.{1}, right.{1}) * (sortOrder == Commands.SortOrder.Descending ? -1 : 1);\r\n\t\t\t\t\t\t\t\t\t\t}}\r\n";
      private const string SqlEntityCamelCaseClrNamePlaceholder = "{sqlEntity.CamelCaseClrName}";
      private const string SqlEntityClrNamePlaceholder = "{sqlEntity.ClrName}";
      private const string SqlEntitySchemaPlaceholder = "{SqlEntitySchema}";
      private const string SqlTableColumnClrNamePlaceholder = "{sqlTableColumn.ClrName}";
      private const string SqlTableColumnClrNativeDataTypeNamePlaceholder = "{sqlTableColumn.ClrNativeDataTypeName}";
      private const string SqlTableColumnParametersAsCsvPlaceholder = "{sqlTable.ColumnParametersAsCsv}";
      private const string SqlTableColumnsAsCsvPlaceholder = "{sqlTable.ColumnsAsCsv}";
      private const string SqlTableSqlNamePlaceholder = "{sqlTable.SqlName}";
      private const string StoredProcedureCamelCaseClrNamePlaceholder = "{storedProcedure.CamelCaseClrName}";
      private const string StoredProcedureClrNamePlaceholder = "{storedProcedure.ClrName}";
      private const string StoredProcedureSqlNamePlaceholder = "{storedProcedure.SqlName}";

      /// <summary>Makes the commands file.</summary>
      /// <returns>The contents of the commands file</returns>
      private static string MakeCommandsFile()
      {
         return PopulateSettingsPlaceholders(Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.CommandsFile].FileContents);
      }

      /// <summary>Makes the name of the enum foreign key.</summary>
      /// <param name="columnName">Name of the column.</param>
      /// <returns>The Enum name of the column</returns>
      private static string MakeEnumForeignKeyName(string columnName)
      {
         string returnValue = columnName;

         if (columnName.EndsWith(Settings.Instance.ProjectSettings.EnumPrimaryKeyColumnSuffixToRemove))
         {
            returnValue = columnName.Substring(0, columnName.Length - Settings.Instance.ProjectSettings.EnumPrimaryKeyColumnSuffixToRemove.Length);
         }

         return returnValue;
      }

      /// <summary>Makes the populate model properties from SQL values.</summary>
      /// <param name="columns">The columns.</param>
      /// <returns>The populate model properties</returns>
      private static string MakePopulateModelPropertiesFromSqlValues(IEnumerable<ISqlTableColumn> columns)
      {
         string standardTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateModelProperty].FileContents;
         string standardTemplateNullable = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateModelPropertyNullable].FileContents;
         string enumTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateModelPropertyEnum].FileContents;

         string clrNativeDataTypeName;

         StringBuilder stringBuilder = new StringBuilder(string.Empty);
         foreach (ISqlTableColumn column in columns)
         {
            if (column.ForeignKey == null || column.ForeignKey.IsEnum == false)
            {
               if (column.IsNullable)
               {
                  // this is a standard NULLABLE column
                  if (column.ClrDataType == typeof(string))
                  {
                     // although this is a nullable column, it's a string which is inherently nullable so use the standard template
                     stringBuilder.Append(standardTemplate.Replace(ColumnClrNamePlaceholder, column.ClrName).Replace(ColumnClrDataTypeNamePlaceholder, column.ClrDataType.Name).Replace(ColumnSqlNamePlaceholder, column.SqlName));
                  }
                  else
                  {
                     // remove the triling "?" from the native datatype
                     clrNativeDataTypeName = column.ClrNativeDataTypeName.Substring(0, column.ClrNativeDataTypeName.Length - 1);

                     stringBuilder.Append(standardTemplateNullable.Replace(ColumnClrNamePlaceholder, column.ClrName).Replace(SqlTableColumnClrNativeDataTypeNamePlaceholder, clrNativeDataTypeName).Replace(ColumnSqlNamePlaceholder, column.SqlName));
                  }
               }
               else
               {
                  // this is a standard NON-NULLABLE column
                  stringBuilder.Append(standardTemplate.Replace(ColumnClrNamePlaceholder, column.ClrName).Replace(ColumnClrDataTypeNamePlaceholder, column.ClrDataType.Name).Replace(ColumnSqlNamePlaceholder, column.SqlName));
               }
            }
            else
            {
               // this is an enum
               stringBuilder.Append(enumTemplate.Replace(ColumnClrNamePlaceholder, MakeEnumForeignKeyName(column.ClrName)).Replace(ColumnClrNamePlaceholderEnum, MakeEnumForeignKeyName(column.ClrName) + Settings.Instance.ProjectSettings.EnumSuffix).Replace(ColumnClrDataTypeNamePlaceholder, MakeEnumForeignKeyName(column.ClrDataType.Name)).Replace(ColumnSqlNamePlaceholder, column.SqlName));
            }
         }

         if (stringBuilder.Length > 0)
         {
            // remove the last comma
            stringBuilder.Length--;
         }

         return stringBuilder.ToString();
      }

      /// <summary>Makes the populate model properties from SQL values.</summary>
      /// <param name="columns">The columns.</param>
      /// <returns>The populate model properties</returns>
      private static string MakePopulateModelPropertiesFromSqlValues(IEnumerable<ISqlColumn> columns)
      {
         string template = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateModelProperty].FileContents;
         string templateNullable = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateModelPropertyNullable].FileContents;
         string clrNativeDataTypeName;

         StringBuilder stringBuilder = new StringBuilder(string.Empty);
         foreach (ISqlColumn column in columns)
         {
            if (column.ClrDataType == typeof(string))
            {
               // use the standard template for strings which are inherently nullable
               stringBuilder.Append(template.Replace(ColumnClrNamePlaceholder, column.ClrName).Replace(ColumnClrDataTypeNamePlaceholder, column.ClrDataType.Name).Replace(ColumnSqlNamePlaceholder, column.SqlName));
            }
            else
            {
               clrNativeDataTypeName = column.ClrNativeDataTypeName.EndsWith("?") ? column.ClrNativeDataTypeName.Substring(0, column.ClrNativeDataTypeName.Length - 1) : column.ClrNativeDataTypeName;
               stringBuilder.Append(templateNullable.Replace(ColumnClrNamePlaceholder, column.ClrName).Replace(SqlTableColumnClrNativeDataTypeNamePlaceholder, clrNativeDataTypeName).Replace(ColumnSqlNamePlaceholder, column.SqlName));
            }
         }

         if (stringBuilder.Length > 0)
         {
            // remove the last comma
            stringBuilder.Length--;
         }

         return stringBuilder.ToString();
      }

      /// <summary>Makes the populate SQL param values.</summary>
      /// <param name="sqlTable">The SQL table.</param>
      /// <param name="includeIdentityColumns">if set to <c>true</c> [include identity columns].</param>
      /// <returns>The populate sql param values</returns>
      private static string MakePopulateSqlParamValues(ISqlTable sqlTable, bool includeIdentityColumns)
      {
         string standardTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateSqlParam].FileContents;
         string enumTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateSqlParamEnum].FileContents;

         StringBuilder stringBuilder = new StringBuilder(string.Empty);
         foreach (ISqlTableColumn column in sqlTable.Columns.Where(column => includeIdentityColumns || column.IsIdentity == false))
         {
            if (column.ForeignKey == null || column.ForeignKey.IsEnum == false)
            {
               // standard column
               stringBuilder.Append(standardTemplate.Replace(ColumnSqlNamePlaceholder, column.SqlName).Replace(SqlEntityCamelCaseClrNamePlaceholder, sqlTable.CamelCaseClrName).Replace(ColumnClrNamePlaceholder, column.ClrName));
            }
            else
            {
               // this is an enum column
               stringBuilder.Append(enumTemplate.Replace(ColumnSqlNamePlaceholder, column.SqlName).Replace(SqlEntityCamelCaseClrNamePlaceholder, sqlTable.CamelCaseClrName).Replace(ColumnClrNamePlaceholder, MakeEnumForeignKeyName(column.ClrName)));
            }
         }

         if (stringBuilder.Length == 0)
         {
            stringBuilder.Append(NoNonIdentityColumnsMessage);
         }

         return stringBuilder.ToString();
      }

      /// <summary>Makes the populate SQL stored procedure param values.</summary>
      /// <param name="parameters">The parameters.</param>
      /// <returns>The populate SQL stored procedure param values</returns>
      private static string MakePopulateSqlStoredProcedureParamValues(IEnumerable<ISqlColumn> parameters)
      {
         string template = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateSqlStoredProcedureParam].FileContents;

         StringBuilder stringBuilder = new StringBuilder(string.Empty);
         foreach (ISqlColumn column in parameters)
         {
            stringBuilder.Append(template.Replace(ColumnSqlNamePlaceholder, column.SqlName).Replace(ColumnCamelCaseClrNamePlaceholder, column.CamelCaseClrName));
         }

         return stringBuilder.ToString();
      }

      /// <summary>Makes the sproc CLR parameters.</summary>
      /// <param name="parameters">The parameters.</param>
      /// <returns>The Sproc CLR Parameters</returns>
      private static string MakeSprocClrParameters(IEnumerable<ISqlColumn> parameters)
      {
         const string Template = "{0} {1}, ";

         StringBuilder stringBuilder = new StringBuilder(string.Empty);
         foreach (ISqlColumn parameter in parameters)
         {
            stringBuilder.AppendFormat(Template, parameter.ClrNativeDataTypeName, parameter.CamelCaseClrName);
         }

         // remove the last space+comma
         if (stringBuilder.Length >= 2)
         {
            stringBuilder.Length = stringBuilder.Length - 2;
         }

         return stringBuilder.ToString();
      }

      /// <summary>Makes the SQL sets from columns i.e. set col1=@col1</summary>
      /// <param name="columns">The columns.</param>
      /// <returns>The SQL Sets from columns</returns>
      private static string MakeSqlSetsFromColumns(IEnumerable<ISqlColumn> columns)
      {
         const string Template = "[{0}]=@{0},";

         StringBuilder stringBuilder = new StringBuilder(string.Empty);
         foreach (ISqlTableColumn column in columns)
         {
            if (column.IsIdentity == false)
            {
               stringBuilder.AppendFormat(Template, column.SqlName);
            }
         }

         // remove the last comma
         if (stringBuilder.Length > 0)
         {
            stringBuilder.Length--;
         }

         return stringBuilder.ToString();
      }

      /// <summary>Populates the settings placeholders.</summary>
      /// <param name="text">The text.</param>
      /// <returns>A string with the settings placeholders populated from the Settings.Instance</returns>
      private static string PopulateSettingsPlaceholders(string text)
      {
         return text.Replace(SettingsInstanceModelClassNamePlaceholder, Settings.Instance.ProjectSettings.ModelClassName).Replace(SettingsInstanceModelClassSuffixPlaceholder, Settings.Instance.ProjectSettings.ModelClassSuffix).Replace(SettingsInstanceServiceClassSuffixPlaceholder, Settings.Instance.ProjectSettings.ServiceClassSuffix).Replace(SettingsInstanceStoredProceduresClassNamePlaceholder, Settings.Instance.ProjectSettings.StoredProceduresClassName).Replace(SettingsInstanceViewsClassNamePlaceholder, Settings.Instance.ProjectSettings.ViewsClassName).Replace(SettingsInstanceCommandsClassNamePlaceholder, Settings.Instance.ProjectSettings.CommandsClassName).Replace(SettingsInstanceNamespacePlaceholder, Settings.Instance.ProjectSettings.Namespace).Replace(SettingsInstanceConnectionStringNamePlaceholder, Settings.Instance.ProjectSettings.ConnectionStringName).Replace(EnumSuffixPlaceholder, Settings.Instance.ProjectSettings.EnumSuffix);
      }

      /// <summary>Makes the model file.</summary>
      /// <returns>The contents of the model file</returns>
      private string MakeModelsFile()
      {
         string modelPropertyTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ModelProperty].FileContents;
         string modelClassTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ModelClass].FileContents;
         string modelsFileTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ModelsFile].FileContents;
         string modelEnumTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ModelEnum].FileContents;
         string modelSortExtensionMethodTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ModelSortExtensionMethod].FileContents;

         StringBuilder content = new StringBuilder(string.Empty);
         StringBuilder properties = new StringBuilder(string.Empty);
         StringBuilder columnEnumProperties = new StringBuilder(string.Empty);
         StringBuilder extensionMethods = new StringBuilder(string.Empty);
         StringBuilder sortExtensionMethodProperties = new StringBuilder(string.Empty);

         string sqlValueLiteral;

         // TABLES
         foreach (SqlTable sqlTable in _tables)
         {
            if (Settings.Instance.ProjectSettings.IncludedTables.Contains(sqlTable.SqlName))
            {
               properties.Length = 0;
               columnEnumProperties.Length = 0;
               extensionMethods.Length = 0;
               sortExtensionMethodProperties.Length = 0;

               if (Settings.Instance.ProjectSettings.EnumTables.Contains(sqlTable.SqlName))
               {
                  // THIS IS AN ENUM TABLE

                  // get the primary key
                  ISqlTableColumn valueColumn = sqlTable.Columns.Find(x => x.IsPrimaryKey);
                  ISqlTableColumn nameColumn = sqlTable.Columns.Find(x => x.ClrNativeDataTypeName == "string");

                  foreach (DataRow dataRow in GetEnumValuesFromTable(valueColumn, nameColumn, sqlTable).Rows)
                  {
                     sqlValueLiteral = Convert.ToString(dataRow[nameColumn.SqlName]);
                     properties.AppendFormat("\t\t\t[Description(\"{2}\")]\r\n\t\t\t{0} = {1},\r\n", Dictionary.MakeClrName(sqlValueLiteral), Convert.ToString(dataRow[valueColumn.SqlName]), Text.MakeSafeForAttributeDescription(sqlValueLiteral));
                  }

                  if (properties.Length > 0)
                  {
                     // remove the last comma + carriage return
                     properties.Length = properties.Length - 3;
                     properties.Append(CrLf);

                     content.Append(modelEnumTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(ContentPlaceholder, properties.ToString()));
                  }

                  RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("Enum for table {0}.{1} built", sqlTable.Schema, sqlTable.SqlName)));
               }
               else
               {
                  // THIS IS A STANDARD TABLE
                  foreach (ISqlTableColumn sqlTableColumn in sqlTable.Columns)
                  {
                     if (sqlTableColumn.ForeignKey == null || sqlTableColumn.ForeignKey.IsEnum == false)
                     {
                        // this is a standard column
                        columnEnumProperties.AppendFormat(ColumnEnumTemplate, sqlTableColumn.ClrName);

                        properties.Append(modelPropertyTemplate.Replace(SqlTableColumnClrNativeDataTypeNamePlaceholder, sqlTableColumn.ClrNativeDataTypeName).Replace(SqlTableColumnClrNamePlaceholder, sqlTableColumn.ClrName));

                        if (sqlTableColumn.IsNullable)
                        {
                           sortExtensionMethodProperties.AppendFormat(SortExtensionMethodNullableColumnTemplate, sqlTable.ClrName, sqlTableColumn.ClrName, sqlTableColumn.ClrNativeDataTypeName.Substring(0, sqlTableColumn.ClrNativeDataTypeName.Length - 1));
                        }
                        else
                        {
                           sortExtensionMethodProperties.AppendFormat(SortExtensionMethodColumnTemplate, sqlTable.ClrName, sqlTableColumn.ClrName);
                        }
                     }
                     else
                     {
                        // this is an enum column
                        columnEnumProperties.AppendFormat(ColumnEnumTemplate, MakeEnumForeignKeyName(sqlTableColumn.ClrName));

                        properties.Append(modelPropertyTemplate.Replace(SqlTableColumnClrNativeDataTypeNamePlaceholder, MakeEnumForeignKeyName(sqlTableColumn.ClrName) + Settings.Instance.ProjectSettings.EnumSuffix).Replace(SqlTableColumnClrNamePlaceholder, MakeEnumForeignKeyName(sqlTableColumn.ClrName)));

                        sortExtensionMethodProperties.AppendFormat(SortExtensionMethodColumnTemplate, sqlTable.ClrName, MakeEnumForeignKeyName(sqlTableColumn.ClrName));
                     }
                  }

                  if (columnEnumProperties.Length > 0)
                  {
                     // remove the last comma + carriage return
                     columnEnumProperties.Length = columnEnumProperties.Length - 3;
                     columnEnumProperties.Append(CrLf);
                  }

                  if (properties.Length > 0)
                  {
                     content.Append(modelClassTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(ContentPlaceholder, properties.ToString()).Replace(ColumnEnumPlaceholder, columnEnumProperties.ToString()).Replace(ExtensionMethodsPlaceholder, extensionMethods.ToString()));

                     content.Append(modelSortExtensionMethodTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(ContentPlaceholder, sortExtensionMethodProperties.ToString()));
                  }

                  RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("Model for table {0}.{1} built", sqlTable.Schema, sqlTable.SqlName)));
               }
            }
         }

         // STORED PROCEDURES
         foreach (SqlStoredProcedure sqlStoredProcedure in _storedProcedures)
         {
            if (Settings.Instance.ProjectSettings.IncludedStoredProcedures.Contains(sqlStoredProcedure.SqlName))
            {
               properties.Length = 0;
               columnEnumProperties.Length = 0;
               extensionMethods.Length = 0;
               sortExtensionMethodProperties.Length = 0;

               foreach (ISqlColumn sqlTableColumn in sqlStoredProcedure.Columns)
               {
                  columnEnumProperties.AppendFormat(ColumnEnumTemplate, sqlTableColumn.ClrName);

                  if (sqlTableColumn.ClrNativeDataTypeName == "string")
                  {
                     sortExtensionMethodProperties.AppendFormat(SortExtensionMethodColumnTemplate, sqlStoredProcedure.ClrName, sqlTableColumn.ClrName);
                  }
                  else
                  {
                     sortExtensionMethodProperties.AppendFormat(SortExtensionMethodNullableColumnTemplate, sqlStoredProcedure.ClrName, sqlTableColumn.ClrName, sqlTableColumn.ClrNativeDataTypeName.Substring(0, sqlTableColumn.ClrNativeDataTypeName.Length - 1));
                  }

                  properties.Append(modelPropertyTemplate.Replace(SqlTableColumnClrNativeDataTypeNamePlaceholder, sqlTableColumn.ClrNativeDataTypeName).Replace(SqlTableColumnClrNamePlaceholder, sqlTableColumn.ClrName));
               }

               if (columnEnumProperties.Length > 0)
               {
                  // remove the last comma + carriage return
                  columnEnumProperties.Length = columnEnumProperties.Length - 3;
                  columnEnumProperties.Append(CrLf);
               }

               if (properties.Length > 0)
               {
                  content.Append(modelClassTemplate.Replace(SqlEntityClrNamePlaceholder, sqlStoredProcedure.ClrName).Replace(ContentPlaceholder, properties.ToString()).Replace(ColumnEnumPlaceholder, columnEnumProperties.ToString()).Replace(ExtensionMethodsPlaceholder, extensionMethods.ToString()));

                  content.Append(modelSortExtensionMethodTemplate.Replace(SqlEntityClrNamePlaceholder, sqlStoredProcedure.ClrName).Replace(ContentPlaceholder, sortExtensionMethodProperties.ToString()));
               }

               RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("Model for stored procedure {0}.{1} built", sqlStoredProcedure.Schema, sqlStoredProcedure.SqlName)));
            }
         }

         // VIEWS
         foreach (SqlView sqlView in _views)
         {
            if (Settings.Instance.ProjectSettings.IncludedViews.Contains(sqlView.SqlName))
            {
               properties.Length = 0;
               columnEnumProperties.Length = 0;
               extensionMethods.Length = 0;
               sortExtensionMethodProperties.Length = 0;

               foreach (ISqlColumn sqlTableColumn in sqlView.Columns)
               {
                  columnEnumProperties.AppendFormat(ColumnEnumTemplate, sqlTableColumn.ClrName);

                  if (sqlTableColumn.ClrNativeDataTypeName == "string")
                  {
                     sortExtensionMethodProperties.AppendFormat(SortExtensionMethodColumnTemplate, sqlView.ClrName, sqlTableColumn.ClrName);
                  }
                  else
                  {
                     sortExtensionMethodProperties.AppendFormat(SortExtensionMethodNullableColumnTemplate, sqlView.ClrName, sqlTableColumn.ClrName, sqlTableColumn.ClrNativeDataTypeName.Substring(0, sqlTableColumn.ClrNativeDataTypeName.Length - 1));
                  }

                  properties.Append(modelPropertyTemplate.Replace(SqlTableColumnClrNativeDataTypeNamePlaceholder, sqlTableColumn.ClrNativeDataTypeName).Replace(SqlTableColumnClrNamePlaceholder, sqlTableColumn.ClrName));
               }

               if (columnEnumProperties.Length > 0)
               {
                  // remove the last comma + carriage return
                  columnEnumProperties.Length = columnEnumProperties.Length - 3;
                  columnEnumProperties.Append(CrLf);
               }

               if (properties.Length > 0)
               {
                  content.Append(modelClassTemplate.Replace(SqlEntityClrNamePlaceholder, sqlView.ClrName).Replace(ContentPlaceholder, properties.ToString()).Replace(ColumnEnumPlaceholder, columnEnumProperties.ToString()).Replace(ExtensionMethodsPlaceholder, extensionMethods.ToString()));

                  content.Append(modelSortExtensionMethodTemplate.Replace(SqlEntityClrNamePlaceholder, sqlView.ClrName).Replace(ContentPlaceholder, sortExtensionMethodProperties.ToString()));
               }

               RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("Model for view {0}.{1} built", sqlView.Schema, sqlView.SqlName)));
            }
         }

         return PopulateSettingsPlaceholders(modelsFileTemplate.Replace(ContentPlaceholder, content.ToString()));
      }

      /// <summary>Makes the services file.</summary>
      /// <returns>The contents of the service file</returns>
      private string MakeServicesFile()
      {
         string servicesFileTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicesFile].FileContents;
         string serviceClassTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceClass].FileContents;

         string serviceCreateTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceCreate].FileContents;
         string serviceRetrieveTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceRetrieve].FileContents;
         string serviceRetrieveAllTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceRetrieveAll].FileContents;
         string serviceUpdateTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceUpdate].FileContents;
         string serviceDeleteTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceDelete].FileContents;
         string servicePopulateModelTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServicePopulateModel].FileContents;
         string serviceStoredProceduresClassTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceStoredProceduresClass].FileContents;
         string serviceStoredProcedureTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceStoredProcedure].FileContents;
         string serviceStoredProcedureVoidTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceStoredProcedureVoid].FileContents;
         string serviceViewsClassTemplate = Settings.Instance.ProjectSettings.Templates[TemplateTypeEnum.ServiceViewsClass].FileContents;

         StringBuilder content = new StringBuilder(string.Empty);
         StringBuilder classContent = new StringBuilder(string.Empty);

         ISqlColumn primaryKeyColumn;

         // TABLES
         foreach (SqlTable sqlTable in _tables)
         {
            if (Settings.Instance.ProjectSettings.IncludedTables.Contains(sqlTable.SqlName))
            {
               classContent.Length = 0;
               primaryKeyColumn = sqlTable.Columns.Find(x => x.IsPrimaryKey);

               if (Settings.Instance.ProjectSettings.EnumTables.Contains(sqlTable.SqlName))
               {
                  // THIS IS AN ENUM TABLE
               }
               else
               {
                  // CREATE
                  classContent.Append(serviceCreateTemplate.Replace(PrimaryKeyColumnClrNativeDataTypeNamePlaceholder, primaryKeyColumn.ClrNativeDataTypeName).Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(SqlEntityCamelCaseClrNamePlaceholder, sqlTable.CamelCaseClrName).Replace(SqlEntitySchemaPlaceholder, sqlTable.Schema).Replace(SqlTableSqlNamePlaceholder, sqlTable.SqlName).Replace(SqlTableColumnsAsCsvPlaceholder, sqlTable.NonIdentityColumnsAsCsv).Replace(SqlTableColumnParametersAsCsvPlaceholder, sqlTable.NonIdentityColumnParametersAsCsv).Replace(PrimaryKeyColumnClrDataTypeNamePlaceholder, primaryKeyColumn.ClrDataType.Name).Replace(MakePopulateSqlEntityParamValuesPlaceholder, MakePopulateSqlParamValues(sqlTable, false)));

                  // RETRIEVE
                  classContent.Append(serviceRetrieveTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(PrimaryKeyColumnClrNativeDataTypeNamePlaceholder, primaryKeyColumn.ClrNativeDataTypeName).Replace(PrimaryKeyColumnCamelCaseClrNamePlaceholder, primaryKeyColumn.CamelCaseClrName).Replace(SqlTableColumnsAsCsvPlaceholder, sqlTable.AllColumnsAsCsv).Replace(SqlEntitySchemaPlaceholder, sqlTable.Schema).Replace(SqlTableSqlNamePlaceholder, sqlTable.SqlName).Replace(PrimaryKeyColumnSqlNamePlaceholder, primaryKeyColumn.SqlName));

                  // RETRIEVE ALL
                  classContent.Append(serviceRetrieveAllTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(SqlEntityCamelCaseClrNamePlaceholder, sqlTable.CamelCaseClrName).Replace(SqlTableColumnsAsCsvPlaceholder, sqlTable.AllColumnsAsCsv).Replace(SqlEntitySchemaPlaceholder, sqlTable.Schema).Replace(SqlTableSqlNamePlaceholder, sqlTable.SqlName));

                  // UPDATE
                  classContent.Append(serviceUpdateTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(SqlEntityCamelCaseClrNamePlaceholder, sqlTable.CamelCaseClrName).Replace(SqlEntitySchemaPlaceholder, sqlTable.Schema).Replace(SqlTableSqlNamePlaceholder, sqlTable.SqlName).Replace(MakePopulateSqlEntityParamValuesPlaceholder, MakePopulateSqlParamValues(sqlTable, true)).Replace(MakeSqlSetsFromColumnsPlaceholder, MakeSqlSetsFromColumns(sqlTable.Columns)).Replace(PrimaryKeyColumnSqlNamePlaceholder, primaryKeyColumn.SqlName));

                  // DELETE
                  classContent.Append(serviceDeleteTemplate.Replace(PrimaryKeyColumnClrNativeDataTypeNamePlaceholder, primaryKeyColumn.ClrNativeDataTypeName).Replace(PrimaryKeyColumnCamelCaseClrNamePlaceholder, primaryKeyColumn.CamelCaseClrName).Replace(SqlEntitySchemaPlaceholder, sqlTable.Schema).Replace(SqlTableSqlNamePlaceholder, sqlTable.SqlName).Replace(PrimaryKeyColumnSqlNamePlaceholder, primaryKeyColumn.SqlName));

                  // POPULATE MODEL
                  classContent.Append(servicePopulateModelTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(MakePopulateModelPropertiesFromSqlValuesPlaceholder, MakePopulateModelPropertiesFromSqlValues(sqlTable.Columns)));
               }

               if (classContent.Length > 0)
               {
                  // Add the class code to the end of the file contents
                  content.Append(serviceClassTemplate.Replace(SqlEntityClrNamePlaceholder, sqlTable.ClrName).Replace(ContentPlaceholder, classContent.ToString()));

                  RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("CRUD methods for table {0}.{1} built", sqlTable.Schema, sqlTable.SqlName)));
               }
            }
         }

         // STORED PROCEDURES
         classContent.Length = 0;

         foreach (SqlStoredProcedure storedProcedure in _storedProcedures)
         {
            if (Settings.Instance.ProjectSettings.IncludedStoredProcedures.Contains(storedProcedure.SqlName))
            {
               if (storedProcedure.Columns.Count > 0)
               {
                  // PROCEDURE
                  classContent.Append(serviceStoredProcedureTemplate.Replace(StoredProcedureClrNamePlaceholder, storedProcedure.ClrName).Replace(StoredProcedureCamelCaseClrNamePlaceholder, storedProcedure.CamelCaseClrName).Replace(MakePopulateSqlEntityParamValuesPlaceholder, MakePopulateSqlStoredProcedureParamValues(storedProcedure.Parameters)).Replace(MakeSprocClrParametersPlaceholder, MakeSprocClrParameters(storedProcedure.Parameters)).Replace(SqlEntitySchemaPlaceholder, storedProcedure.Schema).Replace(StoredProcedureSqlNamePlaceholder, storedProcedure.SqlName));

                  // POPULATE MODEL
                  classContent.Append(servicePopulateModelTemplate.Replace(SqlEntityClrNamePlaceholder, storedProcedure.ClrName).Replace(MakePopulateModelPropertiesFromSqlValuesPlaceholder, MakePopulateModelPropertiesFromSqlValues(storedProcedure.Columns)));
               }
               else
               {
                  // VOID PROCEDURE 
                  classContent.Append(serviceStoredProcedureVoidTemplate.Replace(StoredProcedureClrNamePlaceholder, storedProcedure.ClrName).Replace(MakeSprocClrParametersPlaceholder, MakeSprocClrParameters(storedProcedure.Parameters)).Replace(SqlEntitySchemaPlaceholder, storedProcedure.Schema).Replace(StoredProcedureSqlNamePlaceholder, storedProcedure.SqlName)).Replace(MakePopulateSqlEntityParamValuesPlaceholder, MakePopulateSqlStoredProcedureParamValues(storedProcedure.Parameters));
               }

               RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("Execute method for stored procedure {0}.{1} built", storedProcedure.Schema, storedProcedure.SqlName)));
            }
         }

         if (classContent.Length > 0)
         {
            // add the class code to the end of the file contents
            content.Append(serviceStoredProceduresClassTemplate.Replace(ContentPlaceholder, classContent.ToString()));
         }

         // VIEWS
         classContent.Length = 0;
         foreach (SqlView sqlView in _views)
         {
            if (Settings.Instance.ProjectSettings.IncludedViews.Contains(sqlView.SqlName))
            {
               // VIEW
               classContent.Append(serviceRetrieveAllTemplate.Replace(SqlEntityClrNamePlaceholder, sqlView.ClrName).Replace(SqlEntityCamelCaseClrNamePlaceholder, sqlView.CamelCaseClrName).Replace(SqlTableColumnsAsCsvPlaceholder, sqlView.ColumnsAsCsv).Replace(SqlEntitySchemaPlaceholder, sqlView.Schema).Replace(SqlTableSqlNamePlaceholder, sqlView.SqlName));

               // POPULATE MODEL
               classContent.Append(servicePopulateModelTemplate.Replace(SqlEntityClrNamePlaceholder, sqlView.ClrName).Replace(MakePopulateModelPropertiesFromSqlValuesPlaceholder, MakePopulateModelPropertiesFromSqlValues(sqlView.Columns)));
            }

            RaiseProgressMessageEvent(new ProgressMessageEventArgs(string.Format("Retrieve method for view {0}.{1} built", sqlView.Schema, sqlView.SqlName)));
         }

         if (classContent.Length > 0)
         {
            // add the class code to the end of the file contents
            content.Append(serviceViewsClassTemplate.Replace(ContentPlaceholder, classContent.ToString()));
         }

         // insert the class code into the file template
         return PopulateSettingsPlaceholders(servicesFileTemplate.Replace(ContentPlaceholder, content.ToString()));
      }
   }
}
