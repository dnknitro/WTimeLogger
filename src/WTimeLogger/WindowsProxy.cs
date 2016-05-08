using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WTimeCommon;

namespace WTimeLogger
{
	internal class WindowsProxy
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = true)]
		static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		public static Process GetActiveProcess()
		{
			try
			{
				var hwnd = GetForegroundWindow();
				int processID;
				GetWindowThreadProcessId(hwnd, out processID);
				return Process.GetProcessById(processID);
			}
			catch (ArgumentException ex)
			{
				Logger.Log("log.txt", ex.ToString());
				return null;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct LASTINPUTINFO
		{
			public uint cbSize;
			public uint dwTime;
		}

		[DllImport("User32.dll")]
		private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

		///// <summary>
		/////     Idle time in ticks
		///// </summary>
		///// <returns></returns>
		//public static uint GetIdleTickCount()
		//{
		//	return (uint) Environment.TickCount - GetLastInputTime();
		//}

		///// <summary>
		/////     Last input time in ticks
		///// </summary>
		///// <returns></returns>
		//public static uint GetLastInputTime()
		//{
		//	var lastInPutNfo = new LASTINPUTINFO();
		//	lastInPutNfo.cbSize = (uint) Marshal.SizeOf(lastInPutNfo);
		//	if (!GetLastInputInfo(ref lastInPutNfo))
		//	{
		//		throw new Win32Exception(Marshal.GetLastWin32Error());
		//	}
		//	return lastInPutNfo.dwTime;
		//}

		public static TimeSpan? GetInactiveTime()
		{
			var info = new LASTINPUTINFO();
			info.cbSize = (uint) Marshal.SizeOf(info);
			if (GetLastInputInfo(ref info))
				return TimeSpan.FromMilliseconds(Environment.TickCount - info.dwTime);
			else
				return null;
		}
	}
}