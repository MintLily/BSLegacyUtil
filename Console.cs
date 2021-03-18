using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static BSLegacyUtil.BuildInfo;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BSLegacyUtil
{
    class Con
    {

		private static int errorCount = 0;
		private static StreamWriter log;
		private static string fileprefix = BuildInfo.Name + "_";

		internal static void Init()
		{
			if (log == null)
			{
				string text = Environment.CurrentDirectory + "\\Logs\\" + 
					fileprefix + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".log";
				//string text = Path.Combine(Environment.CurrentDirectory, "Logs/" + Logger.fileprefix + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".log");
				FileInfo fileInfo = new FileInfo(text);
				DirectoryInfo directoryInfo = new DirectoryInfo(fileInfo.DirectoryName);
				if (!directoryInfo.Exists)
				{
					directoryInfo.Create();
				}
				else
				{
					CleanOld(directoryInfo);
				}
				FileStream stream;
				if (!fileInfo.Exists)
				{
					stream = fileInfo.Create();
				}
				else
				{
					stream = new FileStream(text, FileMode.Open, FileAccess.Write, FileShare.Read);
				}
				log = new StreamWriter(stream);
				log.AutoFlush = true;
			}
			Log("ConsoleOutput and Error Logs Initialized");
		}

		internal static void Stop()
		{
			if (log != null)
			{
				log.Close();
			}
		}

		internal static void CleanOld(DirectoryInfo logDirInfo)
		{
			FileInfo[] files = logDirInfo.GetFiles(fileprefix + "*");
			if (files.Length != 0)
			{
				List<FileInfo> list = (from x in files.ToList<FileInfo>()
									   orderby x.LastWriteTime
									   select x).ToList<FileInfo>();
				for (int i = list.Count - 10; i > -1; i--)
				{
					list[i].Delete();
				}
			}
		}

		internal static string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss.fff");
        }
		
		public static void Space()
		{
			Console.WriteLine("");
		}

		public static void Log(string s, string extra = null)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = Con.GetTimestamp();
			if (Program.isDebug)
            {
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			if (extra != null)
			{
				Console.Write("] " + s);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(" " + extra);
			}
			else
				Console.WriteLine("] " + s);
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void LogSuccess(string s)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.isDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Success");
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine("] " + s);
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void Input()
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.isDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("INPUT");
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Red;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] "); }
		}

		public static void ResetColors()
        {
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void InputOption(string num, string s, bool useable = true)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = Con.GetTimestamp();
			if (Program.isDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(num);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			if (useable)
				Console.WriteLine(s);
			else
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(s);
			}
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void SteamUN()
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.isDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("UserName");
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			if (log != null) { log.Write(""); }
		}

		public static void SteamPW()
		{
			Utilities.Utilities.GetPassword();
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.isDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Password");
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			//Utilities.Utilities.SecurePassword();
			if (log != null) { log.Write(""); }
		}

		public static void Error(string s)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			if (errorCount < 255)
			{
				string timestamp = GetTimestamp();
				if (Program.isDebug)
				{
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(timestamp);
					Console.ForegroundColor = foregroundColor;
					Console.Write("] [");
				}
				else
					Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(Name);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error");
				Console.ForegroundColor = foregroundColor;
				Console.Write("] ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(s);
				Console.ForegroundColor = foregroundColor;
				if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
				errorCount++;
			}
			if (errorCount == 255)
			{
				string timestamp2 = GetTimestamp();
				if (Program.isDebug)
				{
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(timestamp2);
					Console.ForegroundColor = foregroundColor;
					Console.Write("] [");
				}
				else
					Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(Name);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error");
				Console.ForegroundColor = foregroundColor;
				Console.Write("] ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("The console error limit has been reached.");
				Console.ForegroundColor = foregroundColor;
				if (log != null) { log.WriteLine("[" + timestamp2 + "] [" + Name + "] " + s); }
				errorCount++;
			}
		}

		public static void _Logo()
        {
			if (!Program.isDebug)
            {
				string title = @"
                   ____ _____ __                               
                  / __ ) ___// /   ___  ____ _____ ________  __
                 / __  \__ \/ /   / _ \/ __ `/ __ `/ ___/ / / /
                / /_/ /__/ / /___/  __/ /_/ / /_/ / /__/ /_/ / 
               /_____/____/_____/\___/\__, /\__,_/\___/\__, /  
                                     /____/           /____/   
";
				Console.WriteLine(title);
            }
        }
	}
}
