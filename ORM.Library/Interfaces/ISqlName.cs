namespace ORM.Library.Interfaces
{
	public interface ISqlName
	{
		string ClrName { get; set; }
		string CamelCaseClrName { get; set; }
		string SqlName { get; set; } 
	}
}