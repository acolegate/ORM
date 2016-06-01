using System.Collections.Generic;

using ORM.Library.HelperFunctions;
using ORM.Library.Interfaces;

namespace ORM.Library.Models
{
   public class SqlTable : ISqlTable
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="SqlTable" /> class.
      /// </summary>
      /// <param name="schema">The schema.</param>
      /// <param name="sqlName">Name of the SQL.</param>
      public SqlTable(string schema, string sqlName)
      {
         Schema = schema;
         SqlName = sqlName;
         ClrName = Dictionary.MakeClrName(sqlName);
         CamelCaseClrName = Text.LowercaseFirstLetter(ClrName);
         Columns = new List<ISqlTableColumn>();
      }

      /// <summary>Gets the columns as CSV.</summary>
      /// <value>The columns as CSV.</value>
      public string AllColumnsAsCsv
      {
         get
         {
            return Data.ColumnsAsCsv(Columns, true);
         }
      }

      /// <summary>Gets or sets the name of the camel case CLR.</summary>
      /// <value>The name of the camel case CLR.</value>
      public string CamelCaseClrName { get; set; }

      /// <summary>Gets or sets the name of the CLR.</summary>
      /// <value>The name of the CLR.</value>
      public string ClrName { get; set; }

      /// <summary>Gets or sets the columns.</summary>
      /// <value>The columns.</value>
      public List<ISqlTableColumn> Columns { get; set; }

      /// <summary>Gets the non identity column parameters as CSV.</summary>
      /// <value>The non identity column parameters as CSV.</value>
      public string NonIdentityColumnParametersAsCsv
      {
         get
         {
            return Data.NonIdentityColumnParametersAsCsv(Columns);
         }
      }

      /// <summary>Gets the non identity columns as CSV.</summary>
      /// <value>The non identity columns as CSV.</value>
      public string NonIdentityColumnsAsCsv
      {
         get
         {
            return Data.ColumnsAsCsv(Columns, false);
         }
      }

      /// <summary>Gets or sets the schema.</summary>
      /// <value>The schema.</value>
      public string Schema { get; set; }

      /// <summary>Gets or sets the name of the SQL.</summary>
      /// <value>The name of the SQL.</value>
      public string SqlName { get; set; }
   }
}
