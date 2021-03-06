﻿using System;
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
			List<TomlKeyValue> rootValues = new List<TomlKeyValue>();
			List<TomlTable> tables = new List<TomlTable>();
			bool startedTable = false;
			TomlTable currTable = null;

			foreach (string line in File.ReadLines(fileName))
			{
				string currentLine = line.Trim();

				// Ignore empty lines
				if (string.IsNullOrEmpty(currentLine))
				{
					// Check if we have a table created and if we started it
					// If we do and the line is empty, add it and close it.
					if (currTable != null && startedTable)
					{
						if (!tables.Contains(currTable)) tables.Add(currTable);
						startedTable = false;
					}
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
