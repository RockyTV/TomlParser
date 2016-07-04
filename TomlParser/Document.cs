using System.Collections.Generic;
using System.Linq;

namespace TomlParser
{
	public class TomlDocument
	{
		private TomlKeyValue[] rootValues;
		private TomlTable[] tables;

		public TomlKeyValue[] RootValues
		{
			get { return rootValues; }
			set { rootValues = value; }
		}

		public TomlTable[] Tables
		{
			get { return tables; }
			set { tables = value; }
		}

		public TomlDocument(IEnumerable<TomlKeyValue> rootValues, IEnumerable<TomlTable> tables)
		{
			this.rootValues = (rootValues ?? Enumerable.Empty<TomlKeyValue>()).ToArray();
			this.tables = (tables ?? Enumerable.Empty<TomlTable>()).ToArray();
		}
	}
}
