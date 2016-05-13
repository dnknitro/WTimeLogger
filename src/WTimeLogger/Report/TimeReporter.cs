using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
			var template = new Template(ResourceUtils.ReadStringFromEmbeddedResource("WTimeLogger.Report.reportTemplate.html"), '$', '$');

			var query = from p in Dao.Instance.TitleTime
				where p.StartDateTime >= fromDateTime && p.StartDateTime < toDateTime
				//orderby p.Title descending
				select p;

			var list = query.ToArray();
			if (!list.Any())
			{
				MessageBox.Show(string.Format("No data for the selected time span ({0} - {1})", fromDateTime, toDateTime), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			{
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
					.OrderByDescending(x => x.Duration)
					.ToList();
				template.Add("executables", executablesSummary);
			}

			{
				var minDateTime = list.Min(x => x.StartDateTime);
				var maxDateTime = list.Max(x => x.EndDateTime);
				template.Add("startTime", minDateTime.ToString());
				template.Add("endTime", maxDateTime.ToString());
				template.Add("totalDurationSec", (maxDateTime - minDateTime).TotalSeconds);

				var colorsHelper = new ColorsHelper();

				var colors = list.GroupBy(x => x.ExecutableName).ToDictionary(x => x.Key, y => colorsHelper.GetNextColor());
				var timeSpans = list
					.OrderBy(x => x.StartDateTime)
					.Select(x => new
					{
						startedStr = x.StartDateTime.ToString(),
						durationStr = x.Duration.ToString(),
						diffSec = (x.StartDateTime - minDateTime).TotalSeconds,
						durationSec = (x.EndDateTime - x.StartDateTime).TotalSeconds,
						description = x.ExecutableName,
						color = colors[x.ExecutableName],
						colorInverted = ColorsHelper.InvertHexColor(colors[x.ExecutableName])
					})
					.Where(x => x.durationSec > 120);

				template.Add("timeSpans", timeSpans);
			}
			var reportFile = @"report.html";
			File.WriteAllText(reportFile, template.Render());
			Process.Start(reportFile);
		}
	}
}