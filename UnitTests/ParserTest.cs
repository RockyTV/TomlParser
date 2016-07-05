using System;
using System.Diagnostics;
using System.IO;

using NUnit.Framework;

namespace TomlParser.UnitTests
{
	[TestFixture]
	public class TomlParserTest
	{
		private string fileName;
		private TomlDocument doc = null;

		[SetUp]
		public void Init()
		{
			fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.toml");
			doc = TomlParser.FromFile(fileName);
		}

		[Test]
		public void ParseDocument()
		{
			TomlDocument otherDoc = TomlParser.FromFile(fileName);
			Assert.Inconclusive("There's no way to compare if both documents are equal yet.");
		}
	}
}
