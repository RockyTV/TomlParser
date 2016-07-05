using System;
using System.Diagnostics;
using System.IO;

using NUnit.Framework;

namespace TomlParser.UnitTests
{
	[TestFixture]
	public class TomlKeyValueTest
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
		public void IsValidName()
		{
			Assert.Throws<InvalidKeyNameException>(delegate { new TomlKeyValue("!@#$%¨&*()key", null); });
			Assert.Throws<InvalidKeyNameException>(delegate { new TomlKeyValue("[key]", null); });
			Assert.DoesNotThrow(delegate { new TomlKeyValue("k_ey", null); });
			Assert.DoesNotThrow(delegate { new TomlKeyValue("k-ey", null); });
			Assert.DoesNotThrow(delegate { new TomlKeyValue("1234", null); });
		}
	}
}
