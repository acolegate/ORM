using System;
using System.Data;

using ORM.Library.HelperFunctions;
using ORM.Library.Interfaces;

namespace ORM.Library.Models
{
	internal class SqlTableColumn : ISqlTableColumn
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlTableColumn" /> class.
		/// </summary>
		/// <param name="sqlName">Name of the SQL.</param>
		/// <param name="sqlDbType">Type of the SQL db.</param>
		/// <param name="isIdentity">if set to <c>true</c> [is identity].</param>
		/// <param name="isPrimaryKey">if set to <c>true</c> [is primary key].</param>
		/// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
		public SqlTableColumn(string sqlName, SqlDbType sqlDbType, bool isIdentity, bool isPrimaryKey, bool isNullable)
		{
			SqlName = sqlName;
			ClrName = Dictionary.MakeClrName(sqlName);
			CamelCaseClrName = Text.LowercaseFirstLetter(ClrName);
			ClrDataType = Data.GetClrType(sqlDbType);
			IsPrimaryKey = isPrimaryKey;
			IsIdentity = isIdentity;
			IsNullable = isNullable;
			ClrNativeDataTypeName = Data.GetClrNativeDataTypeName(ClrDataType, IsNullable);
		}

		/// <summary>Gets or sets a value indicating whether this instance is nullable.</summary>
		/// <value><c>true</c> if this instance is nullable; otherwise, <c>false</c>.</value>
		public bool IsNullable { get; set; }

		/// <summary>Gets or sets the name of the camel case CLR.</summary>
		/// <value>The name of the camel case CLR.</value>
		public string CamelCaseClrName { get; set; }

		/// <summary>Gets or sets the type of the CLR data.</summary>
		/// <value>The type of the CLR data.</value>
		public Type ClrDataType { get; set; }

		/// <summary>Gets or sets the name of the CLR data type.</summary>
		/// <value>The name of the CLR data type.</value>
		public string ClrNativeDataTypeName { get; set; }

		/// <summary>Gets or sets the name of the CLR.</summary>
		/// <value>The name of the CLR.</value>
		public string ClrName { get; set; }

		/// <summary>Gets or sets a value indicating whether this instance is identity.</summary>
		/// <value>
		/// <c>true</c> if this instance is identity; otherwise, <c>false</c>.
		/// </value>
		public bool IsIdentity { get; set; }

		/// <summary>Gets or sets a value indicating whether this instance is primary key.</summary>
		/// <value>
		/// <c>true</c> if this instance is primary key; otherwise, <c>false</c>.
		/// </value>
		public bool IsPrimaryKey { get; set; }

		/// <summary>Gets or sets the foreign key.</summary>
		/// <value>The foreign key.</value>
		public IForeignKey ForeignKey { get; set; }

		/// <summary>Gets or sets the name of the SQL.</summary>
		/// <value>The name of the SQL.</value>
		public string SqlName { get; set; }
	}
}