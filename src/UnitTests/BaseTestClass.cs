using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace UnitTests
{
	[TestFixture]
	public class BaseTestClass
	{
		private static ServiceStackHost _appHost;

		[OneTimeSetUp]
		public void TestFixtureSetUp()
		{
			_appHost = new BasicAppHost().Init();
		}

		[OneTimeTearDown]
		public void TestFixtureTearDown()
		{
			_appHost.Dispose();
		}
	}
}