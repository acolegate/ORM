using System;
using System.Data;

using ORM.Library.HelperFunctions;
using ORM.Library.Interfaces;

namespace ORM.Library.Models
{
   internal class SqlViewColumn : ISqlColumn
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="SqlViewColumn" /> class.
      /// </summary>
      /// <param name="sqlName">Name of the SQL.</param>
      /// <param name="sqlDbType">Type of the SQL db.</param>
      public SqlViewColumn(string sqlName, SqlDbType sqlDbType)
      {
         SqlName = sqlName;
         ClrName = Dictionary.MakeClrName(sqlName);
         CamelCaseClrName = Text.LowercaseFirstLetter(ClrName);
         ClrDataType = Data.GetClrType(sqlDbType);
         ClrNativeDataTypeName = Data.GetClrNativeDataTypeName(ClrDataType, true);
      }

      /// <summary>Gets or sets the name of the camel case CLR.</summary>
      /// <value>The name of the camel case CLR.</value>
      public string CamelCaseClrName { get; set; }

      /// <summary>Gets or sets the type of the CLR data.</summary>
      /// <value>The type of the CLR data.</value>
      public Type ClrDataType { get; set; }

      /// <summary>Gets or sets the name of the CLR.</summary>
      /// <value>The name of the CLR.</value>
      public string ClrName { get; set; }

      /// <summary>Gets or sets the name of the CLR data type.</summary>
      /// <value>The name of the CLR data type.</value>
      public string ClrNativeDataTypeName { get; set; }

      /// <summary>Gets or sets the name of the SQL.</summary>
      /// <value>The name of the SQL.</value>
      public string SqlName { get; set; }
   }
}
