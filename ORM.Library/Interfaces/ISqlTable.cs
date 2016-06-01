using System.Collections.Generic;

namespace ORM.Library.Interfaces
{
	public interface ISqlTable : ISqlEntity
	{
		List<ISqlTableColumn> Columns { get; set; }
	}
}