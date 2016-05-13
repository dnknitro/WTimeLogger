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
			var fromDateTime = DateTime.Parse("05/05/2016");
			new TimeReporter().GenerateHtmlReport(fromDateTime, fromDateTime.AddDays(1));
		}

		[Test]
		public void GenerateHtmlReportTodayTest()
		{
			var fromDateTime = DateTime.Now.Date.AddDays(-1);
			new TimeReporter().GenerateHtmlReport(fromDateTime, fromDateTime.AddDays(1));
		}
	}
}