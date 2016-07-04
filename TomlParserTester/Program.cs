using System;
using TomlParser;

namespace TomlParserTester
{
	public class Program
	{
		public static void Main(string[] args)
		{
			TomlDocument doc = TomlParser.TomlParser.FromFile("test.toml");
			foreach (TomlKeyValue kv in doc.RootValues)
			{
				Console.WriteLine("Key: {0}, Value: {1}, Type: {2}", kv.Key, kv.Value.ToString(), kv.Value.GetType());
			}

			foreach (TomlTable table in doc.Tables)
			{
				Console.WriteLine("Table: {0}", string.Join(".", table.Path));
				foreach (TomlKeyValue kv in table.Values) Console.WriteLine("  Key: {0}, Value: {1}, Type: {2}", kv.Key, kv.Value.ToString(), kv.Value.GetType());
			}

			Console.ReadKey();
		}
	}
}
