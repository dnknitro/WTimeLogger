using System;
using System.Timers;
using System.Windows.Forms;
using LinqToDB;
using WTimeCommon;
using WTimeCommon.DB;
using WTimeCommon.DB.Model;
using Timer = System.Timers.Timer;

namespace WTimeLogger.Time
{
	internal class TimeLogger : IDisposable
	{
		private const int IntervalSec = 1;
		private const int IdleSec = 60;

		private TitleTime _lastTitleTime = null;

		private readonly Timer _timer = new Timer()
		{
			AutoReset = true,
			Enabled = true,
			Interval = IntervalSec*1000,
		};

		public TimeLogger()
		{
			_timer.Elapsed += TimerCallback;
		}

		private void TimerCallback(object sender, ElapsedEventArgs args)
		{
			_timer.Stop();

			string title;
			string executable = null;
			var inactiveTime = WindowsProxy.GetInactiveTime();
			if ((inactiveTime?.TotalSeconds ?? 0) <= IdleSec)
			{
				var activeProcess = WindowsProxy.GetActiveProcess();
				title = activeProcess?.MainWindowTitle ?? "N/A";
				try
				{
					executable = activeProcess?.MainModule.FileName;
				}
				catch (System.ComponentModel.Win32Exception ex)
				{
					executable = ex.Message;
				}
			}
			else
			{
				executable = title = "IDLE";
			}
			//Logger.Log(title);

			try
			{
				var now = DateTime.Now;
				now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

				var previousTitleTime = _lastTitleTime;
				var startDateTime = now.AddSeconds(-IntervalSec);

				if ((now - (previousTitleTime?.EndDateTime ?? now)).TotalSeconds > 30)
				{
					executable = title = "Sleep/Power Off";
					// ReSharper disable once PossibleNullReferenceException
					startDateTime = previousTitleTime.EndDateTime;
				}

				using (var dao = new Dao())
				{
					if (previousTitleTime?.Title == title)
					{
						previousTitleTime.EndDateTime = now;
						dao.Update(previousTitleTime);
					}
					else
					{
						_lastTitleTime = new TitleTime
						{
							Title = title,
							Executable = executable,
							StartDateTime = startDateTime,
							EndDateTime = now
						};

						_lastTitleTime.ID = Convert.ToInt64(dao.InsertWithIdentity(_lastTitleTime));
						//_lastTitleTime.ID = ++index;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex.ToString());
				MessageBox.Show(ex.Message);
				Application.Exit();
			}

			GC.Collect( 0, GCCollectionMode.Forced );
			GC.Collect( 1, GCCollectionMode.Forced );
			GC.Collect( 2, GCCollectionMode.Forced );
			GC.Collect();
			_timer.Start();
		}

		//private static long index = 0;

		public void Dispose()
		{
			_timer.Dispose();
		}
	}
}