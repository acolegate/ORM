using System.Collections.Generic;

namespace ORM.Library.Interfaces
{
	internal interface ISqlStoredProcedure : ISqlEntity
	{
		List<ISqlColumn> Columns { get; set; }
		List<ISqlColumn> Parameters { get; set; }

		string ParametersAsNullsCsv { get; }
	}
}