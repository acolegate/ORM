using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using ORM.Library.Interfaces;

namespace ORM.Library.HelperFunctions
{
   internal static class Data
   {
      /// <summary>Returns all columns as a CSV</summary>
      /// <param name="columns">The columns.</param>
      /// <returns>A CSV of all columns</returns>
      public static string AllColumnsAsCsv(IEnumerable<ISqlColumn> columns)
      {
         StringBuilder csv = new StringBuilder(string.Empty);

         foreach (ISqlColumn column in columns)
         {
            csv.AppendFormat("[{0}],", column.SqlName);
         }

         if (csv.Length > 0)
         {
            csv.Length--;
         }

         return csv.ToString();
      }

      /// <summary>Columnses as CSV.</summary>
      /// <param name="columns">The columns.</param>
      /// <param name="includecIdentityColumns">if set to <c>true</c> [includec identity columns].</param>
      /// <returns>A CSV of all columns</returns>
      public static string ColumnsAsCsv(IEnumerable<ISqlTableColumn> columns, bool includecIdentityColumns)
      {
         StringBuilder csv = new StringBuilder(string.Empty);

         foreach (ISqlTableColumn column in columns)
         {
            if (includecIdentityColumns || column.IsIdentity == false)
            {
               csv.AppendFormat("[{0}],", column.SqlName);
            }
         }

         if (csv.Length > 0)
         {
            csv.Length--;
         }

         return csv.ToString();
      }

      /// <summary>Gets the name of the native version of the CLR type i.e. Int32 -&gt; "long"</summary>
      /// <param name="type">The type.</param>
      /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
      /// <returns>The name of the native version of the CLR type</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">An argument out of range exception</exception>
      public static string GetClrNativeDataTypeName(Type type, bool isNullable)
      {
         if (type == typeof(object))
         {
            return "object" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(string))
         {
            return "string";
         }

         if (type == typeof(decimal))
         {
            return "decimal" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(bool))
         {
            return "bool" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(char))
         {
            return "char" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(byte))
         {
            return "byte" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(sbyte))
         {
            return "sbyte" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(short))
         {
            return "short" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(int))
         {
            return "int" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(long))
         {
            return "long" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(ushort))
         {
            return "ushort" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(uint))
         {
            return "uint" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(ulong))
         {
            return "ulong" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(float))
         {
            return "single" + (isNullable ? "?" : string.Empty);
         }

         if (type == typeof(double))
         {
            return "double" + (isNullable ? "?" : string.Empty);
         }

         return type.Name + (isNullable ? "?" : string.Empty);
      }

      /// <summary>Converts a SqlDbType to a CLR datatype</summary>
      /// <param name="sqlType">The SqlDbType</param>
      /// <returns>A CLR datatype</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">An argument out of range exception</exception>
      public static Type GetClrType(SqlDbType sqlType)
      {
         switch (sqlType)
         {
            case SqlDbType.BigInt:
               return typeof(long);

            case SqlDbType.Binary:
            case SqlDbType.Image:
            case SqlDbType.Timestamp:
            case SqlDbType.VarBinary:
               return typeof(byte[]);

            case SqlDbType.Bit:
               return typeof(bool);

            case SqlDbType.Char:
            case SqlDbType.NChar:
            case SqlDbType.NText:
            case SqlDbType.NVarChar:
            case SqlDbType.Text:
            case SqlDbType.VarChar:
            case SqlDbType.Xml:
               return typeof(string);

            case SqlDbType.DateTime:
            case SqlDbType.SmallDateTime:
            case SqlDbType.Date:
            case SqlDbType.Time:
            case SqlDbType.DateTime2:
               return typeof(DateTime);

            case SqlDbType.Decimal:
            case SqlDbType.Money:
            case SqlDbType.SmallMoney:
               return typeof(decimal);

            case SqlDbType.Float:
               return typeof(double);

            case SqlDbType.Int:
               return typeof(int);

            case SqlDbType.Real:
               return typeof(float);

            case SqlDbType.UniqueIdentifier:
               return typeof(Guid);

            case SqlDbType.SmallInt:
               return typeof(short);

            case SqlDbType.TinyInt:
               return typeof(byte);

            case SqlDbType.Variant:
            case SqlDbType.Udt:
               return typeof(object);

            case SqlDbType.Structured:
               return typeof(DataTable);

            case SqlDbType.DateTimeOffset:
               return typeof(DateTimeOffset);

            default:
               throw new ArgumentOutOfRangeException("sqlType");
         }
      }

      /// <summary>Gets the type of the SQL db.</summary>
      /// <param name="type">The type.</param>
      /// <returns>The SQL DB Type</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">An argument out of range exception</exception>
      public static SqlDbType GetSqlDbType(Type type)
      {
         if (type == typeof(long))
         {
            return SqlDbType.BigInt;
         }

         if (type == typeof(byte[]))
         {
            return SqlDbType.VarBinary;
         }

         if (type == typeof(bool))
         {
            return SqlDbType.Bit;
         }

         if (type == typeof(string))
         {
            return SqlDbType.NVarChar;
         }

         if (type == typeof(DateTime))
         {
            return SqlDbType.DateTime;
         }

         if (type == typeof(decimal))
         {
            return SqlDbType.Decimal;
         }

         if (type == typeof(double))
         {
            return SqlDbType.Float;
         }

         if (type == typeof(int))
         {
            return SqlDbType.Int;
         }

         if (type == typeof(float))
         {
            return SqlDbType.Real;
         }

         if (type == typeof(Guid))
         {
            return SqlDbType.UniqueIdentifier;
         }

         if (type == typeof(short))
         {
            return SqlDbType.SmallInt;
         }

         if (type == typeof(byte))
         {
            return SqlDbType.TinyInt;
         }

         if (type == typeof(object))
         {
            return SqlDbType.Variant;
         }

         if (type == typeof(DataTable))
         {
            return SqlDbType.Structured;
         }

         if (type == typeof(DateTimeOffset))
         {
            return SqlDbType.DateTimeOffset;
         }

         throw new ArgumentOutOfRangeException("type");
      }

      /// <summary>Geta a list of all non-identity column parameters as a CSV.</summary>
      /// <param name="columns">The columns.</param>
      /// <returns>The columns as a csv lit of parameters</returns>
      public static string NonIdentityColumnParametersAsCsv(IEnumerable<ISqlColumn> columns)
      {
         StringBuilder csv = new StringBuilder(string.Empty);

         foreach (ISqlTableColumn column in columns.Cast<ISqlTableColumn>().Where(column => column.IsIdentity == false))
         {
            csv.AppendFormat("@{0},", column.SqlName);
         }

         if (csv.Length > 0)
         {
            csv.Length--;
         }

         return csv.ToString();
      }

      /// <summary>Parses the type of the SQL data.</summary>
      /// <param name="sqlDataType">Type of the SQL data.</param>
      /// <returns>A Sql DataType</returns>
      public static SqlDbType ParseSqlDataType(string sqlDataType)
      {
         return (SqlDbType)Enum.Parse(typeof(SqlDbType), sqlDataType, true);
      }
   }
}
