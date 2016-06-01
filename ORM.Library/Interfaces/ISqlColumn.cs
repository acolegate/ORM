using System;

namespace ORM.Library.Interfaces
{
	public interface ISqlColumn : ISqlName
	{
		Type ClrDataType { get; set; }
		string ClrNativeDataTypeName { get; set; }
	}
}