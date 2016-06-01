using System.Collections.Generic;
using System.Linq;

using ORM.Library.HelperFunctions;
using ORM.Library.Interfaces;

namespace ORM.Library.Models
{
   public class SqlStoredProcedure : ISqlStoredProcedure
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="SqlStoredProcedure" /> class.
      /// </summary>
      /// <param name="schema">The schema.</param>
      /// <param name="sqlName">Name of the SQL.</param>
      public SqlStoredProcedure(string schema, string sqlName)
      {
         Schema = schema;
         ClrName = Dictionary.MakeClrName(sqlName);
         CamelCaseClrName = Text.LowercaseFirstLetter(ClrName);
         Columns = new List<ISqlColumn>();
         Parameters = new List<ISqlColumn>();
         SqlName = sqlName;
      }

      /// <summary>Gets or sets the name of the camel case CLR.</summary>
      /// <value>The name of the camel case CLR.</value>
      public string CamelCaseClrName { get; set; }

      /// <summary>Gets or sets the name of the CLR.</summary>
      /// <value>The name of the CLR.</value>
      public string ClrName { get; set; }

      /// <summary>Gets or sets the columns.</summary>
      /// <value>The columns.</value>
      public List<ISqlColumn> Columns { get; set; }

      /// <summary>Gets or sets the parameters.</summary>
      /// <value>The parameters.</value>
      public List<ISqlColumn> Parameters { get; set; }

      /// <summary>Gets the parameters as nulls CSV.</summary>
      /// <value>The parameters as nulls CSV.</value>
      public string ParametersAsNullsCsv
      {
         get
         {
            return string.Join(",", Enumerable.Repeat("null", Parameters.Count));
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
