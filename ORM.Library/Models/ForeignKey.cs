using ORM.Library.Interfaces;

namespace ORM.Library.Models
{
	internal class ForeignKey : IForeignKey
	{
		public bool IsEnum { get; set; }
		public string PrimaryKeySchemaName { get; set; }
		public string PrimaryKeySqlColumnName { get; set; }
		public string PrimaryKeySqlTableName { get; set; }
	}
}