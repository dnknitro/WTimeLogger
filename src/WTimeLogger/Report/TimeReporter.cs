using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Antlr4.StringTemplate;
using WTimeCommon;
using WTimeCommon.DB;
using WTimeCommon.DB.Model;

namespace WTimeLogger.Report
{
	public class TimeReporter
	{
		public void GenerateHtmlReport(DateTime fromDateTime, DateTime toDateTime)
		{
			using (var dao = new Dao())
			{
				var query = from p in dao.TitleTime
					where p.StartDateTime >= fromDateTime && p.StartDateTime < toDateTime
					//orderby p.Title descending
					select p;

				var list = query.ToArray();

				var template = new Template(ResourceUtils.ReadStringFromEmbeddedResource("WTimeLogger.Report.reportTemplate.html"), '$', '$');
				var executablesSummary = list.GroupBy(x => x.Executable)
					.Select(
						x => new TitleTime()
						{
							Executable = x.Key,
							StartDateTime = DateTime.MinValue,
							EndDateTime = DateTime.MinValue + TimeSpan.FromMilliseconds(x.Sum(z => z.Duration.TotalMilliseconds))
						}
					)
					.Where(x => x.Duration.TotalMinutes > 1)
					.OrderByDescending(x => x.Duration);
				template.Add("executables", executablesSummary);
				var reportFile = @"C:\Desktop\report.html";
				File.WriteAllText(reportFile, template.Render());
				Process.Start(reportFile);
			}
		}
	}
}