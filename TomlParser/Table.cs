using System;
using System.Collections.Generic;
using System.Linq;

namespace TomlParser
{
	public class TomlTable
	{
		public string[] Path { get; }
		public List<TomlKeyValue> Values { get; set; }

		public TomlTable(string[] path, IEnumerable<TomlKeyValue> values)
		{
			Path = path;
			Values = (values ?? Enumerable.Empty<TomlKeyValue>()).ToList();
		}
	}
}
