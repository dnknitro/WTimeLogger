using System.IO;
using NUnit.Framework;
using WTimeCommon.DB;

namespace UnitTests
{
	[TestFixture]
	public class BaseTestFixture
	{
		[OneTimeSetUp]
		public void TestFixtureSetUp()
		{
			Dao.CustomConnectionString = string.Format("Data Source={0}{1}Data{1}WTimeLogger.db3; Version=3;", TestContext.CurrentContext.TestDirectory, Path.DirectorySeparatorChar);
		}
	}
}