namespace ORM.Library.Interfaces
{
	public interface IForeignKey
	{
		string PrimaryKeySchemaName { get; set; }
		string PrimaryKeySqlTableName { get; set; }
		string PrimaryKeySqlColumnName { get; set; }
		bool IsEnum { get; set; }
	}
}