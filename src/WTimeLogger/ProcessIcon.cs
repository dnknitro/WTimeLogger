using System;
using System.Windows.Forms;
using WTimeLogger.Properties;
using WTimeLogger.Report;

namespace WTimeLogger
{
	/// <summary>
	///     https://github.com/linq2db/linq2db
	/// </summary>
	internal class ProcessIcon : IDisposable
	{
		private readonly NotifyIcon trayIcon = new NotifyIcon();

		public void Dispose()
		{
			trayIcon.Dispose();
		}

		public void Initialize()
		{
			// Put the trayIcon in the system tray and allow it react to mouse clicks.			
			trayIcon.MouseClick += TrayIconOnMouseClick;
			trayIcon.Icon = Resources.TrayIcon;
			trayIcon.Text = Resources.ApplicationTitle;
			trayIcon.Visible = true;

			// Attach a context menu.
			trayIcon.ContextMenuStrip = new ContextMenuStrip();

			var exitMenuItem = new ToolStripMenuItem("Exit");
			exitMenuItem.Click += (sender, args) => Application.Exit();
			trayIcon.ContextMenuStrip.Items.Add(exitMenuItem);

			trayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

			var reportMenuItem = new ToolStripMenuItem("Report");
			reportMenuItem.Click += (sender, args) =>
			{
				using (var form = new PickDateRangeForm())
				{
					form.ShowDialog();
					if (form.DialogResult == DialogResult.OK)
						new TimeReporter().GenerateHtmlReport(form.From.Date, form.To.Date.AddDays(1));
				}
			};
			trayIcon.ContextMenuStrip.Items.Add(reportMenuItem);

			reportMenuItem = new ToolStripMenuItem("Report: Today");
			reportMenuItem.Click += (sender, args) => new TimeReporter().GenerateHtmlReport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
			trayIcon.ContextMenuStrip.Items.Add(reportMenuItem);
		}

		private void TrayIconOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
		{
			trayIcon.ContextMenuStrip.Show();
		}
	}
}