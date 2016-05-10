using System;
using NUnit.Framework;
using WTimeLogger.Report;

namespace UnitTests.Report
{
	public class ReportTestFixture : BaseTestFixture
	{
		[Test]
		public void GenerateHtmlReportTest()
		{
			new TimeReporter().GenerateHtmlReport(DateTime.Now.Date.AddDays(-7), DateTime.Now);
		}
	}
}