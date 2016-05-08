using System;
using System.Windows.Forms;
using WTimeCommon.DB;
using WTimeLogger.Time;

namespace WTimeLogger
{
	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run( new Form1() );

			Dao.CreateDatabaseIfNotExists();

			// Show the system tray icon.
			using (var pi = new ProcessIcon())
			using (new TimeLogger())
			{
				pi.Initialize();

				// Make sure the application runs!
				Application.Run();
			}
		}
	}
}