using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.IO;

namespace ConsoleApp1
{
	class Program
	{
		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);
		
		private static string ActiveWindowTitle()
		{
			//Create the variable
			const int nChar = 256;
			StringBuilder ss = new(nChar);
			//Run GetForeGroundWindows and get active window informations
			//assign them into handle pointer variable
			IntPtr handle = GetForegroundWindow();

			if (GetWindowText(handle, ss, nChar) > 0) return ss.ToString();
			else return "";
		}
		static void Main()
		{
			List<(DateTime, string)> data = new();

			Timer tmr = new();
			tmr.Interval = 10000;
			tmr.Elapsed += (x, y) => {
				//get title of active window
				string title = ActiveWindowTitle();
				//check if it is null and add it to list if correct
				if (title != "")
				{
					using FileStream f = File.Open(DateTime.Now.ToString("yyyy-MM-dd"), FileMode.Append);
					
					f.Write(Encoding.UTF8.GetBytes($"{DateTime.Now}: {title}\n"));

					//data.Add((DateTime.Now, title));
					Console.WriteLine($"{DateTime.Now}: {title}");
					//lbActiveWindows.Items.Add(DateTime.Now.ToString("hh:mm:ss") + " - " + title);
				}
			};
			tmr.Start();


			Console.ReadLine();
		}
	}
}
