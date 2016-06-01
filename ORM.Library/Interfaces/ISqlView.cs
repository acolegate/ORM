using System.Collections.Generic;

namespace ORM.Library.Interfaces
{
	public interface ISqlView : ISqlEntity
	{
		List<ISqlColumn> Columns { get; set; }
		string ColumnsAsCsv { get; }
	}
}