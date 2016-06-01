namespace ORM.Library.Interfaces
{
	public interface ISqlTableColumn : ISqlColumn
	{
		bool IsIdentity { get; set; }
		bool IsPrimaryKey { get; set; }
		IForeignKey ForeignKey { get; set; }
		bool IsNullable { get; set; }
	}
}