using System;
using System.Linq;
namespace TomlParser
{
    public class TomlKeyValue
	{
		public string Key { get; }
		public object Value { get; }

		public TomlKeyValue(string key, object value)
		{
			if (key.Any(c => !char.IsLetterOrDigit(c) && c != '-' && c != '_'))
					throw new InvalidKeyNameException();

			Key = key;
			Value = value;
		}
	}
}
