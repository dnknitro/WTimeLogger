using System;
using NUnit.Framework;
using ServiceStack.Razor;
using ServiceStack.Testing;
using ServiceStack.Text;
using ServiceStack.VirtualPath;
using WTimeLogger.Report;

namespace UnitTests.Report
{
	public class ReportTestClass : BaseTestClass
	{
		[Test]
		public void GenerateHtmlReportTest()
		{
			new TimeReporter().GenerateHtmlReport(DateTime.Now.Date.AddDays(-7), DateTime.Now);
		}

		[Test]
		public void Simple_static_example()
		{
			RazorFormat.Instance = null;
			var razor = new RazorFormat
			{
				VirtualFileSources = new InMemoryVirtualPathProvider(new BasicAppHost()),
				EnableLiveReload = false,
			}.Init();

			var page = razor.CreatePage("Hello @Model.Name! Welcome to Razor!");
			var html = razor.RenderToHtml(page, new {Name = "World"});
			html.Print();

			Assert.AreEqual("Hello World! Welcome to Razor!", html);
		}
	}
}