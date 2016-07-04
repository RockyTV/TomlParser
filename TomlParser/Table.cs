using System.Collections.Generic;
using System.Linq;

namespace TomlParser
{
	public class TomlTable
	{
		private readonly string[] path;
		private List<TomlKeyValue> values;

		public string[] Path { get { return path; } }
		public List<TomlKeyValue> Values
		{
			get { return values; }
			set { values = value; }
		}

		public TomlTable(string[] path, IEnumerable<TomlKeyValue> values)
		{
			this.path = path;
			this.values = (values ?? Enumerable.Empty<TomlKeyValue>()).ToList();
		}
	}
}
