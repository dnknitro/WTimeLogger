using System;
using System.IO;
using System.Threading;

namespace WTimeCommon
{
	public class Logger
	{
		public static void Log(string message, params object[] parameters)
		{
			File.AppendAllText("log.txt",
				string.Format("{0}{1} [{2}] {3}", Environment.NewLine, DateTime.Now, Thread.CurrentThread.ManagedThreadId,
					string.Format(message, parameters)));
		}
	}
}