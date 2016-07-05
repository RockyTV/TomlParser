using System;
using System.Collections.Generic;
using System.Linq;

namespace TomlParser
{
	public class TomlDocument
	{
		public TomlKeyValue[] RootValues { get; set; }
		public TomlTable[] Tables { get; set; }

		public TomlDocument(IEnumerable<TomlKeyValue> rootValues, IEnumerable<TomlTable> tables)
		{
			RootValues = (rootValues ?? Enumerable.Empty<TomlKeyValue>()).ToArray();
			Tables = (tables ?? Enumerable.Empty<TomlTable>()).ToArray();
		}
	}
}
