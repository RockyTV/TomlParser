namespace TomlParser
{
    public class TomlKeyValue
	{
		private readonly string key;
		private readonly object value;

		public string Key { get { return key; } }
		public object Value { get { return value; } }

		public TomlKeyValue(string key, object value)
		{
			this.key = key;
			this.value = value;
		}
	}
}
