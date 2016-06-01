namespace ORM.Library.Interfaces
{
	public interface ISqlEntity : ISqlName
	{
		string Schema { get; set; }
	}
}