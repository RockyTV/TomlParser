using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TomlParser
{
	public static class TomlParser
	{
		private static string GetValueType(string value)
		{
			if (TryParseBool(value)) return typeof(bool).ToString();
			else if (TryParseLong(value)) return typeof(long).ToString();
			else if (TryParseDouble(value)) return typeof(double).ToString();
			else return typeof(string).ToString();
		}

		private static object ParseValue(string value, string type)
		{
			switch (type)
			{
				case "System.Boolean":
					return bool.Parse(value);
				case "System.Int64":
					return long.Parse(value);
				case "System.Double":
					return double.Parse(value);
				case "System.String":
					return value;
				default:
					return null;
			}
		}

		private static bool TryParseLong(string value)
		{
			long parse;
			return long.TryParse(value, out parse);
		}

		private static bool TryParseBool(string value)
		{
			bool parse;
			return bool.TryParse(value, out parse);
		}

		private static bool TryParseDouble(string value)
		{
			double parse;
			return double.TryParse(value, out parse);
		}

		public static TomlDocument FromFile(string fileName)
		{
			using (StreamReader sr = new StreamReader(fileName))
			{
				string[] lines = sr.ReadToEnd().Split(new char[] { '\r' });
				List<TomlKeyValue> rootValues = new List<TomlKeyValue>();
				List<TomlTable> tables = new List<TomlTable>();

				bool startedTable = false;
				TomlTable currTable = null;
				for (int i = 0; i < lines.Length; i++)
				{
					string currentLine = lines[i].Trim();

					// Ignore empty lines
					if (string.IsNullOrEmpty(currentLine))
					{
						startedTable = false;
						continue;
					}

					// Ignore comments
					if (currentLine.StartsWith("#"))
						continue;

					if (currentLine.StartsWith("["))
					{
						string[] tableName = currentLine.Substring(currentLine.IndexOf('[') + 1, currentLine.IndexOf(']') - 1).Split('.');
						startedTable = true;
						currTable = new TomlTable(tableName, null);
						continue;
					}

					string keyName = currentLine.Remove(currentLine.IndexOf(" "));
					string stringValue = currentLine.Remove(0, currentLine.IndexOf("=") + 1).Trim();

					// Check if we have comments on the end of the line
					// If we do, remove them
					if (keyName.Contains("#")) keyName = keyName.Remove(keyName.LastIndexOf("#")).Trim();
					if (stringValue.Contains("#")) stringValue = stringValue.Remove(stringValue.LastIndexOf("#")).Trim();

					object keyValue = ParseValue(stringValue, GetValueType(stringValue));

					if (startedTable) currTable.Values.Add(new TomlKeyValue(keyName, keyValue));
					else rootValues.Add(new TomlKeyValue(keyName, keyValue));

					if (currTable != null && !tables.Contains(currTable)) tables.Add(currTable);
				}
				return new TomlDocument(rootValues, tables);
			}
		}
	}
}
